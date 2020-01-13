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

namespace YihuiMgr.UI.ActorMgr
{
    /// <summary>
    /// UserRequstPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserRequstPage : Page
    {
        public UserRequstPage()
        {
            InitializeComponent();
            this.Loaded += UserRequstPage_Loaded;
        }

        private void UserRequstPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataGrid.ItemsSource = DataMgr.ActorMgr.Instance.GetRequstModels();
        }
    }
}
