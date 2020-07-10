using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	public class Salary
	{
		public int Id { get; set; }
		public int Pre_sal { get; set; }
		public int Rp_id { get; set; }
		public DateTime Rp_date { get; set; }
		public int Handler { get; set; }
		public DateTime Cancel_date { get; set; }
		public string Cancel_reason { get; set; }
		public int Current_sal { get; set; }

		public Salary() { }

		public Salary(int Id, int Pre_sal, int Rp_id, DateTime Rp_date,
			int Handler, DateTime Cancel_date, string Cancel_reason,int Current_sal)
		{
			this.Id = Id;
			this.Pre_sal = Pre_sal;
			this.Rp_id = Rp_id;
			this.Rp_date = Rp_date;
			this.Handler = Handler;
			this.Cancel_date = Cancel_date;
			this.Cancel_reason = Cancel_reason;
			this.Current_sal = Current_sal;
		}
	}
}
