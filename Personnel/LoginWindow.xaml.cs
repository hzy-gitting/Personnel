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
using System.Windows.Navigation;
using System.Windows.Shapes;
using personnel.BLL;
using personnel.Model;

namespace personnel
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //接收用户输入数据
            string name = this.txtname.Text.Trim();
            string password = this.txtpassword.Password.Trim();
            //int type = (int)this.cbType.SelectedValue;
            if (name == "" || password == "")
            {
                // 用户名密码为空
                MessageBox.Show("用户名或密码不能为空!");
                return;
            }
            Login login = null;
            LoginBLL bll = new LoginBLL();

            bool flag = bll.Login(name, password, out login);

            if (flag)
            {
                //不把密码保存在内存中
                login.password = null;
                //登录成功记录登录者的信息
                Common.LoginUser = login;
                //登录成功
                //MessageBox.Show("登录成功!");
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                //登录失败
                MessageBox.Show("用户名或密码错误!");
            }
        }
    }
}
