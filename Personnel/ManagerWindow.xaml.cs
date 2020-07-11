using personnel.BLL;
using Personnel.BLL;
using Personnel.DAL;
using Personnel.Model;
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
	/// ManagerWindow.xaml 的交互逻辑
	/// </summary>
	public partial class ManagerWindow : Window
	{
		public ManagerWindow()
		{
			InitializeComponent();
			UserBinding();
		}

		private void UserBinding()//用户显示页面
		{
			UserBLL userBLL = new UserBLL();
			// number表示每个页面显示的记录数 currentSize表示当前显示页数
			List<User> userList = new List<User>();
			userList = userBLL.getAllUsers();    //获取数据源  
							     //int count = userList.Count;         //获取记录总数  
							     ////label.Content = count;
							     //int pageSize = 0;            //pageSize表示总页数  
							     //if (count % number == 0)
							     //{
							     //    pageSize = count / number;
							     //}
							     //else
							     //{
							     //    pageSize = count / number + 1;
							     //}
							     ////tbkUserTotal.Text = pageSize.ToString();

			////tbkUserCurrentsize.Text = currentSize.ToString();
			//userList = userList.Take(number * currentSize).Skip(number * (currentSize - 1)).ToList();   //刷选第currentSize页要显示的记录集  
			UserGrid.ItemsSource = userList;        //重新绑定UserGrid 
			
		}


		private void top_button1_Click(object sender, RoutedEventArgs e)
		{

		}

		private void top_button2_Click(object sender, RoutedEventArgs e)
		{

		}

		/// <summary>
		/// 点击奖罚管理按钮
		/// </summary>
		private void OnClickRPButton(object sender, RoutedEventArgs e)
		{
			RemoveFieldColumns();
			DataGridTextColumn dgtxtCol;
			string[] fields = { "奖罚编号", "奖罚名称", "奖金/罚金" ,
				"Rp_id","Rp_name","Sal"};
			for (int i = 0; i < 3; i++)
			{
				dgtxtCol = new DataGridTextColumn();
				dgtxtCol.Header = fields[i];
				dgtxtCol.Binding = new Binding(fields[i + 3]);
				UserGrid.Columns.Insert(1+i, dgtxtCol);
			}
			RPBLL rpBll = new RPBLL();
			UserGrid.ItemsSource = rpBll.GetAllRP();
		}

		/// <summary>
		/// 删除字段列
		/// </summary>
		private void RemoveFieldColumns()
		{
			int count = UserGrid.Columns.Count();
			//除第一列（checkbox）和最后一列（操作列）外，全部删除
			for (int i = 1; i < count - 1; i++)
			{
				UserGrid.Columns.RemoveAt(1);
			}
		}

	}
}
