using personnel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Personnel;

namespace personnel.DAL
{
    public class LoginDAL
    {
        SqlDbHelper db = new SqlDbHelper();

        //通过ID查找登录用户信息
        public Login GetLoginById(int id)
        {
            //1 sql语句
            string sql = "SELECT * FROM Login WHERE uid=@id";
            MySqlParameter[] param = {new MySqlParameter("@id",MySqlDbType.Int32)};
            param[0].Value = id;
            //2 执行sql语句
            DataTable dt = db.ExecuteDataTable(sql, param);
            //3 数据库到实体类的映射
            Login login = null;
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                login = DataRowToLogin(dr);
            }
            return login;
        }
       
        //通过用户名查找对象
        public Login GetLoginByuserName(string Name)
        {
            string sql = "SELECT * FROM Login WHERE username=@username;";
            MySqlParameter[] param = {
                                        new MySqlParameter("@username",MySqlDbType.VarChar)
                                   };
            param[0].Value = Name;
            //2 执行sql语句
            DataTable dt = db.ExecuteDataTable(sql, param);
            Login login = null;
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                login = DataRowToLogin(dr);
            }
            return login;
        }

        //通过用户名和密码获取登录用户
        public Login GetLoginByuserameAndpassword(string name, string password)
        {
            string sql = "SELECT * FROM login WHERE username=@username AND password=@password";
            MySqlParameter[] p = {
                                        new MySqlParameter("@username",MySqlDbType.VarChar),
                                        new MySqlParameter("@password",MySqlDbType.VarChar)
                                   };
            p[0].Value = name;
            p[1].Value = password;
            DataTable dt = db.ExecuteDataTable(sql, p);
            Login login = null;
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0]; // 第1行记录
                login = DataRowToLogin(dr);
            }
            return login;
        }

       public Login GetLoginerByName(string name)
        {
                String sql = "select * from login where username=@username";
                MySqlParameter[] p = { new MySqlParameter("@username", MySqlDbType.VarChar) };
                p[0].Value = name;
                DataTable dt = db.ExecuteDataTable(sql, p);
            Login loginer= null;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                loginer = DataRowToLogin(dr);
            }
            return loginer;
        }

        //通过用户名查找对象 传入对象
        public int UpdateLogin(Login loginer)
        {
            string sql = "update login set password=@password where uid=@uid";
            MySqlParameter[] param = {
                new MySqlParameter("@password",MySqlDbType.VarChar),
                new MySqlParameter("@uid",MySqlDbType.Int32)
            };
            param[0].Value = loginer.Password;
            param[1].Value = loginer.Uid;
            return db.ExecuteNonQuery(sql, param);
        }

  
        private Login DataRowToLogin(DataRow dr)
        {
            Login login = new Login();
            login.Uid = Convert.ToInt32(dr["uid"]);
            login.Password = Convert.ToString(dr["password"]);
            login.Username = Convert.ToString(dr["username"]);
            login.Flag = Convert.ToInt32(dr["flag"]);
            return login;
        }

        private List<Login> DataTableToLogin(DataTable dt)
        {
            List<Login> users = new List<Login>();
            foreach (DataRow dr in dt.Rows)
            {
                users.Add(DataRowToLogin(dr));
            }
            return users;
        }
    }
}
