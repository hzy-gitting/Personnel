using MySql.Data.MySqlClient;
using System;
using Personnel.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.DAL
{
    public class SalaryDAL { 
    //从工具类获取数据库连接对象
    SqlDbHelper db = new SqlDbHelper();
    //查看所有用户工资信息
    public Salary GetAllSalary()
        {

        }
    //根据用户ID查询工资信息
    //根据用户姓名查询工资信息

    //增加一个用户 如果返回为1，说明插入成功
    public int Addsalary(Salary salary )
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
        p[0].Value = salary;
        p[1].Value = user.Folk;
        p[2].Value = user.Birthday;
        p[3].Value = user.Age;
        p[4].Value = user.Phone;
        p[5].Value = user.Work_Time;
        p[6].Value = user.Job;

        return db.ExecuteNonQuery(sql, p);
    }
    //删除一个用户,如果返回1,说明删除成功
    public int DeletUser(int id)
    {
        String sql = "delete from user where id=@id";
        MySqlParameter[] p = { new MySqlParameter("@id", MySqlDbType.Int32) };
        return db.ExecuteNonQuery(sql, p);
    }

    //修改一个用户 如果返回1，说明修改成功
    public int ModifyUser(User user)
    {

        String sql = "update user set name=@name,folk=@folk,birthday=@birthday,age=@age,phone=@phone,work_time=@work_time,job=@job where id=@id";
        //参数列表赋值 防止SQL注入
        MySqlParameter[] p =
        {
                new MySqlParameter("@name",MySqlDbType.VarChar),
                new MySqlParameter("@folk", MySqlDbType.VarChar),
                new MySqlParameter("@birthday", MySqlDbType.DateTime),
                new MySqlParameter("@age", MySqlDbType.Int32),
                new MySqlParameter("@phone", MySqlDbType.VarChar),
                new MySqlParameter("@work_time", MySqlDbType.Int32),
                new MySqlParameter("@job", MySqlDbType.VarChar),
                new MySqlParameter("@id",MySqlDbType.Int32)
        };
        p[0].Value = user.Name;
        p[1].Value = user.Folk;
        p[2].Value = user.Birthday;
        p[3].Value = user.Age;
        p[4].Value = user.Phone;
        p[5].Value = user.Work_Time;
        p[6].Value = user.Job;
        p[7].Value = user.Id;
        return db.ExecuteNonQuery(sql, p);
    }

    //查询所有用户 （返回数组）
    public List<User> getAllUsers()
    {
        String sql = "select * from user";
        //模拟数据库表 （相当于二维数组）
        DataTable dt = db.ExecuteDataTable(sql);
        List<User> users = new List<User>();
        //ORM  对象关系映射  把查询到的信息封装成对象
        foreach (DataRow dr in dt.Rows)
        {
            User user = DataRowToUser(dr);
            users.Add(user);
        }
        return users;
    }

    //根据ID查找用户（搜索按钮）如果返回非空，说明查找成功
    public User GetUserByID(int id)
    {
        String sql = "select * from user where id=@id";
        MySqlParameter[] p = { new MySqlParameter("@id", MySqlDbType.Int32) };
        p[0].Value = id;
        DataTable dt = db.ExecuteDataTable(sql, p);
        User user = null;
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            user = DataRowToUser(dr);
        }
        return user;
    }
    //根据姓名查找用户（搜索按钮）
    public User GetUserByName(int name)
    {
        String sql = "select * from user where name=@name";
        MySqlParameter[] p = { new MySqlParameter("@name", MySqlDbType.VarChar) };
        p[0].Value = name;
        DataTable dt = db.ExecuteDataTable(sql, p);
        User user = null;
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            user = DataRowToUser(dr);
        }
        return user;
    }
    //统计用户数量
    public int GetUserNum()
    {
        String sql = "select count(*) from user";
        DataTable dt = db.ExecuteDataTable(sql);
        DataRow dr = dt.Rows[0];
        int num = 0;
        num = Convert.ToInt32(dr["count(*)"]);
        return num;
    }

    //将行中数据对应到对象属性
    private User DataRowToUser(DataRow dr)
    {
        User user = new User();
        //数据库中数据格式和实体类中格式不同
        user.Id = Convert.ToInt32(dr["id"]);
        if (dr["birthday"] != DBNull.Value)//判断日期非空
        {
            user.Birthday = Convert.ToDateTime(dr["Birthday"]);
        }
        user.Name = Convert.ToString(dr["name"]);
        user.Folk = Convert.ToString(dr["folk"]);
        user.Job = Convert.ToString(dr["job"]);
        user.Age = Convert.ToInt32(dr["age"]);
        user.Phone = Convert.ToString(dr["phone"]);
        user.Work_Time = Convert.ToInt32(dr["work_time"]);
        return user;
    }

}
}
