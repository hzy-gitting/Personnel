using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	public class Quit
	{
		public int id { get; set; }
		public DateTime quit_date { get; set; }
		public string quit_reason { get; set; }

		public Quit()
		{

		}
		public Quit(int id, DateTime quit_date, string quit_reason)
		{
			this.id = id;
			this.quit_date = quit_date;
			this.quit_reason = quit_reason;
		}
	}
}
