using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
	public static class dbMehods
	{
		static string _connectionString = Common.Configs.connectionString;
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
	}
}
