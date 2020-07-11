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
using Personnel;

namespace personnel
{
	/// <summary>
	/// LoginWindow.xaml 的交互逻辑
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
			string name = this.txtname.Text.Trim(); //Trim去头尾空格
			string password = this.txtpassword.Password.Trim();
			//输入内容验证
			if (name == "" || password == "")
			{
				MessageBox.Show("用户名或密码不能为空!");
				return;
			}
			Login loginer = null;
			LoginBLL bll = new LoginBLL();

			int flag = bll.Login(name, password, out loginer);
			
			

			switch (flag)
			{
				case -1:
					{
						MessageBox.Show("用户名或密码错误!");
						break;
					}
				case 0:
					{
						loginer.Password = null;
						CommonUser.LoginUser = loginer;
						new UserWindow().Show();
						this.Close();
						break;
					}
				case 1:
					{
						loginer.Password = null;
						CommonUser.LoginUser = loginer;
						new ManagerWindow().Show();
						this.Close();
						break;
					}
			}
		}
	}
}
