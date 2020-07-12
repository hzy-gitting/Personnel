using Personnel.BLL;
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
	/// Window1.xaml 的交互逻辑
	/// </summary>
	public partial class WindowAddUser : Window
	{
		UserBLL userBll = new UserBLL();
		public WindowAddUser()
		{
			InitializeComponent();
		}

		//点击添加按钮
		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CheckValid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"添加用户时出错");
			}
			User newUser = new User();
			newUser.Age = Convert.ToInt32(txtBoxAge.Text);
			newUser.Folk = txtBoxFolk.Text;
			newUser.Job = txtBoxJob.Text;
			newUser.Name = txtBoxName.Text;
			newUser.Phone = txtBoxPhone.Text;
			newUser.Work_Time = Convert.ToInt32(txtBoxWorkTime.Text);
			newUser.Birthday = dpBirthday.DisplayDate;
			userBll.AddUser(newUser);

		}
		/// <summary>
		/// 验证合法性
		/// </summary>
		private void CheckValid()
		{
			//所有文本框
			TextBox[] textBoxes = {
				txtBoxAge , txtBoxFolk , txtBoxJob ,
				txtBoxName,txtBoxPhone,txtBoxWorkTime
			};
			foreach(TextBox tb in textBoxes)
			{
				if (tb.Text == "")
				{
					throw new Exception("信息填写不能为空");
				}
			}
			//只能输入数字的文本框
			TextBox[] textBoxes_Num = {
				txtBoxAge ,txtBoxPhone,txtBoxWorkTime
			};
			foreach (TextBox tb in textBoxes_Num)
			{
				if (!ManagerWindow.isInteger(tb.Text) )
				{
					throw new Exception("检测到非法输入，请输入数字");
				}
			}
			
		}

		//点击取消按钮，关闭窗口
		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
