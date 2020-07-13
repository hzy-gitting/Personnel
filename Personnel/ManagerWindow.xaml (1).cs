using personnel;
using personnel.BLL;
using personnel.Model;
using Personnel.BLL;
using Personnel.DAL;
using Personnel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	/// ManagerWindow.xaml 的交互逻辑
	/// </summary>
	public partial class ManagerWindow : Window
	{
		static int stateflag = 0;
		const int maxnum = 12;
		UserBLL userBLL = new UserBLL();

		List<int> selectedID = new List<int>();  //保存选中要进行批量删除的用户ID

		public ManagerWindow()
		{
			InitializeComponent();
			this.WindowStartupLocation = WindowStartupLocation;
			this.ResizeMode = ResizeMode.CanMinimize;
			//管理员登录系统后默认进入用户管理
			stateflag = 1;
			ManagerBinding(maxnum, 1);
			pagename.Content = "用户管理";
		}

		//点击返回上一页响应事件
		private void return_button(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1: return_button_user();break;
				case 2: return_button_RP(); break;
				case 3: return_buttonSalary();break;
			}
		}


		//点击返回最前页  如果已经在最前页就失效
		private void return_all_button(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1: return_all_button_user(); break;
				case 2: return_all_button_RP(); break;
				case 3: return_all_buttonSalary();  break;
			}
		}


		//点击进入下一页响应事件
		private void proceed_button(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1: proceed_button_user(); break;
				case 2: proceed_button_RP(); break;
				case 3: proceed_buttonSalary(); break;
			}
		}

		

		//点击跳转到最末页  如果已经在最末页就失效
		private void proceed_all_button(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1: proceed_all_button_user(); break;
				case 2: proceed_all_button_RP(); break;
				case 3: proceed_all_buttonSalary(); break;
			}
		}

		//跳转按钮
		private void jump_button(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1: jump_button_user(); break;
				case 2: jump_button_RP();  break;
				case 3: jump_buttonSalary(); break;
			}
		}

		/*函数isInteger(string s)获取用户输入的值
		   1.判断合法性
		   正则表达式判断输入是否为整数
		       \d 匹配数字（0-9 数字）
		       例子：判断输入的字符串是否符合\d这个正则表达式
	                    string s = Console.ReadLine();
	                    string pattern =@"^\d*$";
	                    bool isMatch = Regex.IsMatch(s, pattern);
		       说明：^\d 表示以数字为开头， *表示开头后面的n个字符，
	                      $表示以\d结尾，因为$会搜索最近的元字符来使用，所以不用再输入一次\d
	                  因为这个正则表达式中有 \ ，所以要在最开始前加 @ 表示这个 \没有转移符的意思
	                  使用Regex.IsMatch方法判断s的条件是否与正则表达式的条件成立
		   */
		public static bool isInteger(string s)
		{
			string pattern = @"^\d*$";
			return Regex.IsMatch(s, pattern);
		}


		/// 删除字段列
		private void RemoveFieldColumns()
		{
			int count = UserGrid.Columns.Count();
			//除第一列（checkbox）和最后一列（操作列）外，全部删除
			for (int i = 1; i < count - 1; i++)
			{
				UserGrid.Columns.RemoveAt(1);
			}
		}

		///条件查询
		private void search_button(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1:search_button_user();break;
				case 2:SearchRP();break;
				case 3:search_buttonSalary();break;
				default: break;
			}

		}

		//删除一个用户记录
		private void bt_deleteonerecord(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1:bt_deleteonerecord_user();break;
				case 2: DeleteOneRP(); break; 
				case 3:bt_deleteonerecordSalary();break;
				default: break;
			}
		}

		private void cb_choose(object sender, RoutedEventArgs e)
		{
			CheckBox cb = sender as CheckBox;
			int ID = (int)(cb.Tag);
			if (cb.IsChecked == true)
			{
				selectedID.Add(ID);
			}
			else
			{
				selectedID.Remove(ID);
			}
		}

		//批量删除用户记录
		private void bt_deletemany(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1:bt_deletemany_user();break;
				case 2:DeleteSelectedRPs(); break; 
				case 3:bt_deletemanySalary();break;
				default: break;
			}
		}
		private void ADDButton_Click(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1:add_button_user();break;
				case 2:AddOneRP();break;
				case 3:ADDButton_ClickSalary();break;
				default: break;
			}
		}

		private void bt_modify_Click(object sender, RoutedEventArgs e)
		{
			switch (stateflag)
			{
				case 1:usermodify_button_user(); break;
				case 2: UpdateOneRP(); break; 
				case 3:bt_modify_ClickSalary();break;
				default: break;
			}
		}


		//管理员修改用户名 密码
		private void modify_password(object sender, RoutedEventArgs e)
		{
			ModifyPassword modifpassword = new ModifyPassword();
			modifpassword.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			modifpassword.Owner = this;
			modifpassword.ShowDialog();
		}

		//注销，返回登录界面
		private void user_exit(object sender, RoutedEventArgs e)
		{
			LoginWindow backlogin = new LoginWindow();
			backlogin.Show();
			backlogin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			this.Close();
		}
	}
}
