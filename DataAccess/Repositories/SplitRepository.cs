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
		public bool AddSplitParticipant(List<SplitContact> splitContacts)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "SplitID", splitContacts.First().SplitID },
				{ "SplitParticipantID", splitContacts.First().SplitParticipantID },
				{ "OweAmount", splitContacts.First().OweAmount }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("AddSplitParticipant", parameters);
		}

		public int CreateSplit(SplitInfo split)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", split.CreatedBy },
				{ "SplitAmount", split.SplitAmount },
				{ "SplitDescription", split.SplitDescription },
				{ "Deadline", split.Deadline },
			};
			return (int)DataAccess.dbMethods.DbUpdate("CreateSplit", parameters, true);
		}

		public bool DeleteSplitParticipant(int userID, int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitID", splitID }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("DeleteSplitParticipant", parameters);
		}

		public List<ParticipantDto> GetSplitParticipant(int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "SplitID", splitID }
			};
			return DataAccess.dbMethods.DbSelect<ParticipantDto>("GetSplitParticipant", parameters, reader =>
			{
				return new ParticipantDto {
					SplitParticipant = new SplitContact {
						SplitID = (int)reader["SplitID"],
						SplitParticipantID = (int)reader["SplitParticipantID"],
						OweAmount = (decimal)reader["OweAmount"],
						SplitStatus = (SplitStatus)Enum.Parse(typeof(SplitStatus), reader["SplitStatus"].ToString()),
						StatusUpdateOn = Convert.ToDateTime(reader["ApprovedOn"]),
						PaidOn = Convert.ToDateTime(reader["PaidOn"]),
					},
					UserData = new User {
						UserID = (int)reader["UserID"],
						FullName = reader["FullName"].ToString(),
						UserName = reader["UserName"].ToString(),
						IsActive = Convert.ToBoolean(reader["IsActive"]),
						CreatedAt = (DateTime)reader["CreatedAt"]
					}
				};
			});
		}

		public List<SplitInfo> GetNotifications(int userID, string splitStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitStatus", splitStatus }
			};
			return DataAccess.dbMethods.DbSelect<SplitInfo>("GetSplits", parameters, reader =>
			{
				return new SplitInfo {
					SplitID = (int)reader["SplitID"],
					CreatedBy = (int)reader["CreatedBy"],
					SplitAmount = (decimal)reader["SplitAmount"],
					SplitDescription = reader["SplitDescription"].ToString(),
					CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
					UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"]),
					Deadline = Convert.ToDateTime(reader["Deadline"]),
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
			return (bool)DataAccess.dbMethods.DbUpdate("PayDue", parameters);
		}

		public bool ToggleSplitRequest(int userID, int splitID, int change)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitID", splitID },
				{ "Change", change }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("ToggleSplitRequest", parameters);
		}
	}
}
