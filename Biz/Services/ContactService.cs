using Common;
using Common.Types;
using Common.Utils;
using DataAccess.Repositories;

namespace Biz.Services
{
	//	- delete contact won't work(throws exception) if the cache is not populated (i.e. delete gets executed before get contact)
	//- same issue for interact request( if get requests has not called before then cache won't be populated yet)
	public class ContactService
	{
		private readonly ContactRepository contactRepo;
		private readonly NotificationService notifyService;
		private readonly CacheService _cache;
		private readonly Identity _common;
		private readonly int _userID;
		private readonly string _userName;
		public ContactService(CacheService cache, NotificationService notificationService, Identity commonMethods) 
		{
			contactRepo = new ContactRepository();
			notifyService = notificationService;
			_cache = cache;
			_common = commonMethods;
			(_userName, _userID) = _common.GetClaims();
		}

		public List<ContactDto> GetContacts(ContactStatus contactStatus, ContactRequestDirection reqdir = ContactRequestDirection.NILL)
		{
			List<ContactDto> contacts = contactRepo.GetContacts(_userID, contactStatus);
			List<ContactDto> result;
			string cacheKey = $"Contacts_{reqdir}_{contactStatus}_{_userID}";

			if (contactStatus == ContactStatus.PENDING)
			{
				result = reqdir switch
				{
					ContactRequestDirection.SENT => contacts.Where(c => c.ContactData.UserID1 == _userID).ToList(),
					ContactRequestDirection.RECEIVED => contacts.Where(c => c.ContactData.UserID2 == _userID).ToList(),
					_ => contacts
				};
			}
			else
			{
				result = contacts;
			}

			_cache.InsertIntoCache(cacheKey, result);
			return result;
		}


		public bool CreateContact(string toUserName)
		{
			int ContactID = contactRepo.CreateContact(_userID, toUserName);

			if (ContactID > 0)
			{
				UserService userService = new UserService();
				Notification notification = new Notification()
				{
					NotifyFor = _userID,
					ActionPerformedBy = _userName,
					NotificationText = NotificationText.ConnectionCreated + toUserName,
					NotificationType = NotificationType.CONNECTION_SENT
				};
				notifyService.CreateNotification(notification);

				notification.NotifyFor = userService.GetUserID(toUserName);
				notification.NotificationText = NotificationText.ConnectionRequested + _userName;
				notifyService.CreateNotification(notification);

				//return contactRepo.GetThisContact(ContactID);
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// as of now this method only utilized for deleting the approved/existing connections
		/// </summary>
		/// <param name="contactID"></param>
		/// <returns></returns>
		public bool DeleteContact(int contactID)
		{
			if (IsOperationAllowed(contactID, "DELETE"))
			{
				if (contactRepo.DeleteContact(contactID)){
					Notification notification = new Notification()
					{
						NotifyFor = 1,
						ActionPerformedBy = "SYSTEM",
						NotificationText = NotificationText.ConnectionDeleted,
						NotificationType = NotificationType.CONNECTION_DELETED
					};
					notifyService.CreateNotification(notification);
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// this method is to be utilized for either accepting or rejecting a connection request as of now
		/// </summary>
		/// <param name="contactID"></param>
		/// <param name="contactStatus"></param>
		/// <returns></returns>
		public bool InteractContact(int contactID, ContactStatus contactStatus)
		{
			if (IsOperationAllowed(contactID))
			{
				int affectedUser = contactRepo.InteractContact(contactID, contactStatus);
				if (affectedUser > 0)
				{
					Notification notification = new Notification() {
						NotifyFor = affectedUser,
						ActionPerformedBy = "",
						NotificationText = (contactStatus == ContactStatus.APPROVED
							? NotificationText.ConnectionAccepted : NotificationText.ConnectionRejected) + affectedUser,
						NotificationType = contactStatus == ContactStatus.APPROVED
							? NotificationType.CONNECTION_ACCEPTED : NotificationType.CONNECTION_REJECTED
					};
					notifyService.CreateNotification(notification);

					notification.NotifyFor = affectedUser;
					notifyService.CreateNotification(notification);

					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// this method is used for authorization checks if the operation on the given ContactID is allowed by logged in user or not
		/// </summary>
		/// <param name="ContactID"></param>
		/// <param name="operation"></param>
		/// <returns></returns>
		public bool IsOperationAllowed(int ContactID, string operation = "")
		{
			ContactStatus contactStatus = (operation == "DELETE") ? ContactStatus.APPROVED : ContactStatus.PENDING;
			ContactRequestDirection reqdir = (operation == "DELETE") ? ContactRequestDirection.NILL : ContactRequestDirection.RECEIVED;

			//as of now this logic can't be utilized for checking for deleting the sent connection request
			string cacheKey = $"Contacts_{reqdir}_{contactStatus}_{_userID}";
			List<ContactDto> contacts;

			//if we dont find the object in cache then it will return null and typecasting the null will throw an exception
			try
			{
				contacts = (List<ContactDto>)_cache.GetFromCache(cacheKey);
			}
			catch (Exception ex)
			{
				//true way to handle this exception would be to populate the cache and then perform all the check operations
				Console.WriteLine(ex);
				return false;
			}

			if(contacts.Find(c => c.ContactData.ContactID == ContactID) != default(ContactDto))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
