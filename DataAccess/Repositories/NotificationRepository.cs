using System.Data;
using Common.Types;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
	public class NotificationRepository
	{
		private readonly string _connectionString;

		public NotificationRepository()
		{
			_connectionString = "";
		}

		public void CreateNotification(Notification notification)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("CreateNotification", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@NotifyFor", notification.NotifyFor);
					command.Parameters.AddWithValue("@ActionPerformedBy", notification.ActionPerformedBy);
					command.Parameters.AddWithValue("@NotificationText", notification.NotificationText);
					command.Parameters.AddWithValue("@NotificationType", notification.NotificationType);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public void DeleteNotification(int notificationID, int userID) 
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("DeleteNotification", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@NotificationID", notificationID);
					command.Parameters.AddWithValue("@UserID", userID);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public List<Notification> GetNotifications(int userID)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("GetNotifications", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);

					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						List<Notification> notifications = new List<Notification>();
						while (reader.Read())
						{
							notifications.Add(new Notification()
							{
								NotificationID = (int)reader["NotificationID"],
								NotifyFor = (int)reader["NotifyFor"],
								ActionPerformedBy = reader["ActionPerformedBy"].ToString(),
								NotificationText = reader["NotificationText"].ToString(),
								NotificationType = (NotificationType)Enum.Parse(typeof(NotificationType), reader["NotificationType"].ToString()),
								isDeleted = Convert.ToBoolean(reader["IsDeleted"]),

							});
						}
						return notifications;
					}
				}
			}
			return null;
		}
	}
}
