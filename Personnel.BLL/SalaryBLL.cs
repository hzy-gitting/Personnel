﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using personnel.DAL;
using personnel.Model;
using Personnel.DAL;

namespace Personnel.BLL
{
    public class SalaryBLL
    {
        SalaryDAL salaryDAL = new SalaryDAL();
		/// <summary>
		/// 添加工资信息
		/// </summary>
		/// <param name="quit_date"></param>
		/// <param name="quit_reason"></param>
		/// <returns>添加成功返回true， 否则返回false</returns>
		public bool AddSalary(Salary salary)
		{
			return salaryDAL.Addsalary(salary) > 0;
		}
		/// <summary>
		/// 修改工资
		/// </summary>
		/// <param name="salary"></param>
		/// <returns>修改成功返回true， 否则返回false</returns>
		public bool Modifysalary(Salary salary)
		{
			return salaryDAL.ModifySalary(salary) > 0;
		}
		/// <summary>
		/// 根据ID获得用户奖惩信息
		/// </summary>
		/// <param name="salary"></param>
		/// <returns>添加成功返回true， 否则返回false</returns>
		public Salary GetsalaryByID(int id)
		{
			return salaryDAL.GetSalaryByID(id);
		}
		/// <summary>
		/// 获得所有工资
		/// </summary>
		/// <returns></returns>
		public List<Salary> GetAllSalary()
		{
			return salaryDAL.getAllSalary();
		}
	}
}
