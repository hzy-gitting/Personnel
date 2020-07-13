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
		Login loginer = new Login();

		public ManagerWindow(Login loginer)
		{
			//如何实现登录拦截?
			if (loginer == null)
			{
				MessageBox.Show("请先登录!");
				new LoginWindow().Show();
			}
			else
			{
				InitializeComponent();
				this.WindowStartupLocation = WindowStartupLocation;
				this.ResizeMode = ResizeMode.CanMinimize;
				ManagerBinding(maxnum, 1);
				pagename.Content = "用户管理";
			}
		}

		//点击切换到用户管理
		private void OnClickUserManageButton(object sender, RoutedEventArgs e)
		{
			pagename.Content = "用户管理";
			stateflag = 1;
			RemoveFieldColumns();
			DataGridTextColumn dgtxtCol;
			string[] fields = { "ID", "姓名", "民族" ,"生日","年龄","联系方式","工种",

		"Id","Name","Folk","Birthday","Age","Phone","Job"};
			for (int i = 0; i < 7; i++)
			{
				dgtxtCol = new DataGridTextColumn();
				dgtxtCol.Header = fields[i];
				dgtxtCol.Binding = new Binding(fields[i + 7]);
				UserGrid.Columns.Insert(1 + i, dgtxtCol);
			}
			ManagerBinding(maxnum, 1);
		}

		//实现分页，maxnum是每页最大记录数,假定为12条记录 currentPage表示当前所在页数
		private void ManagerBinding(int maxnum, int currentpage)//管理员显示用户管理页面
		{
			UserBLL userBLL = new UserBLL();
			// number表示每个页面显示的记录数 currentSize表示当前显示页数
			List<User> userList = userBLL.getAllUsers();
			int sum = userList.Count;//sum是记录总数
			sumlabel.Content = sum; //把记录数显示到标签上
			int pagesize = 0;
			if (sum % maxnum == 0)//刚好可以填满
			{
				pagesize = sum / maxnum;
			}
			else
			{
				pagesize = (sum / maxnum) + 1;
			}
			tb_currentpage.Text = currentpage.ToString(); //显示当前页
			tb_endpage.Text = pagesize.ToString(); //显示总页数
			UserGrid.ItemsSource = userList.Skip((currentpage - 1) * maxnum).Take(maxnum).ToList();
		}

		//点击返回上一页响应事件
		private void return_button_user()
		{
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage > 1)
			{
				ManagerBinding(maxnum, currentpage - 1);
			}
		}

		//点击返回最前页  如果已经在最前页就失效
		private void return_all_button_user()
		{
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage > 1)
			{
				ManagerBinding(maxnum, 1);
			}
		}

		//点击进入下一页响应事件
		private void proceed_button_user()
		{
			int endpage = int.Parse(tb_endpage.Text); //获取最大页数
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage < endpage)
			{
				ManagerBinding(maxnum, currentpage + 1);
			}
		}

		//点击跳转到最末页  如果已经在最末页就失效
		private void proceed_all_button_user()
		{
			int endpage = int.Parse(tb_endpage.Text); //获取最大页数
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage < endpage)
			{
				ManagerBinding(maxnum, endpage);
			}
		}

		//跳转按钮
		private void jump_button_user()
		{
			if ((tb_jumpage.Text) != "" && isInteger(tb_jumpage.Text))
			{
				int jumpage = int.Parse(tb_jumpage.Text);
				int endpage = int.Parse(tb_endpage.Text);
				if (jumpage > 0 && jumpage <= endpage)
				{
					ManagerBinding(maxnum, jumpage);
				}
				else
				{
					MessageBox.Show("跳转页数不在查询范围内!");
					return;
				}
				//清空格子中的数,防止保留在页面上影响下次操作

			}
			else
			{
				MessageBox.Show("请输入有效数字!");
			}
			tb_jumpage.Text = "";
		}


		/*-------------------增加用户---------------------------*/

		private void add_button_user()
		{
			WindowAddUser add = new WindowAddUser();
			add.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			//添加用户界面悬浮在当前页面上
			add.Owner = this;
			add.ShowDialog();
			//重新刷新界面
			ManagerBinding(maxnum, 1);
		}

		/*-------------------删除---------------------------------*/

		//删除一个用户记录
		private void bt_deleteonerecord_user()
		{
			//获取当前行的对象
			User user = (User)UserGrid.SelectedItem;
			int Id = user.Id;
			if (MessageBox.Show("是否删除该用户的信息？", "Tips", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				userBLL.DeletUser(Id);
				MessageBox.Show("删除成功!");
			}
			ManagerBinding(maxnum, 1);
		}

		//批量删除用户记录
		private void bt_deletemany_user()
		{
			if (selectedID.Count != 0)
			{
				if (MessageBox.Show("是否删除所选中的员工信息?", "Tips", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					foreach (int i in selectedID)
					{
						userBLL.DeletUser(i); //循环遍历删除里面的记录
					}
					MessageBox.Show("删除成功！");
				}
				else
				{
					selectedID.Clear();
				}
				ManagerBinding(maxnum, 1);     //刷新页面
			}
			else
			{
				MessageBox.Show("请选择要删除的用户");
			}
		}


		/*--------------------------修改---------------------------------*/
		private void usermodify_button_user()
		{
			//问题:怎么将当前窗口的数据传给修改用户界面？ 通过构造函数传入？
			User user = (User)UserGrid.SelectedItem;
			ModifyUser modi = new ModifyUser(user);
			modi.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			//修改用户界面悬浮在当前页面上
			modi.Owner = this;
			modi.ShowDialog();
			//重新刷新界面
			ManagerBinding(maxnum, 1);
		}

		/*--------------------------条件查询---------------------------------*/

		private void search_button_user()
		{
			if (combox_search.SelectedItem == null)
			{
				MessageBox.Show("请先选择查询条件");
				return;
			}
			if (txt_search.Text != "" && (combox_search.SelectedItem).ToString() != "")
			{
				UserBLL userBLL = new UserBLL();
				List<User> users = new List<User>();
				User user = null;
				//按员工ID查询 索引从0开始 但是第0项是空字符串
				if (combox_search.SelectedIndex == 1)
				{
					if (!isInteger(txt_search.Text))
					{
						MessageBox.Show("请输入数字");
						return;
					}
					int id = int.Parse(txt_search.Text);
					user = userBLL.GetUserByID(id);
					if (user != null)
					{
						users.Add(user);
					}
				}
				else
				{
					string name = txt_search.Text;
					users = userBLL.GetUserByName(name);
				}
				UserGrid.ItemsSource = users;
				txt_search.Text = "";

				sumlabel.Content = (users == null ?
					0 :
					users.Count);
			}
			else
			{
				MessageBox.Show("查找方式和内容不能为空!");
			}
		}

		//管理员修改用户名 密码
		private void modify_password_user()
		{
			ModifyPassword modifpassword = new ModifyPassword(loginer);
			modifpassword.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			modifpassword.Owner = this;
			modifpassword.ShowDialog();
		}

		//注销，返回登录界面
		private void user_exit_user()
		{
			new LoginWindow().Show();
		}
	}
}
