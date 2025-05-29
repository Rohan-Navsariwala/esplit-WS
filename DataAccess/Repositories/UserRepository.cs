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
		public bool CreateUser(User user)
		{
			Dictionary<string, object> UserInfo = new Dictionary<string, object>()
			{
				{"UserID", user.UserID },
				{ "IsActive", user.IsActive },
				{ "CreatedAt", user.CreatedAt }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("CreateUser", UserInfo);
		}

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

		public bool DeleteUser(string userID)
		{
			Dictionary<string, object> UserInfo = new Dictionary<string, object>()
			{
				{ "UserID", userID }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("DeleteUser", UserInfo);
		}
	}
}
