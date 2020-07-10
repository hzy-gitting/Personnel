using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	public class Login
	{
		public int Uid { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		public Login() { }

		public Login(string username,string password)
		{
			this.Username = username;
			this.Password = password;
		}
	}
}
