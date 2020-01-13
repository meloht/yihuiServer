using System.Windows;
using System.Windows.Controls;
using YihuiMgr.DataMgr;
using YihuiMgr.Util;

namespace YihuiMgr.UI.CrowFounding
{
    /// <summary>
    /// CrowFoundingMgrPage.xaml 的交互逻辑
    /// </summary>
    public partial class CrowFoundingMgrPage : Page
    {
        public CrowFoundingMgrPage()
        {
            InitializeComponent();
            this.Loaded += CrowFoundingMgrPage_Loaded;
        }

        private void CrowFoundingMgrPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxFirst.LoadData();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCrowFoundWindow window = new AddCrowFoundWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void ButtonQurey_OnClick(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            if (ComboBoxFirst.GetCategory() == null)
                return;

            int num = -1;
            if (TextId.Text.Trim().Length > 0)
            {
                num = HelpFunction.ConvertToInt(TextId.Text.Trim());
            }
            DataGrid.ItemsSource = CrowFoundMgr.Instance.QueryCrowDatas(num, TextActorName.Text.Trim(), this.ComboBoxFirst.GetCategory().fc_id);
        }

        private void ButtoncfBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button button= sender as Button;
            if(button==null)
                return;
            UpdateCrowdWindow window=new UpdateCrowdWindow((int)button.Tag);
            window.WindowStartupLocation=WindowStartupLocation.CenterScreen;
            window.WindowState=WindowState.Maximized;
            window.ShowDialog();

        }
    }
}
