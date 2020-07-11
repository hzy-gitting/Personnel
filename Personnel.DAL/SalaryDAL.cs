using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using personnel.DAL;
using personnel.Model;
using System.Data;

namespace Personnel.DAL
{
    public class SalaryDAL
    {
        //从工具类获取数据库连接对象
        SqlDbHelper db = new SqlDbHelper();
        //查看所有用户工资信息
        //根据用户ID查询工资信息
        //根据用户姓名查询工资信息

        //增加一个用户 如果返回为1，说明插入成功
        public int Addsalary(Salary salary)
        {
            String sql = "insert into salary values(@id, @pre_sal,@rp,@rp_date,@handler,@cancel_date,@cancel_reason,@current_sal";
            //参数列表赋值 防止SQL注入
            MySqlParameter[] p =
            {
                new MySqlParameter("@id",MySqlDbType.Int32),
                new MySqlParameter("@pre_sal", MySqlDbType.Int32),
                new MySqlParameter("@rp",MySqlDbType.Int32),
                new MySqlParameter("@rp_date",MySqlDbType.DateTime),
                new MySqlParameter("@handler",MySqlDbType.Int32),
                new MySqlParameter("@cancel_date",MySqlDbType.DateTime),
                new MySqlParameter("@cancel_reason",MySqlDbType.VarChar),
                new MySqlParameter("@current_sal",MySqlDbType.Int32),
        };
            p[0].Value = salary.Id;
            p[1].Value = salary.Pre_sal;
            p[2].Value = salary.Rp_id;
            p[3].Value = salary.Rp_date;
            p[4].Value = salary.Handler;
            p[5].Value = salary.Cancel_date;
            p[6].Value = salary.Cancel_reason;
            p[7].Value = salary.Current_sal;

            return db.ExecuteNonQuery(sql, p);
        }
        /// <summary>
        /// 修改工资
        /// </summary>
        /// <param name="salary"></param>
        /// <returns></returns>
        public int ModifySalary(Salary salary)
        {

            String sql = "update Salary set pre_sal@pre_sal,rp=@rp,rp_data=@rp_data,handler=@handler,cancel_date=@cancel_date,cancel_reason=@cancel_reason,current_sal=@current_sal where id=@id";
            //参数列表赋值 防止SQL注入
            MySqlParameter[] p =
            {
                new MySqlParameter("@pre_sal", MySqlDbType.Int32),
                new MySqlParameter("@rp",MySqlDbType.Int32),
                new MySqlParameter("@rp_date",MySqlDbType.DateTime),
                new MySqlParameter("@handler",MySqlDbType.Int32),
                new MySqlParameter("@cancel_date",MySqlDbType.DateTime),
                new MySqlParameter("@cancel_reason",MySqlDbType.VarChar),
                new MySqlParameter("@current_sal",MySqlDbType.Int32),
        };
            p[0].Value = salary.Pre_sal;
            p[1].Value = salary.Rp_id;
            p[2].Value = salary.Rp_date;
            p[3].Value = salary.Handler;
            p[4].Value = salary.Cancel_date;
            p[5].Value = salary.Cancel_reason;
            p[6].Value = salary.Current_sal;
            return db.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 查询所有工资 （返回数组）
        /// </summary>
        /// <returns></returns>
        public List<Salary> getAllSalary()
        {
            //Salary salary = new Salary();
            String sql = "select * from Salary";
            //模拟数据库表 （相当于二维数组）
            DataTable dt = db.ExecuteDataTable(sql);
            List<Salary> salarys = new List<Salary>();
            //ORM  对象关系映射  把查询到的信息封装成对象
            foreach (DataRow dr in dt.Rows)
            {
                Salary salary = DataRowToSalary(dr);
                salarys.Add(salary);
            }
            return salarys;
        }

        /// <summary>
        /// 根据ID查找用户奖惩信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Salary GetSalaryByID(int id)
        {
            String sql = "select * from Salary where id=@id";
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
        /// 将行中数据对应到对象属性
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private Salary DataRowToSalary(DataRow dr)
        {
            Salary salary = new Salary();
            //数据库中数据格式和实体类中格式不同
            salary.Id = Convert.ToInt32(dr["id"]);
            salary.Pre_sal = Convert.ToInt32(dr["pre_sal"]);
            salary.Rp_id = Convert.ToInt32(dr["rp"]);
            salary.Rp_date = Convert.ToDateTime(dr["rp_date"]);
            salary.Handler = Convert.ToInt32(dr["handler"]);
            salary.Cancel_date = Convert.ToDateTime(dr["cancel_date"]);
            salary.Cancel_reason = Convert.ToString(dr["cancel_reason"]);
            return salary;
        }

    }
}
