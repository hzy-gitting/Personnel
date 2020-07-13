using personnel.DAL;
using personnel.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personnel.BLL
{
    public class LoginBLL
    {
        LoginDAL logindal = new LoginDAL();
        
        //返回值有三种情况 1.用户名不存在或密码错误返回-1  2.身份是员工返回0  3.身份是管理员返回1
        public int Login(string username, string password,out Login loginer)
        {
            //这里用两种判断方式 此处选择查询用户和密码，如果查询到了返回真
            loginer = logindal.GetLoginByuserameAndpassword(username, password);
            if (loginer == null) { return -1; }
            return loginer.Flag;  //标志位复用
        }


        public Login GetLoginByuserName(string Name)
        {
            Login login = new Login();
            login = logindal.GetLoginByuserName(Name);
            return login;
        }

        public int UpdateLogin(Login loginer)
        {
            return logindal.UpdateLogin(loginer);
        }
    }
}
