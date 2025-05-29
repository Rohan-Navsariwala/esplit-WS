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
	public static class dbMethods
	{
		public static string _connectionString;
		
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

		/// <summary>
		/// Executes a stored procedure with the provided parameters and returns true if the operation(insert, delete, update) was successful.
		/// </summary>
		/// <param name="storedProcedure"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static object DbUpdate(string storedProcedure, Dictionary<string, object > parameters, bool expectScalar = false)
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
							command.Parameters.Add(new SqlParameter($"@{parameter.Key}", parameter.Value ?? DBNull.Value));
						}
						connection.Open();
						if (expectScalar)
						{
							return command.ExecuteScalar();
						}
						else
						{
							int rowsAffected = command.ExecuteNonQuery();
							return rowsAffected > 0;
						}
					}
				}
			}
			return expectScalar ? null : false;
		}

		/// <summary>
		/// Executes a stored procedure with the provided parameters and maps the results to a list of type T.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="storedProcedure"></param>
		/// <param name="parameters"></param>
		/// <param name="map"></param>
		/// <returns></returns>
		public static List<T> DbSelect<T>(string storedProcedure, Dictionary<string, object> parameters, Func<SqlDataReader,T> map)
		{
			List<T> results = new List<T>();
			if (!string.IsNullOrWhiteSpace(storedProcedure) && parameters.Count() > 0)
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					using (var command = new SqlCommand(storedProcedure, connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						foreach (var parameter in parameters)
						{
							command.Parameters.Add(new SqlParameter($"@{parameter.Key}", parameter.Value ?? DBNull.Value));
						}
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								results.Add(map(reader));
							}
						}
					}
				}
			}
			return results;
		}
	}
}
