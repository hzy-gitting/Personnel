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
					User newUser = new User();
					newUser.Age = Convert.ToInt32(txtBox_Age.Text);
					newUser.Folk = txtBox_Folk.Text;
					newUser.Job = combox_addjob.Text;
					newUser.Name = txtBox_Name.Text;
					newUser.Phone = txtBox_Phone.Text;
					newUser.Work_Time = Convert.ToInt32(txtBox_WorkTime.Text);
					newUser.Birthday = dp_Birthday.DisplayDate;
					userBll.AddUser(newUser);
					MessageBox.Show("添加成功!");
					this.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "添加用户时出错");
				}
		}

		private void CheckValid()
		{
			//所有文本框
			TextBox[] textBoxes = {
				txtBox_Age , txtBox_Folk,
				txtBox_Name,txtBox_Phone,txtBox_WorkTime
			};
			foreach(TextBox tb in textBoxes)
			{
				if (tb.Text == ""|| combox_addjob.Text=="")
				{
					throw new Exception("请填写完整信息!");
				}
			}
			//只能输入数字的文本框
			TextBox[] textBoxes_Num = {
				txtBox_Age ,txtBox_Phone,txtBox_WorkTime
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
