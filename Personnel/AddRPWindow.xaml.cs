using personnel.BLL;
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
	/// AddRPWindow.xaml 的交互逻辑
	/// 2020/7/12 10:40 
	/// hzy
	/// </summary>
	public partial class AddRPWindow : Window
	{
		RPBLL rpBll = new RPBLL();

		public AddRPWindow()
		{
			InitializeComponent();
		}

		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CheckValid();

				if (rpBll.AddRP(txtBoxRPName.Text, Convert.ToInt32(txtBoxSal),
					Convert.ToInt32(txtBoxUserId), dpRPTime.DisplayDate))
				{
					this.Close();
				}
				else
				{
					throw new Exception("添加奖惩记录失败");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "添加奖惩记录出错",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
			
		}
		private void CheckValid()
		{
			//所有文本框
			TextBox[] textBoxes = {
				txtBoxRPName , txtBoxSal , txtBoxUserId ,
			};
			foreach (TextBox tb in textBoxes)
			{
				if (tb.Text == "")
				{
					throw new Exception("信息填写不能为空");
				}
			}
			//只能输入数字的文本框
			TextBox[] textBoxes_Num = {
				txtBoxSal ,txtBoxUserId,
			};
			foreach (TextBox tb in textBoxes_Num)
			{
				if (!ManagerWindow.isInteger(tb.Text))
				{
					throw new Exception("检测到非法输入，请输入数字");
				}
			}
		}
		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
