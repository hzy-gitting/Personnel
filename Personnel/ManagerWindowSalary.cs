using personnel.BLL;
using personnel.Model;
using Personnel.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Personnel
{
	public partial class ManagerWindow : Window
	{
		SalaryBLL SalaryBLL = new SalaryBLL();
		List<Salary> salaryList = new List<Salary>();
		static int sum;
		//Salary分页
		//实现分页，maxnum是每页最大记录数,假定为12条记录 currentPage表示当前所在页数
		private void SalaryBinding(int maxnum, int currentpage)//管理员显示工资管理页面
		{
			int sum = salaryList.Count;
			// number表示每个页面显示的记录数 currentSize表示当前显示页数
			if (stateflag == 3)
			{
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
				UserGrid.ItemsSource = salaryList.Skip((currentpage - 1) * maxnum).Take(maxnum).ToList();
			}
		}
		/// <summary>
		/// 点击返回上一页响应事件
		/// </summary>
		private void return_buttonSalary()
		{
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage > 1)
			{
				SalaryBinding(maxnum, currentpage - 1);
			}
		}
		/// <summary>
		/// 点击返回最前页  如果已经在最前页就失效
		/// </summary>
		private void return_all_buttonSalary()
		{
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage > 1)
			{
				//ManagerBinding(maxnum, 1);
				SalaryBinding(maxnum, 1);
			}
		}
		/// <summary>
		/// 点击进入下一页响应事件
		/// </summary>
		private void proceed_buttonSalary()
		{
			int endpage = int.Parse(tb_endpage.Text); //获取最大页数
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage < endpage)
			{
				//ManagerBinding(maxnum, currentpage + 1);
				SalaryBinding(maxnum, currentpage + 1);
			}
		}
		/// <summary>
		/// 点击跳转到最末页  如果已经在最末页就失效
		/// </summary>
		private void proceed_all_buttonSalary()
		{
			int endpage = int.Parse(tb_endpage.Text); //获取最大页数
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage < endpage)
			{
				SalaryBinding(maxnum, endpage);
			}
		}
		/// <summary>
		/// 跳转按钮
		/// </summary>
		private void jump_buttonSalary()
		{
			if ((tb_jumpage.Text) != "" && isInteger(tb_jumpage.Text))
			{
				int jumpage = int.Parse(tb_jumpage.Text);
				int endpage = int.Parse(tb_endpage.Text);
				if (jumpage > 0 && jumpage <= endpage)
				{
					//ManagerBinding(maxnum, jumpage);
					SalaryBinding(maxnum, jumpage);
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
		/// <summary>
		/// 条件查询
		/// </summary>
		private void search_buttonSalary()
		{
			if (combox_search.SelectedItem == null)
			{
				MessageBox.Show("请先选择查询条件");
				return;
			}
			if (txt_search.Text != "" && (combox_search.SelectedItem).ToString() != "")
			{
				SalaryBLL salaryBLL = new SalaryBLL();
				Salary salary = null;
				salaryList.Clear();
				if (combox_search.SelectedIndex == 1)
				{
					if (!isInteger(txt_search.Text))
					{
						MessageBox.Show("请输入数字");
						return;
					}
					int id = int.Parse(txt_search.Text);
					salary = salaryBLL.GetsalaryByID(id);
					if (salary != null)
					{
						salaryList.Add(salary);
						UserGrid.ItemsSource = salaryList;
					}
				}
				sum = salaryList.Count;
				SalaryBinding(maxnum, 1);
				txt_search.Text = "";

			}
			else
			{
				MessageBox.Show("查找方式和内容不能为空!");
			}
		}
		/// <summary>
		/// 删除一个用户记录
		/// </summary>
		private void bt_deleteonerecordSalary()
		{
			Salary salary = (Salary)UserGrid.SelectedItem;
			int Id = salary.Id;
			if (MessageBox.Show("是否删除该用户的信息？", "Tips", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				SalaryBLL salaryBLL = new SalaryBLL();
				salaryBLL.DeletSalary(Id);
			}
			salaryList = SalaryBLL.GetAllSalary();
			SalaryBinding(maxnum, 1);
		}
		/// <summary>
		/// 批量删除用户记录
		/// </summary>
		private void bt_deletemanySalary()
		{
			if (selectedID.Count != 0)
			{
				if (MessageBox.Show("是否删除所选中的员工信息?", "Tips", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{

					foreach (int i in selectedID)
					{
						SalaryBLL salaryBLL = new SalaryBLL();
						salaryBLL.DeletSalary(i); //循环遍历删除里面的记录
					}
				}
				salaryList = SalaryBLL.GetAllSalary();
				SalaryBinding(maxnum, 1);     //刷新页面
			}
			else
				MessageBox.Show("请选择要删除的用户");
		}
		/// <summary>
		/// 工资管理模块
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SalaryButton_Click(object sender, RoutedEventArgs e)
		{
			stateflag = 3;
			RemoveFieldColumns();
			pagename.Content = "工资管理";
			DataGridTextColumn dgtxtCol;
			string[] fields = { "id", "基本工资", "最终工资" ,
		"Id","Pre_sal","Current_sal"};
			for (int i = 0; i < 3; i++)
			{
				dgtxtCol = new DataGridTextColumn();
				dgtxtCol.Header = fields[i];
				dgtxtCol.Binding = new Binding(fields[i + 3]);
				UserGrid.Columns.Insert(1 + i, dgtxtCol);
			}

			//List<Salary> salarys = new List<Salary>();
			salaryList.Clear();
			Salary salary = new Salary();
			SalaryBLL salaryBLL = new SalaryBLL();
			List<Salary> salary1 = salaryBLL.GetAllSalary();
			int id = 0;
			foreach (Salary item in salary1)
			{
				id = item.Id;
				salary = salaryBLL.GetsalaryByID(id);
				RP rP = new RP();
				RPBLL rpBll = new RPBLL();
				List<RP> rps = rpBll.GetRPsByUserId(id);
				int sum = 0;
				if (rps != null)
				{
					foreach (RP item1 in rps)
					{
						sum += item1.Sal;
					}
					salary.Current_sal = salary.Pre_sal + sum;
					salaryBLL.Modifysalary(salary);
				}
				Salary salary2 = new Salary();
				salary2 = salaryBLL.GetsalaryByID(id);
				if (salary2 != null)
				{
					salaryList.Add(salary2);
				}
			}
			UserGrid.ItemsSource = salaryList;
			sum = salaryList.Count;//sum是记录总数
			SalaryBinding(maxnum, 1);
		}
		/// <summary>
		/// 新增工资函数
		/// </summary>
		private void ADDButton_ClickSalary()
		{
			new AddSalary().ShowDialog();
			salaryList = SalaryBLL.GetAllSalary();
			SalaryBinding(maxnum, 1);
		}
		/// <summary>
		/// 工资修改函数
		/// </summary>
		private void bt_modify_ClickSalary()
		{
			Salary salary = (Salary)UserGrid.SelectedItem;
			ModifySalary modi = new ModifySalary(salary);
			modi.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			//修改用户界面悬浮在当前页面上
			modi.Owner = this;
			modi.ShowDialog();
			salaryList = SalaryBLL.GetAllSalary(); 
			//重新刷新界面
			SalaryBinding(maxnum, 1);
		}
	}
}
