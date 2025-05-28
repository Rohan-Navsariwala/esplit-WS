using System.Data;
using Common.Types;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
	public class NotificationRepository
	{
		public bool CreateNotification(Notification notification)
		{
			Dictionary<string, object> _notifications = new Dictionary<string, object>() 
			{
				{ "NotifyFor", notification.NotifyFor },
				{ "ActionPerformedBy", notification.ActionPerformedBy },
				{ "NotificationText", notification.NotificationText },
				{ "NotificationType", notification.NotificationType }
			};
			return DataAccess.dbMethods.DbUpdate("CreateNotification", _notifications);
		}

		public bool DeleteNotification(int notificationID, int userID) 
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "NotificationID", notificationID },
				{ "UserID", userID }
			};
			return DataAccess.dbMethods.DbUpdate("DeleteNotification", parameters);
		}

		public List<Notification> GetNotifications(int userID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "UserID", userID }
			};
			return DataAccess.dbMethods.DbSelect<Notification>("GetNotifications", parameters, reader =>
			{
				return new Notification {
					NotificationID = (int)reader["NotificationID"],
					NotifyFor = (int)reader["NotifyFor"],
					ActionPerformedBy = reader["ActionPerformedBy"].ToString(),
					NotificationText = reader["NotificationText"].ToString(),
					NotificationType = (NotificationType)Enum.Parse(typeof(NotificationType), reader["NotificationType"].ToString()),
					isDeleted = Convert.ToBoolean(reader["IsDeleted"]),
				};
			});
		}
	}
}
