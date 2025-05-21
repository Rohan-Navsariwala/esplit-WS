using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Types
{
	public class ConnectionDto
	{
		public User UserData { get; set; }
		public Contact ContactData { get; set; }

		public ConnectionDto()
		{
			ContactData.UserID1 = 0;
			ContactData.UserID2 = 0;
		}
	}
}
