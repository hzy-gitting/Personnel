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
    /// ModifyUser.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyUser : Window
    {
        UserBLL userBll=new UserBLL();
        public ModifyUser(User user)
        {
            InitializeComponent();
            InitData(user);
        }

        private void InitData(User user)
        {
            txtBox_Id.Text = Convert.ToString(user.Id);
            txtBox_Name.Text = user.Name;
            txtBox_Age.Text = (user.Age).ToString();
            txtBox_Phone.Text = user.Phone;
            txtBox_Folk.Text = user.Folk;
             switch (user.Job)
             {
                 case "普通员工": { combox_addjob.SelectedIndex =1; break; }
                 case "高级员工": { combox_addjob.SelectedIndex =2; break; }
                 case "副经理": { combox_addjob.SelectedIndex = 3; break; }
                 case "总经理": { combox_addjob.SelectedIndex = 4; break; }
                 case "董事长": { combox_addjob.SelectedIndex = 5; break; }
             }
            txtBox_WorkTime.Text = (user.Work_Time).ToString();
            dp_Birthday.DisplayDate = user.Birthday;
            dp_Birthday.SelectedDate= user.Birthday;
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckValid();
                User newUser = new User();
                newUser.Id = Convert.ToInt32(txtBox_Id.Text);
                newUser.Age = Convert.ToInt32(txtBox_Age.Text);
                newUser.Folk = txtBox_Folk.Text;
                newUser.Job = combox_addjob.Text;
                newUser.Name = txtBox_Name.Text;
                newUser.Phone = txtBox_Phone.Text;
                newUser.Work_Time = Convert.ToInt32(txtBox_WorkTime.Text);
                newUser.Birthday = dp_Birthday.DisplayDate;
                userBll.ModifyUser(newUser);
                //MessageBox.Show("修改成功!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改用户时出错");
            }
        }

        private void CheckValid()
        {
            //所有文本框
            TextBox[] textBoxes = {
                txtBox_Age , txtBox_Folk,
                txtBox_Name,txtBox_Phone,txtBox_WorkTime
            };
            foreach (TextBox tb in textBoxes)
            {
                if (tb.Text == "" || combox_addjob.Text == "")
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
                if (!ManagerWindow.isInteger(tb.Text))
                {
                    throw new Exception("检测到非法输入，请输入数字");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
