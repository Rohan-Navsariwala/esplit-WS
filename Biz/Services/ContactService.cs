using Common;
using Common.Types;
using DataAccess.Repositories;

namespace Biz.Services
{
	public class ContactService
	{
		ContactRepository contactRepo;
		NotificationService notifyService;
		UserService userService;
		public ContactService() 
		{
			contactRepo = new ContactRepository();
			notifyService = new NotificationService();
		}

		public List<ContactDto> GetContacts(int userID, ContactStatus contactStatus, string actionType = "")
		{
			//later we have to add authorization checks before returing any connections
			List<ContactDto> contacts = contactRepo.GetContacts(userID, contactStatus);
			if(actionType == "Sent" && contactStatus == ContactStatus.PENDING)
			{
				return contacts.Where(c => c.ContactData.UserID1 == userID).ToList();
			}
			else if(actionType == "Received" && contactStatus == ContactStatus.PENDING)
			{
				return contacts.Where(c => c.ContactData.UserID2 == userID).ToList();
			}
			else
			{
				return contacts;
			}
		}

		public int CreateContact(int userID, string toUserName)
		{
			//auth checks
			int ContactID = contactRepo.CreateContact(userID, toUserName);

			if (ContactID > 0)
			{
				userService = new UserService();
				Notification notification = new Notification()
				{
					NotifyFor = userID,
					ActionPerformedBy = "",
					NotificationText = NotificationText.ConnectionCreated,
					NotificationType = NotificationType.CONNECTION_SENT
				};
				notifyService.CreateNotification(notification);

				notification.NotifyFor = userService.GetUserID(toUserName);
				notification.NotificationText = NotificationText.ConnectionRequested;
				notifyService.CreateNotification(notification);
				
				return ContactID;
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
					NotifyFor = 0,
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
					NotifyFor = 0,
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
