using personnel;
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
        public ModifyPassword()
        {
            InitializeComponent();
            text_Name.Text = CommonUser.LoginUser.Username;
        }

        private void Modify(object sender, RoutedEventArgs e)
        {
            string password = this.text_Password.Password.Trim();
            if (password == "")
            {
                MessageBox.Show("请填写完整信息!");
                return;
            }
            newloginer = CommonUser.LoginUser;
            newloginer.Password = password;
                loginBLL.UpdateLogin(newloginer);
                MessageBox.Show("修改成功!");
                this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
