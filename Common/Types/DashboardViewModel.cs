using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Types
{
	public class DashboardViewModel
	{
		public List<List<SplitInfo>> SplitsData {  get; set; }

		public List<ContactDto> Contacts { get; set; }
	}
}
