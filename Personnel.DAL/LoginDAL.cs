using personnel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace personnel.DAL
{
    public class LoginDAL
    {
        SqlDbHelper db = new SqlDbHelper();

        /// <summary>
        /// GetLoginById
        /// </summary>
        /// <param name="loginid"></param>
        /// <returns></returns>
        public Login GetLoginById(int id)
        {
            //1 sql语句
            string sql = "SELECT * FROM Login WHERE uid=@id";
            MySqlParameter[] param = {
                                        new MySqlParameter("@id",MySqlDbType.Int32)
                                   };
            param[0].Value = id;
            //2 执行sql语句
            DataTable dt = db.ExecuteDataTable(sql, param);
            //3 关系--》对象     orm --》 ef  nhibernate
            Login login = null;
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                login = DataRowToLogin(dr);
            }
            return login;
        }
        /// <summary>
        /// GetLoginByuserName
        /// </summary>
        /// <param name="loginname"></param>
        /// <returns></returns>
        public Login GetLoginByuserName(string Name)
        {
            //1 sql语句
            string sql = "SELECT * FROM Login WHERE name=@name;";
            MySqlParameter[] param = {
                                        new MySqlParameter("@name",MySqlDbType.VarChar)
                                   };
            param[0].Value = Name;
            //2 执行sql语句
            DataTable dt = db.ExecuteDataTable(sql, param);
            //3 关系--》对象     orm --》 ef  nhibernate
            Login login = null;
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                login = DataRowToLogin(dr);
            }
            return login;
        }

        /// <summary>
        /// GetLoginByuserameAndpassword
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Login GetLoginByuserameAndpassword(string name, string password)
        {
            //1 sql语句
            string sql = "SELECT * FROM login WHERE username=@name AND password=@password";
            MySqlParameter[] @params = {
                                        new MySqlParameter("@name",MySqlDbType.VarChar),
                                        new MySqlParameter("@password",MySqlDbType.VarChar)
                                   };
            @params[0].Value = name;
            @params[1].Value = password;
            //2 执行sql语句
            DataTable dt = db.ExecuteDataTable(sql, @params);
            //3 关系--》对象     orm --》 ef  nhibernate
            Login login = null;
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0]; // 第1行记录
                login = DataRowToLogin(dr);
            }
            return login;
        }

        /// <summary>
        /// 把行转化成对象
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="type">table 表   view视图</param>
        /// <returns></returns>
        private Login DataRowToLogin(DataRow dr)
        {
            Login login = new Login();

            //user.Id = Convert.ToInt32(dr[0]);
            login.uid = Convert.ToInt32(dr["uid"]);
            login.password = Convert.ToString(dr["password"]);
            login.username = Convert.ToString(dr["username"]);
            return login;
        }
    }
}
