using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Common.Types;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace DataAccess
{
	public static class dbMehods
	{
		static string _connectionString = System.Configuration.ConfigurationManager.AppSettings["wString"];

		
		public static bool PingDatabase()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SqlCommand("SELECT 1", connection))
				{
					var result = command.ExecuteScalar();
					return Convert.ToInt32(result) == 1;
				}
			}
		}

		public static bool DbUpdate(string storedProcedure, Dictionary<string, string> parameters)
		{
			if( !string.IsNullOrWhiteSpace(storedProcedure) && parameters.Count() > 0)
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					using (var command = new SqlCommand(storedProcedure, connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						foreach (var parameter in parameters)
						{
							command.Parameters.AddWithValue($"@{parameter.Key}", parameter.Value);
						}
						connection.Open();
						if (command.ExecuteNonQuery() > 0)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				}
			}
			return false;
		}

		public static void DbSelect(string storedProcedure, Dictionary<string, string> parameters, out DataTable results)
		{
			if (!string.IsNullOrWhiteSpace(storedProcedure) && parameters.Count() > 0)
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					using (var command = new SqlCommand(storedProcedure, connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						foreach (var parameter in parameters)
						{
							command.Parameters.AddWithValue($"@{parameter.Key}", parameter.Value);
						}
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							results = new DataTable();
							results.Load(reader);

						}
					}
				}
			}
			results = null;
		}
	}
}
