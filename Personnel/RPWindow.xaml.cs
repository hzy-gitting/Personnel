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
	/// RPWindow.xaml 的交互逻辑
	/// </summary>
	public partial class RPWindow : Window
	{
		RPBLL rpBLL = new RPBLL();
		public RPWindow()
		{
			InitializeComponent();
			dgRP.ItemsSource = rpBLL.GetAllRP();
		}
	}
}
