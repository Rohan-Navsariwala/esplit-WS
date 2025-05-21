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

		public void AddSplitParticipant(int splitID, int userID, decimal oweAmount)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand("AddSplitParticipant", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@SplitID", splitID);
					command.Parameters.AddWithValue("@UserID", userID);
					command.Parameters.AddWithValue("@OweAmount", oweAmount);

					connection.Open();
					command.ExecuteNonQuery();
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
					command.Parameters.AddWithValue("@SplitID", splitID);
					command.Parameters.AddWithValue("@UserID", userID);
					command.Parameters.AddWithValue("@OweAmount", oweAmount);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
