﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Types
{
	public class User
	{
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string FullName { get; set; }
		public string PasswordHash { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsActive { get; set; }
	}
}
