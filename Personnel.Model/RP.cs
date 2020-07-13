using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	/// <summary>
	/// 奖罚类
	/// 2020/7/11 因业务需求，对RP表结构做出了修改，新增user_id（外键）和rp_time两个字段，
	/// 把原来的rp_id字段（主键）改名为no
	/// </summary>
	public class RP
	{
		public int No { get; set; }
		public string Rp_name { get; set; }
		public int Sal { get; set; }


		public int User_id { get; set; }

		public DateTime Rp_time { get; set; }

		public int Id
		{
			get
			{
				return No;
			}
		}
		public RP() { }

		public RP(int no, string rp_name, int sal, int User_id, DateTime Rp_time)
		{
			this.No = no;
			this.Rp_name = rp_name;
			this.Sal = sal;
			this.User_id = User_id;
			this.Rp_time = Rp_time;
		}
	}
}
