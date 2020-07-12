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
    /// AddSalary.xaml 的交互逻辑
    /// </summary>
    public partial class AddSalary : Window
    {
        SalaryBLL salaryBLL = new SalaryBLL();
        public AddSalary()
        {
            InitializeComponent();
        }
		/// <summary>
		/// 点击添加按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CheckValid();
				Salary newsalary = new Salary();
				newsalary.Id = Convert.ToInt32(txtBoxId.Text);
				newsalary.Pre_sal = Convert.ToInt32(txtBoxPre_sal.Text);
				newsalary.Current_sal = Convert.ToInt32(txtBoxCurrent_sal.Text);
				salaryBLL.AddSalary(newsalary);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "添加用户时出错");
			}
			new ManagerWindow().Show();
			this.Close();
		}
		private void CheckValid()
		{
			//所有文本框
			TextBox[] textBoxes = {
				txtBoxId , txtBoxPre_sal , txtBoxCurrent_sal 
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
				txtBoxId , txtBoxPre_sal , txtBoxCurrent_sal
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
		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			new ManagerWindow().Show();
			this.Close();
		}
	}
}
