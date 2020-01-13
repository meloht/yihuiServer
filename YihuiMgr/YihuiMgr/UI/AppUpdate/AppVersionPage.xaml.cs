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
using YihuiMgr.DataMgr;
using YihuiMgr.UI.ActorMgr;

namespace YihuiMgr.UI.AppUpdate
{
    /// <summary>
    /// AppVersionPage.xaml 的交互逻辑
    /// </summary>
    public partial class AppVersionPage : Page
    {
        public AppVersionPage()
        {
            InitializeComponent();
           
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddAppVersion win = new AddAppVersion();
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win.ShowDialog();
        }

        private void ButtonQurey_OnClick(object sender, RoutedEventArgs e)
        {
            this.DataGrid.ItemsSource = AndroidMgr.Instance.GetAndroidUpdates();
        }
    }
}
