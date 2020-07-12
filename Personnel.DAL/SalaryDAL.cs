using MySql.Data.MySqlClient;
using System;
using Personnel.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using personnel.Model;

namespace Personnel.DAL
{
	public class SalaryDAL
	{
		//从工具类获取数据库连接对象
		SqlDbHelper db = new SqlDbHelper();

		//增加一个用户工资信息 如果返回为1，说明插入成功
		public int AddUserSalary(Salary salary)
		{
			String sql = "insert into salary (id,pre_sal,current_sal) " +
				" values(@id,@pre_sal,@current_sal)";
			//参数列表赋值 防止SQL注入
			MySqlParameter[] p =
			{
				new MySqlParameter ("@id",MySqlDbType.Int32),
				new MySqlParameter("@pre_sal",MySqlDbType.Int32),
				new MySqlParameter("@current_sal", MySqlDbType.Int32)
			};
			p[0].Value = salary.Id;
			p[1].Value = salary.Pre_sal;
			p[2].Value = salary.Current_sal;

			return db.ExecuteNonQuery(sql, p);
		}
		//删除一个用户工资信息,如果返回1,说明删除成功
		public int DeletUserSalary(int id)
		{
			String sql = "delete from salary where id=@id";
			MySqlParameter[] p = { new MySqlParameter("@id", MySqlDbType.Int32) };
			return db.ExecuteNonQuery(sql, p);
		}

		//修改一个用户工资 如果返回1，说明修改成功
		public int ModifyUserSalary(Salary salary)
		{
			String sql = "update salary set pre_sal=@pre_sal ," +
				"current_sal=@current_sal where id=@id";
			//参数列表赋值 防止SQL注入
			MySqlParameter[] p ={
				new MySqlParameter("@pre_sal",MySqlDbType.Int32),
				new MySqlParameter("@current_sal", MySqlDbType.Int32),
				new MySqlParameter("@id",MySqlDbType.Int32)
			};
			p[0].Value = salary.Pre_sal;
			p[1].Value = salary.Current_sal;
			p[2].Value = salary.Id;
			return db.ExecuteNonQuery(sql, p);
		}

		//查询所有用户工资信息 （返回数组）
		public List<Salary> getAllSalary()
		{
			String sql = "select * from salary";
			//模拟数据库表 （相当于二维数组）
			DataTable dt = db.ExecuteDataTable(sql);
			List<Salary> salaries = new List<Salary>();
			//ORM  对象关系映射  把查询到的信息封装成对象
			foreach (DataRow dr in dt.Rows)
			{
				Salary salary = DataRowToSalary(dr);
				salaries.Add(salary);
			}
			return salaries;
		}

		//根据ID查找用户工资信息（搜索按钮）如果返回非空，说明查找成功
		public Salary GetUserSalaryByID(int id)
		{
			String sql = "select * from salary where id=@id";
			MySqlParameter[] p = { new MySqlParameter("@id", MySqlDbType.Int32) };
			p[0].Value = id;
			DataTable dt = db.ExecuteDataTable(sql, p);
			Salary salary = null;
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				salary = DataRowToSalary(dr);
			}
			return salary;
		}
		/// <summary>
		/// 删除一个员工工资信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public int DeletSalary(int Id)
		{
			String sql = "delete from salary where id=@id";
			MySqlParameter[] p = { new MySqlParameter("@id", MySqlDbType.Int32) };
			p[0].Value = Id;
			return db.ExecuteNonQuery(sql, p);
		}


		//统计用户工资信息数量
		public int GetUserSalaryNum()
		{
			String sql = "select count(*) from salary";
			DataTable dt = db.ExecuteDataTable(sql);
			DataRow dr = dt.Rows[0];
			int num = 0;
			num = Convert.ToInt32(dr["count(*)"]);
			return num;
		}

		//数据库中数据格式和实体类中格式不同,所以需要将行中数据对应到对象属性
		private Salary DataRowToSalary(DataRow dr)
		{
			Salary salary = new Salary();
			//所有字段都设置了非空约束，所以这里不再判空
			salary.Id = Convert.ToInt32(dr["id"]);
			salary.Pre_sal = Convert.ToInt32(dr["pre_sal"]);
			salary.Current_sal = Convert.ToInt32(dr["current_sal"]);
			return salary;
		}
		

	}
}
