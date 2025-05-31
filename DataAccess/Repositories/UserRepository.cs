using System.Data;
using Common.Types;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using Common;

namespace DataAccess.Repositories
{
	public class UserRepository
	{
		/// <summary>
		/// creates a user
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool CreateUser(User user)
		{
			Dictionary<string, object> UserInfo = new Dictionary<string, object>()
			{
				{ "UserName", user.UserName },
				{ "FullName", user.FullName },
				{ "PasswordHash", user.PasswordHash }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("CreateUser", UserInfo);
		}

		/// <summary>
		/// returns whole user without password
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public User GetUserById(int userID)
		{
			Dictionary<string, object> UserInfo = new Dictionary<string, object>()
			{
				{ "UserID", userID }
			};
			List<User> users = dbMethods.DbSelect("GetUserByID", UserInfo, reader =>
			{
				return new User {
					UserID = (int)reader["UserID"],
					FullName = reader["FullName"].ToString(),
					UserName = reader["UserName"].ToString(),
					IsActive = Convert.ToBoolean(reader["IsActive"]),
					CreatedAt = (DateTime)reader["CreatedAt"]
				};
			});
			return users.Count > 0 ? users[0] : null;
		}

		/// <summary>
		/// returns whole user with password
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public User GetUserByUserName(string userName)
		{
			Dictionary<string, object> UserInfo = new Dictionary<string, object>()
			{
				{"UserName", userName}
			};

			List<User> users = dbMethods.DbSelect<User>("GetUserByUserName", UserInfo, reader =>
			{
				return new User {
					UserID = (int)reader["UserID"],
					FullName = reader["FullName"].ToString(),
					UserName = reader["UserName"].ToString(),
					PasswordHash = reader["PasswordHash"].ToString(),
					IsActive = Convert.ToBoolean(reader["IsActive"]),
					CreatedAt = (DateTime)reader["CreatedAt"]
				};
			});
			return users.Count > 0 ? users[0] : null;
		}

		/// <summary>
		/// this will set IsActive status of user to 0
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public bool DeleteUser(string userID)
		{
			Dictionary<string, object> UserInfo = new Dictionary<string, object>()
			{
				{ "UserID", userID }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("DeleteUser", UserInfo);
		}

		/// <summary>
		/// this scopes only single userName based on id provided
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public string GetUserNameByID(int userID)
		{
			Dictionary<string, object> UserInfo = new Dictionary<string, object>()
			{
				{ "UserID", userID }
			};
			List<string> users = dbMethods.DbSelect<string>("GetUserNameByID", UserInfo, reader =>
			{
				return reader["UserName"].ToString();
			});
			return users[0];
		}

		/// <summary>
		/// this scopes only single userID based on username provided
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public int GetUserIDByUserName(string userName)
		{
			Dictionary<string, object> UserInfo = new Dictionary<string, object>()
			{
				{ "UserName", userName }
			};
			List<int> users = dbMethods.DbSelect<int>("GetUserIDByUserName", UserInfo, reader =>
			{
				return (int)reader["UserID"];
			});
			return users[0];
		}
	}
}
