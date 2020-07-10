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
        LoginDAL loginDAL = new LoginDAL();
        public bool Login(string username, string password, out Login login)
        {
            //user = userDal.GetUserByLoginName(name, type);
            login = loginDAL.GetLoginByuserameAndpassword(username, password);

            return login != null;
        }
    }
}
