using Personnel.BLL;
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
using personnel;
using personnel.Model;
using Personnel.Model;
namespace Personnel
{
	/// <summary>
	/// UserWindow.xaml 的交互逻辑
	/// </summary>
	public partial class UserWindow : Window
	{
		UserBLL userBLL = new UserBLL();
		SalaryBLL salaryBLL = new SalaryBLL();
		RPBLL rpBLL = new RPBLL();

		User user;
		Salary salary;
		List<RP> rps;
		int uid = CommonUser.LoginUser.Uid;
		//运行时配置
		public UserWindow()
		{
			try
			{
				InitializeComponent();
				GetUserInfoAndSalary();
				ShowLoginUserInfoAndSalaryInfo();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK,
					MessageBoxImage.Error);
			}
		}
		//显示当前登录员工的个人信息和工资信息
		private void ShowLoginUserInfoAndSalaryInfo()
		{
			txtBoxAge.Text = Convert.ToString(user.Age);
			txtBoxCurrentSal.Text = Convert.ToString(salary.Current_sal);
			txtBoxFolk.Text = user.Folk;
			txtBoxJob.Text = user.Job;
			txtBoxName.Text = user.Name;
			txtBoxPhone.Text = user.Phone;
			txtBoxPreSal.Text = Convert.ToString(salary.Pre_sal);
			txtBoxWorkTime.Text = Convert.ToString(user.Work_Time);
			dpBirthday.SelectedDate = user.Birthday;
		}
		//检测输入的合法性
		private void CheckValid()
		{
			//所有文本框
			TextBox[] textBoxes = {
				txtBoxAge , txtBoxFolk , txtBoxJob ,
				txtBoxName,txtBoxPhone,txtBoxWorkTime
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
				txtBoxAge ,txtBoxPhone,txtBoxWorkTime
			};
			foreach (TextBox tb in textBoxes_Num)
			{
				if (!ManagerWindow.isInteger(tb.Text))
				{
					throw new Exception("检测到非法输入，请输入数字");
				}
			}

		}

		//获取当前登录员工的个人信息，工资信息
		private void GetUserInfoAndSalary()
		{
			user = userBLL.GetUserByID(uid);
			if (user == null)
			{
				throw new Exception("无法获得uid为" + uid + "的员工信息");
			}
			salary = salaryBLL.GetsalaryByID(uid);
			if (salary == null)
			{
				throw new Exception("无法获得uid为" + uid + "的工资信息");
			}
		}
		//点击 查看奖惩记录
		private void BtnWatchRP_Click(object sender, RoutedEventArgs e)
		{
			RPWindow rpWindow = new RPWindow();
			rpWindow.Show();
		}

		//点击修改按钮
		private void BtnUpdate_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CheckValid();
				User userUpdated = new User();
				userUpdated.Age = Convert.ToInt32(txtBoxAge.Text);
				userUpdated.Birthday = (DateTime)dpBirthday.SelectedDate;
				userUpdated.Folk = txtBoxFolk.Text;
				userUpdated.Id = uid;
				userUpdated.Job = txtBoxJob.Text;
				userUpdated.Name = txtBoxName.Text;
				userUpdated.Phone = txtBoxPhone.Text;
				userUpdated.Work_Time = Convert.ToInt32(txtBoxWorkTime.Text);
				if (0 != userBLL.ModifyUser(userUpdated))
				{
					MessageBox.Show("修改信息成功", "人事工资管理系统", MessageBoxButton.OK);
				}
				else
				{
					MessageBox.Show("修改信息失败", "人事工资管理系统", MessageBoxButton.OK,
						MessageBoxImage.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "人事工资管理系统");
			}
		}

		//点击退出按钮
		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
