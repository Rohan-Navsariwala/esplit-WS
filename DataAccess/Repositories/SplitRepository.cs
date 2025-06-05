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
		public bool AddSplitParticipant(SplitContact splitContact)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "SplitID", splitContact.SplitID },
				{ "SplitParticipantID", splitContact.SplitParticipantID },
				{ "OweAmount", splitContact.OweAmount }
			};
			return (bool)DataAccess.DBMethods.DbUpdate("AddSplitParticipant", parameters);
		}

		public int CreateSplit(SplitInfo split)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", split.CreatedBy },
				{ "SplitAmount", split.SplitAmount },
				{ "SplitDescription", split.SplitDescription }
			};
			return Convert.ToInt32(DataAccess.DBMethods.DbUpdate("CreateSplit", parameters, true));
		}

		public bool DeleteSplitParticipant(int splitParticipantID, int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "SplitParticipantID", splitParticipantID },
				{ "SplitID", splitID }
			};
			return (bool)DataAccess.DBMethods.DbUpdate("DeleteSplitParticipant", parameters);
		}

		public List<ParticipantDto> GetSplitParticipant(int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "SplitID", splitID }
			};
			return DataAccess.DBMethods.DbSelect<ParticipantDto>("GetSplitParticipants", parameters, reader =>
			{
				return new ParticipantDto {
					SplitParticipant = new SplitContact {
						SplitID = (int)reader["SplitID"],
						SplitParticipantID = (int)reader["SplitParticipantID"],
						OweAmount = (decimal)reader["OweAmount"],
						SplitStatus = (SplitStatus)reader["SplitStatus"],
						StatusUpdateOn = reader["StatusUpdateOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["StatusUpdateOn"]),
					},
					UserData = new User {
						UserID = (int)reader["UserID"],
						FullName = reader["FullName"].ToString(),
						UserName = reader["UserName"].ToString(),
						CreatedAt = (DateTime)reader["CreatedAt"],
						IsActive = Convert.ToBoolean(reader["IsActive"])
					}
				};
			});
		}

		public List<SplitInfo> GetSplits(int userID, SplitStatus splitStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitStatus", (int)splitStatus }
			};
			return DataAccess.DBMethods.DbSelect<SplitInfo>("GetSplits", parameters, reader =>
			{
				return new SplitInfo {
					SplitID = (int)reader["SplitID"],
					CreatedBy = (int)reader["CreatedBy"],
					SplitAmount = (decimal)reader["SplitAmount"],
					SplitDescription = reader["SplitDescription"].ToString(),
					CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
					UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["UpdatedOn"]),
					IsClosed = Convert.ToBoolean(reader["IsClosed"]),
				};
			});
		}

		public bool PayDue(int userID, int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitID", splitID }
			};
			return (bool)DataAccess.DBMethods.DbUpdate("PayDue", parameters);
		}

		public bool ToggleSplit(int userID, int splitID, SplitStatus splitStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitID", splitID },
				{ "SplitStatus", (int)splitStatus }
			};
			return (bool)DataAccess.DBMethods.DbUpdate("ToggleSplit", parameters);
		}

		public bool MarkClosed(int userID, int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitID", splitID },
			};

			return (bool)DataAccess.DBMethods.DbUpdate("MarkClosed", parameters);
		}
	}
}
