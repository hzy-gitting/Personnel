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
using personnel.Model;
using Personnel.BLL;
using Personnel.Model;

namespace Personnel
{
	/// <summary>
	/// ModifySalary.xaml 的交互逻辑
	/// </summary>
	public partial class ModifySalary : Window
	{
		SalaryBLL salaryBLL = new SalaryBLL();
		public ModifySalary(Salary salary)
		{
			InitializeComponent();
			InitData(salary);
		}
		private void InitData(Salary salary)
		{
			txtBoxModifyId.Text = Convert.ToString(salary.Id);
			txtBoxModifyPre_sal.Text = Convert.ToString(salary.Pre_sal);
			txtBoxModifyCurrent_sal.Text = Convert.ToString(salary.Current_sal);
		}

		/// <summary>
		/// 点击添加按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnModifyAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CheckValid();
				Salary newsalary = new Salary();
				newsalary.Id = Convert.ToInt32(txtBoxModifyId.Text);
				newsalary.Pre_sal = Convert.ToInt32(txtBoxModifyPre_sal.Text);
				newsalary.Current_sal = Convert.ToInt32(txtBoxModifyCurrent_sal.Text);
				salaryBLL.Modifysalary(newsalary);
				MessageBox.Show("修改成功!");
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "添加用户时出错");
			}
			this.Close();
		}
		private void CheckValid()
		{
			//所有文本框
			TextBox[] textBoxes = {
		txtBoxModifyId , txtBoxModifyPre_sal , txtBoxModifyCurrent_sal
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
		txtBoxModifyId , txtBoxModifyPre_sal , txtBoxModifyCurrent_sal
	    };
			foreach (TextBox tb in textBoxes_Num)
			{
				if (!ManagerWindow.isInteger(tb.Text))
				{
					throw new Exception("检测到非法输入，请输入数字");
				}
			}

		}
		///点击取消按钮，关闭窗口
		private void BtnModifyCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

	}
}
