using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Types
{
	public class Contact
	{
		public int ContactID { get; set; }
		public int UserID1 { get; set; }
		public int UserID2 { get; set; }
		public ConnectionStatus ConnectionStatus { get; set; }
		public DateTime ConnectionInit { get; set; }
		public DateTime ApprovedOn { get; set; }

	}

	public enum ConnectionStatus
	{
		[Description("PENDING")]
		PENDING,

		[Description("APPROVED")]
		Approved,

		[Description("REJECTED")]
		Rejected,

		[Description("DELETED")]
		Deleted
	}
}
