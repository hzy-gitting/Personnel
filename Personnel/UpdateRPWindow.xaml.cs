using personnel.BLL;
using personnel.Model;
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
	/// UpdateRPWindow.xaml 的交互逻辑
	/// </summary>
	public partial class UpdateRPWindow : Window
	{
		public int No;
		RPBLL rpBll = new RPBLL();
		public UpdateRPWindow(int no)
		{
			No = no;
			InitializeComponent();
			ShowTheRP();
		}

		//显示指定的奖罚记录
		private void ShowTheRP()
		{
			RP rp = rpBll.GetRPByNo(No);
			txtBoxNo.Text = Convert.ToString(rp.No);
			txtBoxRpName.Text = rp.Rp_name;
			txtBoxSal.Text = Convert.ToString(rp.Sal);
			txtBoxUserId.Text = Convert.ToString(rp.User_id);
			dpRPTime.SelectedDate = rp.Rp_time;
		}
		//点击修改奖罚记录
		private void BtnUpdate_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CheckValid();
				if (!rpBll.UpdateRPById(No, txtBoxRpName.Text, Convert.ToInt32(txtBoxSal.Text),
					Convert.ToInt32(txtBoxUserId.Text),
					(DateTime)dpRPTime.SelectedDate))
				{
					MessageBox.Show("修改奖罚记录失败");
				}
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "修改奖罚记录时出错");
			}
		}
		private void CheckValid()
		{
			//所有文本框
			TextBox[] textBoxes = {
				txtBoxNo , txtBoxRpName,
				txtBoxSal,txtBoxUserId,
			};
			foreach (TextBox tb in textBoxes)
			{
				if (tb.Text == "" )
				{
					throw new Exception("请填写完整信息!");
				}
			}
			//只能输入数字的文本框
			TextBox[] textBoxes_Num = {
				txtBoxNo ,txtBoxUserId
			};
			foreach (TextBox tb in textBoxes_Num)
			{
				if (!ManagerWindow.isInteger(tb.Text))
				{
					throw new Exception("检测到非法输入，请输入数字");
				}
			}
			//奖金罚金可能为负数，特殊处理
			if (txtBoxSal.Text[0] == '-')
			{
				if (!ManagerWindow.isInteger(txtBoxSal.Text.Substring(
					1,txtBoxSal.Text.Length-1)))
				{
					throw new Exception("检测到非法输入，请输入数字");
				}
			}
			else
			{
				if (!ManagerWindow.isInteger(txtBoxSal.Text))
				{
					throw new Exception("检测到非法输入，请输入数字");
				}
			}
		}
		//点击取消修改
		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

	}
}
