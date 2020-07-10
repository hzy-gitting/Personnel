using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	public class RP
	{
		public int rp_id { get; set; }
		public string rp_name { get; set; }
		public int sal { get; set; }

		public RP()
		{

		}
		public RP(int rp_id, string rp_name, int sal)
		{
			this.rp_id = rp_id;
			this.rp_name = rp_name;
			this.sal = sal;
		}
	}
}
