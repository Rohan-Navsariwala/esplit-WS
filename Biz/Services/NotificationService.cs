using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Types;
using DataAccess.Repositories;

namespace Biz.Services
{
	public class NotificationService
	{
		NotificationRepository notifyRepo;
		public NotificationService() 
		{
			notifyRepo = new NotificationRepository();
		}

		public List<Notification> GetNotifications(int userID)
		{
			//later we have to add authorization checks before returing any notifications
			return notifyRepo.GetNotifications(userID);
		}

		public int CreateNotification(Notification notification)
		{
			//auth checks
			return notifyRepo.CreateNotification(notification);
		}

		public bool DeleteNotification(int notificationID, int userID)
		{
			//auth checks
			return notifyRepo.DeleteNotification(notificationID, userID);
		}
	}
}
