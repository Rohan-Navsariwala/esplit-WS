using System.Data;
using Common.Types;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
	public class NotificationRepository
	{
		/// <summary>
		/// creates a notification
		/// </summary>
		/// <param name="notification"></param>
		/// <returns></returns>
		public int CreateNotification(Notification notification)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>() 
			{
				{ "NotifyFor", notification.NotifyFor },
				{ "ActionPerformedBy", notification.ActionPerformedBy },
				{ "NotificationText", notification.NotificationText },
				{ "NotificationType", notification.NotificationType }
			};
			return Convert.ToInt32(DataAccess.dbMethods.DbUpdate("CreateNotification", parameters, true));
		}

		/// <summary>
		/// deletes a notification
		/// </summary>
		/// <param name="notificationID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public bool DeleteNotification(int notificationID, int userID) 
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "NotificationID", notificationID },
				{ "NotifyFor", userID }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("DeleteNotification", parameters);
		}

		/// <summary>
		/// gets all notification for user which are not deleted yet
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public List<Notification> GetNotifications(int userID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "NotifyFor", userID }
			};
			return DataAccess.dbMethods.DbSelect<Notification>("GetNotifications", parameters, reader =>
			{
				return new Notification {
					NotificationID = (int)reader["NotificationID"],
					NotifyFor = (int)reader["NotifyFor"],
					ActionPerformedBy = reader["ActionPerformedBy"].ToString(),
					NotificationText = reader["NotificationText"].ToString(),
					NotificationType = (NotificationType)reader["NotificationType"]
					//IsDeleted = Convert.ToBoolean(reader["IsDeleted"]),
				};
			});
		}
	}
}
