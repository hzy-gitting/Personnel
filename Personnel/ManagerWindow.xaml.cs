using personnel.BLL;
using Personnel.BLL;
using Personnel.DAL;
using Personnel.Model;
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
    /// ManagerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ManagerWindow : Window
    {
        const int maxnum = 12;
        public ManagerWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation;
            this.ResizeMode = ResizeMode.CanMinimize;
            ManagerBinding(maxnum,1);
            pagename.Content = "用户管理";
        }


        //实现分页，maxnum是每页最大记录数,假定为12条记录 currentPage表示当前所在页数
        private void ManagerBinding(int maxnum,int currentpage)//管理员显示用户管理页面
        {
            UserBLL userBLL = new UserBLL();
            // number表示每个页面显示的记录数 currentSize表示当前显示页数
            List<User> userList = userBLL.getAllUsers();
            int sum = userList.Count;//sum是记录总数
            sumlabel.Content=sum; //把记录数显示到标签上
            int pagesize = 0;
            if (sum % maxnum == 0)//刚好可以填满
            {
                pagesize = sum / maxnum;
            }
            else
            {
                pagesize = (sum / maxnum )+ 1;
            }
            tb_currentpage.Text = currentpage.ToString(); //显示当前页
            tb_endpage.Text=pagesize.ToString(); //显示总页数
            UserGrid.ItemsSource = userList.Skip((currentpage - 1) * maxnum).Take(maxnum).ToList();
        }

        //点击返回上一页响应事件
        private void return_button(object sender, RoutedEventArgs e)
        {
            int currentpage = int.Parse(tb_currentpage.Text);
           if (currentpage > 1)
            {
               ManagerBinding(maxnum, currentpage - 1);
            }
        }

        //点击返回最前页  如果已经在最前页就失效
        private void return_all_button(object sender, RoutedEventArgs e)
        {
            int currentpage = int.Parse(tb_currentpage.Text);
            if (currentpage > 1)
            {
                ManagerBinding(maxnum, 1);
            }
        }

        //点击进入下一页响应事件
        private void proceed_button(object sender, RoutedEventArgs e)
        {
            int endpage = int.Parse(tb_endpage.Text); //获取最大页数
            int currentpage=int.Parse(tb_currentpage.Text);
            if (currentpage <endpage)
            {
                 ManagerBinding(maxnum, currentpage + 1);
            }
        }

        //点击跳转到最末页  如果已经在最末页就失效
        private void proceed_all_button(object sender, RoutedEventArgs e)
        {
            int endpage = int.Parse(tb_endpage.Text); //获取最大页数
            int currentpage = int.Parse(tb_currentpage.Text);
            if (currentpage < endpage)
            {
                ManagerBinding(maxnum, endpage);
            }
        }

        /*函数isInteger(string s)获取用户输入的值
           1.判断合法性
           正则表达式判断输入是否为整数
               \d 匹配数字（0-9 数字）
               例子：判断输入的字符串是否符合\d这个正则表达式
                    string s = Console.ReadLine();
                    string pattern =@"^\d*$";
                    bool isMatch = Regex.IsMatch(s, pattern);
               说明：^\d 表示以数字为开头， *表示开头后面的n个字符，
                      $表示以\d结尾，因为$会搜索最近的元字符来使用，所以不用再输入一次\d
                  因为这个正则表达式中有 \ ，所以要在最开始前加 @ 表示这个 \没有转移符的意思
                  使用Regex.IsMatch方法判断s的条件是否与正则表达式的条件成立
           */
        private static bool isInteger(string s)
        {
            string pattern = @"^\d*$";
            return Regex.IsMatch(s, pattern);
        }


        //跳转按钮
        private void jump_button(object sender, RoutedEventArgs e)
        {
            if (isInteger(tb_jumpage.Text) && (tb_jumpage.Text)!=null)
            {
                int jumpage = int.Parse(tb_jumpage.Text);
                int endpage = int.Parse(tb_endpage.Text);
                if (jumpage > 0 && jumpage <= endpage)
                {
                    ManagerBinding(maxnum, jumpage);
                }
                else
                {
                    MessageBox.Show("跳转页数不在查询范围内!");
                    return;
                }
                //清空格子中的数,防止保留在页面上影响下次操作

            }else
            {
                MessageBox.Show("请输入有效数字!");
            }
            tb_jumpage.Text = "";
        }

        //动态生成页面
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
                UserGrid.Columns.Insert(1 + i, dgtxtCol);
            }
            RPBLL rpBll = new RPBLL();
            UserGrid.ItemsSource = rpBll.GetAllRP();

        }

        /// 删除字段列
        private void RemoveFieldColumns()
        {
            int count = UserGrid.Columns.Count();
            //除第一列（checkbox）和最后一列（操作列）外，全部删除
            for (int i = 1; i < count - 1; i++)
            {
                UserGrid.Columns.RemoveAt(1);
            }
        }


        //2020-07-11 20:12 剩余功能如下：
        //查看用户详细信息
        //修改用户信息
        //增加一个用户
        //删除用户记录
        //根据条件查找用户



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
    }
}
