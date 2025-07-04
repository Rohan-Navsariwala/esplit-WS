﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Types
{
	public class SplitInfo
	{
		public int SplitID { get; set; }
		public int CreatedBy { get; set; }
		public string SplitDescription { get; set; }
		public DateTime CreatedOn { get; set; }
		public decimal SplitAmount { get; set; }
		public DateTime UpdatedOn { get; set; }
		public bool IsClosed { get; set; }
		public SplitStatus SplitParticipantStatus { get; set; }
	}
}
