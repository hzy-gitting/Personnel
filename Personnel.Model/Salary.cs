using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	public class Salary
	{
		public int id { get; set; }
		public int p_sal { get; set; }
		public int rp_id { get; set; }
		public DateTime rp_date { get; set; }
		public int handler { get; set; }
		public DateTime cancel_date { get; set; }
		public string cancel_reason { get; set; }

		public Salary(int id, int p_sal, int rp_id, DateTime rp_date,
			int handler, DateTime cancel_date, string cancel_reason)
		{
			this.id = id;
			this.rp_id = rp_id;
			this.p_sal = p_sal;
			this.rp_date = rp_date;
			this.handler = handler;
			this.cancel_date = cancel_date;
			this.cancel_reason = cancel_reason;
		}
	}
}
