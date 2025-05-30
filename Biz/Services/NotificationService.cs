using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Types;

namespace Biz.Services
{
	public class NotificationService
	{
		NotificationService notifyService;
		public NotificationService() 
		{
			notifyService = new NotificationService();
		}

		public List<Notification> GetNotifications(int userID, string notificationType)
		{
			//later we have to add authorization checks before returing any notifications
			return notifyService.GetNotifications(userID, notificationType);
		}

		public int CreateNotification(Notification notification)
		{
			//auth checks
			return notifyService.CreateNotification(notification);
		}

		public bool DeleteNotification(int notificationID, int userID)
		{
			//auth checks
			return notifyService.DeleteNotification(notificationID, userID);
		}
	}
}
