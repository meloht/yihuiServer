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
using YihuiMgr.DataMgr;
using YihuiMgr.Util;
using YihuiMgr.ViewModel.CrowFound;

namespace YihuiMgr.UI.Order
{
    /// <summary>
    /// SelectCfWindows.xaml 的交互逻辑
    /// </summary>
    public partial class SelectCfWindow : Window
    {
        public SelectCfWindow()
        {
            InitializeComponent();
            this.Loaded += SelectCfWindow_Loaded;
        }

        private void SelectCfWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxCfFirst.LoadData();
        }

        public event EventHandler<CrowData> OnSelectHandle;
        private void ButtonCfQurey_OnClick(object sender, RoutedEventArgs e)
        {
            CfDataGrid.ItemsSource = null;
            if (ComboBoxCfFirst.GetCategory() == null)
                return;

            int num = -1;
            if (TextId.Text.Trim().Length > 0)
            {
                num = HelpFunction.ConvertToInt(TextId.Text.Trim());
            }
            CfDataGrid.ItemsSource = CrowFoundMgr.Instance.QueryCrowDatas(num, TextActorName.Text.Trim(), this.ComboBoxCfFirst.GetCategory().fc_id);
        }

        private void ButtonCfBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;
            CrowData data = btn.DataContext as CrowData;
            if (data != null)
            {
                OnSelectHandle?.Invoke(null, data);
            }
        }
    }
}
