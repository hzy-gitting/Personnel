using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using personnel.BLL;
using personnel.DAL;
using personnel.Model;
using Personnel.DAL;

namespace Personnel.BLL
{
    public class SalaryBLL
    {
        SalaryDAL salaryDAL = new SalaryDAL();
		/// <summary>
		/// 添加员工工资信息
		/// </summary>
		/// <param name="quit_date"></param>
		/// <param name="quit_reason"></param>
		/// <returns>添加成功返回true， 否则返回false</returns>
		public bool AddSalary(Salary salary)
		{
			return salaryDAL.AddUserSalary(salary) > 0;
		}
		/// <summary>
		/// 修改工资
		/// </summary>
		/// <param name="salary"></param>
		/// <returns>修改成功返回true， 否则返回false</returns>
		public bool Modifysalary(Salary salary)
		{
			return salaryDAL.ModifyUserSalary(salary) > 0;
		}
		/// <summary>
		/// 根据ID获得用户工资信息
		/// </summary>
		/// <param name="salary"></param>
		/// <returns>添加成功返回true， 否则返回false</returns>
		public Salary GetsalaryByID(int id)
		{
			return salaryDAL.GetUserSalaryByID(id);
		}
		/// <summary>
		/// 获得所有工资
		/// </summary>
		/// <returns></returns>
		public List<Salary> GetAllSalary()
		{
			return salaryDAL.getAllSalary();
		}

		//统计用户工资信息数量
		public int GetUserSalaryNum()
		{
			return salaryDAL.GetUserSalaryNum();
		}

		/// <summary>
		/// 计算当前所有员工的最终工资并更新到数据库
		/// </summary>
		public void CalcAllUserCurrentSalaryAndUpdateDB()
		{
			RPBLL rpBll = new RPBLL();
			List<Salary> salaries = GetAllSalary();
			foreach (Salary sal in salaries)
			{
				UpdateCurrentSalaryById(sal.Id);
			}
		}
		/// <summary>
		/// 计算并修改指定员工id的最终工资
		/// 当某员工的奖金/罚金有变时应该调用此方法重新计算最终工资
		/// </summary>
		/// <param name="id"></param>
		public void UpdateCurrentSalaryById(int id)
		{
			Salary sal = GetsalaryByID(id);
			RPBLL rpBll = new RPBLL();
			List<RP> rps = rpBll.GetRPsByUserId(sal.Id);
			int sum = 0;
			if (rps != null)
			{
				foreach (RP rp in rps)
				{
					sum += rp.Sal;
				}
			}
			sal.Current_sal = sal.Pre_sal + sum;
			Modifysalary(sal);
		}
	}
}
