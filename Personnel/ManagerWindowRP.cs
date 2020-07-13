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

namespace Personnel
{
	/// <summary>
	/// ManagerWindow类的奖罚管理分部
	/// </summary>
	public partial class ManagerWindow : Window
	{
		RPBLL rpBll = new RPBLL();

		List<RP> m_rps = new List<RP>();

		/// <summary>
		/// 点击奖罚管理按钮，在UserGrid中显示所有奖罚记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RPButton_Click(object sender, RoutedEventArgs e)
		{
			pagename.Content = "奖罚管理";
			stateflag = 2;
			RemoveFieldColumns();
			DataGridTextColumn dgtxtCol;
			string[] fields = { "奖惩编号", "用户id", "奖罚名称","奖罚金","处理时间" ,
		"No","User_id","Rp_name","Sal","Rp_time"};
			for (int i = 0; i < fields.Length / 2; i++)
			{
				dgtxtCol = new DataGridTextColumn();
				dgtxtCol.Header = fields[i];
				dgtxtCol.Binding = new Binding(fields[i + fields.Length / 2]);
				UserGrid.Columns.Insert(1 + i, dgtxtCol);
			}
			m_rps = rpBll.GetAllRP(); 
			ManagerBindingRP(rpBll.GetAllRP(), maxnum, 1);
			
		}

		/// <summary>
		/// 删除一条奖罚记录
		/// hzy
		/// </summary>
		private void DeleteOneRP()
		{
			RP rp = (RP)UserGrid.SelectedItem;
			RPBLL rpBll = new RPBLL();
			if (!rpBll.DeleteRPByNo(rp.No))
			{
				MessageBox.Show("删除奖罚记录失败", "人事工资管理系统");
			}
			m_rps = rpBll.GetAllRP(); 
			ManagerBindingRP(rpBll.GetAllRP(), maxnum, 1);
		}

		/// <summary>
		/// 批量删除奖罚记录
		/// hzy
		/// </summary>
		private void DeleteSelectedRPs()
		{
			if (selectedID.Count != 0)
			{
				if (MessageBox.Show("是否删除所选中的奖罚记录?", "Tips", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					foreach (int i in selectedID)
					{
						if (!rpBll.DeleteRPByNo(i))
						{
							MessageBox.Show(
								"删除No为" + i + "的奖罚记录失败");
						}
					}
				}
				m_rps = rpBll.GetAllRP();
				ManagerBindingRP(rpBll.GetAllRP(), maxnum, 1);
			}
			else
			{
				MessageBox.Show("请选择要删除的奖罚记录");
			}
		}

		/// <summary>
		/// 修改一条奖罚记录
		/// hzy
		/// </summary>
		private void UpdateOneRP()
		{
			RP rp = (RP)UserGrid.SelectedItem;
			UpdateRPWindow updateRPWindow = new UpdateRPWindow(rp.No);
			updateRPWindow.ShowDialog();
			m_rps = rpBll.GetAllRP();
			ManagerBindingRP(rpBll.GetAllRP(), maxnum, 1);
		}

		/// <summary>
		/// 添加一条奖罚记录
		/// </summary>
		private void AddOneRP()
		{
			AddRPWindow addRPWindow = new AddRPWindow();
			addRPWindow.ShowDialog();
			m_rps = rpBll.GetAllRP();
			ManagerBindingRP(rpBll.GetAllRP(),maxnum,1); 
		}
		/// <summary>
		/// 搜索一条奖罚记录
		/// </summary>
		private void SearchRP()
		{
			if (combox_search.SelectedItem == null)
			{
				MessageBox.Show("请先选择查询条件");
				return;
			}
			if (txt_search.Text != "" && (combox_search.SelectedItem).ToString() != "")
			{
				RPBLL rpBLL = new RPBLL();
				List<RP> rps = new List<RP>();
				//按员工ID查询 索引从0开始 但是第0项是空字符串
				if (combox_search.SelectedIndex == 1)
				{
					if (!isInteger(txt_search.Text))
					{
						MessageBox.Show("请输入数字");
						return;
					}
					int id = int.Parse(txt_search.Text);
					rps = rpBLL.GetRPsByUserId(id);
				}
				else
				{
					string name = txt_search.Text;
					rps = rpBLL.GetRPByRPName(name);
				}
				UserGrid.ItemsSource = rps;
				m_rps = rps;
				ManagerBindingRP(rps, maxnum, 1);
				txt_search.Text = "";
			}
			else
			{
				MessageBox.Show("查找方式和内容不能为空!");
			}
		}

		//实现分页，maxnum是每页最大记录数,假定为12条记录 currentPage表示当前所在页数
		private void ManagerBindingRP(List<RP> rps ,int maxnum, int currentpage)
		{
			// number表示每个页面显示的记录数 currentSize表示当前显示页数
			//List<RP> rps = rpBll.GetAllRP();
			int sum;
			if (rps == null)
			{
				sum = 0;
			}
			else
			{
				sum = rps.Count;//sum是记录总数
			}
			sumlabel.Content = sum; //把记录数显示到标签上
			int pagesize = 0;
			pagesize = 
				((sum % maxnum == 0) ? sum / maxnum : (sum / maxnum) + 1);
			tb_currentpage.Text = currentpage.ToString(); //显示当前页
			tb_endpage.Text = pagesize.ToString(); //显示总页数
			if (rps == null)
			{
				UserGrid.ItemsSource = null;
				return;
			}
			UserGrid.ItemsSource = rps.Skip((currentpage - 1) * maxnum).Take(maxnum).ToList();
		}


		//点击返回上一页响应事件(RP)
		private void return_button_RP()
		{
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage > 1)
			{
				ManagerBindingRP(m_rps,maxnum, currentpage - 1);
			}
		}

		//点击返回第一页（RP）
		private void return_all_button_RP()
		{
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage > 1)
			{
				ManagerBindingRP(m_rps,maxnum, 1);
			}
		}

		//点击进入下一页(RP)
		private void proceed_button_RP()
		{
			int endpage = int.Parse(tb_endpage.Text); //获取最大页数
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage < endpage)
			{
				ManagerBindingRP(m_rps,maxnum, currentpage + 1);
			}
		}
		//点击跳转到最末页(RP)
		private void proceed_all_button_RP()
		{
			int endpage = int.Parse(tb_endpage.Text); //获取最大页数
			int currentpage = int.Parse(tb_currentpage.Text);
			if (currentpage < endpage)
			{
				ManagerBindingRP(m_rps,maxnum, endpage);
			}
		}


		//跳转页按钮（RP）
		private void jump_button_RP()
		{
			if ( (tb_jumpage.Text) != "" && isInteger(tb_jumpage.Text) )
			{
				int jumpage = int.Parse(tb_jumpage.Text);
				int endpage = int.Parse(tb_endpage.Text);
				if (jumpage > 0 && jumpage <= endpage)
				{
					ManagerBindingRP(m_rps,maxnum, jumpage);
				}
				else
				{
					MessageBox.Show("跳转页数不在查询范围内!");
					return;
				}
			}
			else
			{
				MessageBox.Show("请输入有效数字!");
			}
			tb_jumpage.Text = "";
		}
	}
}
