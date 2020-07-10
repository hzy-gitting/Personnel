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
		public int Flag { get; set; }  //标识位,0为员工 1为管理员

		public Login() { }

		public Login(string username,string password,int flag)
		{
			this.Username = username;
			this.Password = password;
			this.Flag = flag;
		}
	}
}
