using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.Model
{
	/// <summary>
	/// 工资
	/// 2020/7/11 17：39 Salary表删除了Rp_id，Rp_date，Handler，Cancel_date，Cancel_reason字段
	/// </summary>
	public class Salary
	{
		//用户id
		public int Id { get; set; }
		//初始工资
		public int Pre_sal { get; set; }
		//实际工资
		public int Current_sal { get; set; }

		public Salary() { }

		public Salary(int Id, int Pre_sal, int Current_sal)
		{
			this.Id = Id;
			this.Pre_sal = Pre_sal;
			this.Current_sal = Current_sal;
		}
	}
}
