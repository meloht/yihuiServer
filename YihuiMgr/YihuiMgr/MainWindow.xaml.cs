using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using YihuiMgr.Http;
using YihuiMgr.UI.ActorMgr;
using YihuiMgr.UI.CrowFounding;
using YihuiMgr.Test;
using YihuiMgr.UI.AppUpdate;
using YihuiMgr.UI.Order;
using YihuiMgr.UI.Top;
using ActorMgrPage = YihuiMgr.UI.ActorMgr.ActorMgrPage;
using CrowFoundingMgrPage = YihuiMgr.UI.CrowFounding.CrowFoundingMgrPage;
using SelectUserWindow = YihuiMgr.UI.CrowFounding.SelectUserWindow;
using VideoPage = YihuiMgr.UI.VideoMgr.VideoPage;

namespace YihuiMgr
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void ActorMgrBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //MainFrame.Navigate(new Uri("ActorMgr/ActorMgrPage.xaml",UriKind.Relative));
            //BorderContent.Child = new ActorMgrPage();
            MainFrame.Navigate(new ActorMgrPage());
        }

        private void ButtonCdBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CrowFoundingMgrPage());
        }

        private async void ButtonTest_OnClick(object sender, RoutedEventArgs e)
        {
            //AddTopWindow win=new AddTopWindow();
            //win.WindowStartupLocation=WindowStartupLocation.CenterScreen;
            //win.WindowState=WindowState.Maximized;
            //win.ShowDialog();

            //ActorMgr.Instance.TestEf();
            //WindowHtml test = new WindowHtml();
            //test.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //test.ShowDialog();
            WebUtils controller = new WebUtils();
            string str = await controller.DoPost(null, null);
            MessageBox.Show(str);
        }

        private void ButtonVideo_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new VideoPage());
        }

        private void ButtonOrder_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderPage());
        }

        private void ButtonTop_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TopMgrPage());
        }

        private void ButtonRequst_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UserRequstPage());
        }

        private void ButtonUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AppVersionPage());
        }
    }
}
