using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Types
{
	public class SplitContact
	{
		public int SplitID { get; set; }
		public int SplitParticipantID { get; set; }
		public decimal OweAmount { get; set; }
		public SplitStatus SplitStatus { get; set; }
		public DateTime StatusUpdateOn { get; set; }
	}

	public enum SplitStatus
	{
		Default = 0,
		PENDING_APPROVAL = 1,
		REJECTED = 2,
		APPROVED_UNPAID = 3,
		PAID = 4,
		OWNED = 11,
		ALL = 12
	}
}
