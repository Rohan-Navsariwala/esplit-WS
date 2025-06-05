using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Types;
using Common.Utils;
using DataAccess.Repositories;
using static Azure.Core.HttpHeader;

namespace Biz.Services
{
	public class NotificationService
	{
		NotificationRepository notifyRepo;
		private readonly int _userID;
		private readonly string _userName;
		public NotificationService(Identity common) 
		{
			notifyRepo = new NotificationRepository();
			(_userName, _userID) = common.GetClaims();

		}

		public List<Notification> GetNotifications()
		{
			//later we have to add authorization checks before returing any notifications
			return notifyRepo.GetNotifications(_userID);
		}

		public int CreateNotification(Notification notification)
		{
			//auth checks
			return notifyRepo.CreateNotification(notification);
		}

		public bool DeleteNotification(int notificationID)
		{
			//auth checks
			return notifyRepo.DeleteNotification(notificationID, _userID);
		}
	}
}
