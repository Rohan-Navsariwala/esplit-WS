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
		public DateTime ApprovedOn { get; set; }
		public DateTime PaidOn { get; set; }
	}

	public enum SplitStatus
	{
		PENDING_APPROVAL,
		APPROVED_UNPAID,
		PAID
	}
}
