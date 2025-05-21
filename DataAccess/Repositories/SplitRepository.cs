using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Common.Types;

namespace DataAccess.Repositories
{
	public class SplitRepository
	{
		private readonly string _connectionString;

		public SplitRepository() 
		{
			_connectionString = "";
		}

		public void AddSplitParticipant(List<SplitContact> splitContacts)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand("AddSplitParticipant", connection))
				{
					connection.Open();
					foreach(SplitContact splitContact in splitContacts)
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@SplitID", splitContact.SplitID);
						command.Parameters.AddWithValue("@UserID", splitContact.SplitParticipantID);
						command.Parameters.AddWithValue("@OweAmount", splitContact.OweAmount);
						command.ExecuteNonQuery();
					}
				}
			}
		}

		public void CreateSplit(SplitInfo split)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand("AddSplit", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", split.CreatedBy);
					command.Parameters.AddWithValue("@SplitDescription", split.SplitDescription);
					command.Parameters.AddWithValue("@Deadline", split.Deadline);
					command.Parameters.AddWithValue("@SplitAmount", split.SplitAmount);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public void DeleteSplitParticipant(int userID, int splitID)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand("DeleteSplitParticipant", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);
					command.Parameters.AddWithValue("@SplitID", splitID);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public List<ParticipantDto> GetSplitParticipant(int splitID)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("GetSplitParticipant", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@SplitID", splitID);

					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						List<ParticipantDto> participants = new List<ParticipantDto>();
						while (reader.Read())
						{
							participants.Add(new ParticipantDto() {

								SplitParticipant = new SplitContact() {
									SplitID = (int)reader["SplitID"],
									SplitParticipantID = (int)reader["SplitParticipantID"],
									OweAmount = (decimal)reader["OweAmount"],
									SplitStatus = (SplitStatus)Enum.Parse(typeof(SplitStatus), reader["SplitStatus"].ToString()),
									ApprovedOn = Convert.ToDateTime(reader["ApprovedOn"]),
									PaidOn = Convert.ToDateTime(reader["ApprovedOn"])
								},

								UserData = new User() {
									UserID = (int)reader["UserID"],
									FullName = reader["FullName"].ToString(),
									UserName = reader["UserName"].ToString(),
									IsActive = Convert.ToBoolean(reader["IsActive"]),
									CreatedAt = (DateTime)reader["CreatedAt"]
								}
							});
						}
						return participants;
					}
				}
			}
			return null;
		}

		public List<SplitInfo> GetNotifications(int userID, string splitStatus)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				using (var command = new SqlCommand("GetSplits", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);
					command.Parameters.AddWithValue("@SplitStatus", splitStatus);

					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						List<SplitInfo> splits = new List<SplitInfo>();
						while (reader.Read())
						{
							splits.Add(new SplitInfo() {

								SplitID = (int)reader["SplitID"],
								CreatedBy = (int)reader["CreatedBy"],
								SplitAmount = (decimal)reader["SplitAmount"],
								SplitDescription = reader["SplitDescription"].ToString(),
								CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
								UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"]),
								Deadline = Convert.ToDateTime(reader["Deadline"]),
								IsClosed = Convert.ToBoolean(reader["IsClosed"]),

							});
						}
						return splits;
					}
				}
			}
			return null;
		}

		public void PayDue(int userID, int splitID)
		{
			using(SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand("PayDue", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);
					command.Parameters.AddWithValue("@SplitID", splitID);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public void ToggleSplitRequest(int userID, int splitID, int change)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand("ToggleSplitRequest", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userID);
					command.Parameters.AddWithValue("@SplitID", splitID);
					command.Parameters.AddWithValue("@Change", change);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
