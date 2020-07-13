using personnel.BLL;
using personnel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Personnel
{
    /// <summary>
    /// ModifyPassword.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyPassword : Window
    {
        LoginBLL loginBLL= new LoginBLL();
        Login newloginer = new Login();
        public ModifyPassword(Login loginer)
        {
            newloginer = loginer;
            InitializeComponent();
            text_Name.Text = loginer.Username;
            text_Password.Password = loginer.Password;
        }

        private void Modify(object sender, RoutedEventArgs e)
        {
            Login currentloginer = new Login();
            string name = this.text_Name.Text.Trim(); 
            string password = this.text_Password.Password.Trim();
            currentloginer=loginBLL.GetLoginByuserName(name);
            newloginer.Uid = currentloginer.Uid;//绑定Uid
            if (name == "" || password == "")
            {
                MessageBox.Show("请填写完整信息!");
                return;
            }
            if (loginBLL.GetLoginByuserName(name) == null||name==newloginer.Username)
            {
                loginBLL.UpdateLogin(newloginer);
                MessageBox.Show("修改成功");
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名已存在,请重新输入!");
                    return ;
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
