using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	public class Login
	{
		public int uid { get; set; }
		public string username { get; set; }
		public string password { get; set; }

		public Login(string username,string password)
		{
			this.username = username;
			this.password = password;
		}
	}
}
