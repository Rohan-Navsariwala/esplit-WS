using Common;
using Common.Types;
using Common.Utils;
using DataAccess.Repositories;

namespace Biz.Services
{
	public class ContactService
	{
		private readonly ContactRepository contactRepo;
		private readonly NotificationService notifyService;
		private readonly CacheService _cache;
		private readonly CommonMethods _commonMethods;
		public ContactService(CacheService cache, NotificationService notificationService, CommonMethods commonMethods) 
		{
			contactRepo = new ContactRepository();
			notifyService = notificationService;
			_cache = cache;
			_commonMethods = commonMethods;
		}

		public List<ContactDto> GetContacts(int userID, ContactStatus contactStatus, ContactRequestDirection actionType = ContactRequestDirection.NILL)
		{
			List<ContactDto> contacts = contactRepo.GetContacts(userID, contactStatus);
			List<ContactDto> result;
			string cacheKey = $"Contacts_{actionType}_{contactStatus}_{userID}";

			if (contactStatus == ContactStatus.PENDING)
			{
				result = actionType switch
				{
					ContactRequestDirection.SENT => contacts.Where(c => c.ContactData.UserID1 == userID).ToList(),
					ContactRequestDirection.RECEIVED => contacts.Where(c => c.ContactData.UserID2 == userID).ToList(),
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


		public int CreateContact(string toUserName)
		{
			(string userName, int userID) = _commonMethods.GetClaims();
			int toUserID = contactRepo.CreateContact(userID, toUserName);

			if (toUserID > 0)
			{
				UserService userService = new UserService();
				Notification notification = new Notification()
				{
					NotifyFor = userID,
					ActionPerformedBy = userName,
					NotificationText = NotificationText.ConnectionCreated,
					NotificationType = NotificationType.CONNECTION_SENT
				};
				notifyService.CreateNotification(notification);

				notification.NotifyFor = userService.GetUserID(toUserName);
				notification.NotificationText = NotificationText.ConnectionRequested;
				notifyService.CreateNotification(notification);
				
				return toUserID;
			}
			else
			{
				return 0;
			}
		}

		public bool DeleteContact(int contactID)
		{
			//auth checks
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
		public bool InteractContact(int contactID, ContactStatus contactStatus)
		{
			//auth checks
			int affectedUser = contactRepo.InteractContact(contactID, contactStatus);
			if (affectedUser > 0)
			{
				Notification notification = new Notification()
				{
					NotifyFor = 1,
					ActionPerformedBy = "",
					NotificationText = contactStatus == ContactStatus.APPROVED
						? NotificationText.ConnectionAccepted : NotificationText.ConnectionRejected,
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
	}
}
