using personnel.BLL;
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
		const int maxnum = 12;
		UserBLL userBLL = new UserBLL();
		public ManagerWindow()
		{
			InitializeComponent();
			this.WindowStartupLocation = WindowStartupLocation;
			this.ResizeMode = ResizeMode.CanMinimize;
			ManagerBinding(maxnum, 1);
			pagename.Content = "用户管理";
		}


		//实现分页，maxnum是每页最大记录数,假定为12条记录 currentPage表示当前所在页数
		private void ManagerBinding(int maxnum, int currentpage)//管理员显示用户管理页面
		{
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
		private void return_button(object sender, RoutedEventArgs e)
		{
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage > 1)
			{
				ManagerBinding(maxnum, currentpage - 1);
			}
		}

		//点击返回最前页  如果已经在最前页就失效
		private void return_all_button(object sender, RoutedEventArgs e)
		{
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage > 1)
			{
				ManagerBinding(maxnum, 1);
			}
		}

		//点击进入下一页响应事件
		private void proceed_button(object sender, RoutedEventArgs e)
		{
			int endpage = int.Parse(tb_endpage.Text); //获取最大页数
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage < endpage)
			{
				ManagerBinding(maxnum, currentpage + 1);
			}
		}

		//点击跳转到最末页  如果已经在最末页就失效
		private void proceed_all_button(object sender, RoutedEventArgs e)
		{
			int endpage = int.Parse(tb_endpage.Text); //获取最大页数
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage < endpage)
			{
				ManagerBinding(maxnum, endpage);
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


		//跳转按钮
		private void jump_button(object sender, RoutedEventArgs e)
		{
			if (isInteger(tb_jumpage.Text) && (tb_jumpage.Text) != "")
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

		//点击奖罚管理按钮，动态改变dataGrid控件的列
		private void OnClickRPButton(object sender, RoutedEventArgs e)
		{
			RemoveFieldColumns();
			DataGridTextColumn dgtxtCol;
			string[] fields = { "奖罚编号", "奖罚名称", "奖金/罚金" ,
				"奖惩对象id","奖罚日期",
		"Rp_id","Rp_name","Sal","User_id","Rp_time"};
			int i = 0;
			for (; i < fields.Length / 2; i++)
			{
				dgtxtCol = new DataGridTextColumn();
				dgtxtCol.Width = 100;
			
				dgtxtCol.Header = fields[i];
				dgtxtCol.Binding = new Binding(fields[i + fields.Length / 2]);
				UserGrid.Columns.Insert(1 + i, dgtxtCol);
			}

			RPBLL rpBll = new RPBLL();
			UserGrid.ItemsSource = rpBll.GetAllRP();

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


		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		//条件查询
		private void search_button(object sender, RoutedEventArgs e)
		{

			if (txt_search.Text != "" && (combox_search.SelectedItem).ToString() != "")
			{
				UserBLL userBLL = new UserBLL();
				List<User> users = new List<User>();
				User user = null;
				//按员工ID查询 索引从0开始 但是第0项是空字符串
				if (combox_search.SelectedIndex == 1)
				{
					int id = int.Parse(txt_search.Text);
					user = userBLL.GetUserByID(id);
					if (user != null)
					{
						users.Add(user);
						UserGrid.ItemsSource = users;
					}
				}
				else
				{
					string name = txt_search.Text;
					users = userBLL.GetUserByName(name);
					if (users != null)
					{
						UserGrid.ItemsSource = users;
					}
				}
			}
			else
			{
				MessageBox.Show("查找方式和内容不能为空!");
			}
		}

		//删除一个用户记录
		private void bt_deleteonerecord(object sender, RoutedEventArgs e)
		{
			//获取当前行的对象
			User user = (User)UserGrid.SelectedItem;
			int Id = user.Id;
			if (MessageBox.Show("是否删除该用户的信息？", "Tips", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				userBLL.DeletUser(Id);
			}
			ManagerBinding(maxnum, 1);
		}


		List<int> selectedID = new List<int>();  //保存选中要进行批量删除的用户ID

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

			if (selectedID.Count != 0)
			{
				if (MessageBox.Show("是否删除所选中的员工信息?", "Tips", MessageBoxButton.YesNo) == MessageBoxResult.OK)
				{

					foreach (int i in selectedID)
					{
						userBLL.DeletUser(i); //循环遍历删除里面的记录
					}
				}
				else
				{
					selectedID.Clear();
				}
				ManagerBinding(maxnum, 1);     //刷新页面
			}
			else
				MessageBox.Show("请选择要删除的用户");
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
