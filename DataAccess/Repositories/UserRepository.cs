using System.Data;
using Common.Types;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
	public class UserRepository
	{
		private readonly string _connectionString;

		public UserRepository()
		{
			_connectionString = "";
		}

		public void CreateUser(User user)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("CreateUser", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserName", user.UserName);
					command.Parameters.AddWithValue("@FullName", user.FullName);
					command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public User GetUserById(int userID)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("GetUserByID", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);

					connection.Open();
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return new User {
								UserID = (int)reader["UserID"],
								FullName = reader["FullName"].ToString(),
								UserName = reader["UserName"].ToString(),
								IsActive = Convert.ToBoolean(reader["IsActive"]),
								CreatedAt = (DateTime)reader["CreatedAt"]
							};
						}
					}
				}
			}
			return null;
		}

		public User GetUserByUserName(string userName)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("GetUserByUserName", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserName", userName);

					connection.Open();
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return new User {
								UserID = (int)reader["UserID"],
								FullName = reader["FullName"].ToString(),
								UserName = reader["UserName"].ToString(),
								PasswordHash = reader["PasswordHash"].ToString(),
								IsActive = Convert.ToBoolean(reader["IsActive"]),
								CreatedAt = (DateTime)reader["CreatedAt"]
							};
						}
					}
				}
			}
			return null;
		}

		public void DeleteUser(string userID)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("DeleteUser", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

	}

}
