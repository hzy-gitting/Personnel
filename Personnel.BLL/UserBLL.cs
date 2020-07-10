using Personnel.DAL;
using Personnel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.BLL
{
    public class UserBLL
    {
        UserDAL userdal = new UserDAL();
        //增加一个用户 如果返回为1，说明插入成功
        public int AddUser(User user)
        {
            return userdal.AddUser(user);
        }
        //删除一个用户,如果返回1,说明删除成功
        public int DeletUser(int id)
        {
            return userdal.DeletUser(id);
        }

        //修改一个用户 如果返回1，说明修改成功
        public int ModifyUser(User user)
        {
            return userdal.ModifyUser(user);
        }

        //查询所有用户 （返回数组）
        public List<User> getAllUsers()
        {
            return userdal.getAllUsers();
        }

        //根据ID查找用户（搜索按钮）如果返回非空，说明查找成功
        public User GetUserByID(int id)
        {
            return userdal.GetUserByID(id);
        }
        //根据姓名查找用户（搜索按钮）
        public User GetUserByName(int name)
        {
            return userdal.GetUserByName(name);
        }
        //统计用户数量
        public int GetUserNum()
        {
            return userdal.GetUserNum();
        }

    }
}
