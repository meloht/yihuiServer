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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YihuiMgr.Controls
{
    /// <summary>
    /// ProgressControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressControl : UserControl
    {
        public ProgressControl()
        {
            InitializeComponent();
        }

        public void ShowBar()
        {
            this.Visibility=Visibility.Visible;
        }

        public void HideBar()
        {
            this.Visibility = Visibility.Collapsed;
        }

        public void Progress(int per)
        {
            ProBar.Value = per;
        }
    }
}
