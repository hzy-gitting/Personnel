using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	public class RP
	{
		public int Rp_id { get; set; }
		public string Rp_name { get; set; }
		public int Sal { get; set; }

		public RP() { }

		public RP(int rp_id, string rp_name, int sal)
		{
			this.Rp_id = rp_id;
			this.Rp_name = rp_name;
			this.Sal = sal;
		}
	}
}
