using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Types;
using DataAccess.Repositories;

namespace Biz.Services
{
	public class ContactService
	{
		ContactRepository contactRepo;
		NotificationService notifyService;
		public ContactService() 
		{
			contactRepo = new ContactRepository();
			notifyService = new NotificationService();
		}

		public List<ContactDto> GetContacts(int userID, string connectionStatus, string actionType = "")
		{
			//later we have to add authorization checks before returing any connections
			List<ContactDto> connections = contactRepo.GetContacts(userID, connectionStatus);
			if(actionType == "Sent" && connectionStatus == "PENDING")
			{
				return connections.Where(c => c.ContactData.UserID1 == userID).ToList();
			}
			else if(actionType == "Received" && connectionStatus == "PENDING")
			{
				return connections.Where(c => c.ContactData.UserID2 == userID).ToList();
			}
			else
			{
				return connections;
			}


		}

		public int CreateContact(int userID, string toUserName)
		{
			//auth checks
			int ConnectionID = contactRepo.CreateContact(userID, toUserName);

			if (ConnectionID > 0)
			{
				Notification notification = new Notification()
				{
					NotifyFor = userID,
					ActionPerformedBy = toUserName,
					NotificationText = NotificationText.ConnectionCreated,
					NotificationType = NotificationType.Connection_Sent
				};
				notifyService.CreateNotification(notification);

				notification.ActionPerformedBy = "username from session";
				notification.NotifyFor = 1; //here receivers uid goes
				notification.NotificationText = NotificationText.ConnectionRequested;
				notifyService.CreateNotification(notification);
				
				return ConnectionID;
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
					ActionPerformedBy = "username from session",
					NotificationText = NotificationText.ConnectionDeleted,
					NotificationType = NotificationType.Connection_Deleted
				};
				notifyService.CreateNotification(notification);
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool InteractContact(int contactID, string connectionStatus)
		{
			//auth checks
			if(contactRepo.InteractContact(contactID, connectionStatus))
			{
				Notification notification = new Notification()
				{
					NotifyFor = 1,
					ActionPerformedBy = "username from session",
					NotificationText = connectionStatus == "APPROVED" 
						? NotificationText.ConnectionAccepted : NotificationText.ConnectionRejected,
					NotificationType = connectionStatus == "APPROVED" 
						? NotificationType.Connection_Accepted : NotificationType.Connection_Rejected

				};
				var a = test.Four;
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
