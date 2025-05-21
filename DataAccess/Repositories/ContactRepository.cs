using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Common.Types;

namespace DataAccess.Repositories
{
	public class ContactRepository
	{
		private readonly string _connectionString;

		public ContactRepository() 
		{
			_connectionString = "";
		}

		public void CreateConnection(int userID, string toUserName)
		{
			using(SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand("CreateConnection", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);
					command.Parameters.AddWithValue("@toUserName", toUserName);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public void DeleteConnection(int contactID) 
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand("DeleteConnection", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@ContactID", contactID);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public List<ConnectionDto> GetConnections(int userID, string connectionStatus)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("GetContacts", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);
					command.Parameters.AddWithValue("@ConnectionStatus", connectionStatus);

					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						List<ConnectionDto> connections = new List<ConnectionDto>();
						while (reader.Read())
						{
							connections.Add(new ConnectionDto()
							{
								UserData = new User()
								{
									UserID = (int)reader["UserID"],
									UserName = reader["UserName"].ToString(),
									FullName = reader["FullName"].ToString(),
									CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
									IsActive = Convert.ToBoolean(reader["IsActive"]),

								},

								ContactData = new Contact()
								{
									ContactID = (int)reader["ContactID"],
									ConnectionInit = Convert.ToDateTime(reader["ConnectionInit"]),
									ApprovedOn = Convert.ToDateTime(reader["ApprovedOn"]),
									ConnectionStatus = (ConnectionStatus)Enum.Parse(typeof(ConnectionStatus), reader["ConnectionStatus"].ToString()),
								}

							});
						}
						return connections;
					}
				}
			}
			return null;
		}

		public void InteractConnection(int userID, string connectionStatus)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand("InteractConnection", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);
					command.Parameters.AddWithValue("@ConnectionStatus", connectionStatus);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
