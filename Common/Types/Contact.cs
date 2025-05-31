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
		public ContactStatus ContactStatus { get; set; }
		public DateTime ContactInit { get; set; }
		public DateTime StatusUpdateOn { get; set; }

	}

	public enum ContactStatus
	{
		PENDING = 1,
		APPROVED = 2,
		REJECTED = 3,
		DELETED = 4
	}
}
