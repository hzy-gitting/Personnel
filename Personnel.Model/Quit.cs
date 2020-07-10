using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	public class Quit
	{
		public int Id { get; set; }
		public DateTime Quit_date { get; set; }
		public string Quit_reason { get; set; }

		public Quit() { }

		public Quit(int id, DateTime quit_date, string quit_reason)
		{
			this.Id = id;
			this.Quit_date = quit_date;
			this.Quit_reason = quit_reason;
		}
	}
}
