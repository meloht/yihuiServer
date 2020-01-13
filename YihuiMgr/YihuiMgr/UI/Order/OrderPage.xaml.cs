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
using YihuiMgr.ViewModel;
using YihuiMgr.ViewModel.CrowFound;

namespace YihuiMgr.UI.Order
{
    /// <summary>
    /// OrderPage.xaml 的交互逻辑
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            this.Loaded += OrderPage_Loaded;
        }

        private void OrderPage_Loaded(object sender, RoutedEventArgs e)
        {
            BindOrderState();

        }

        /// <summary>
        /// 0 新建 1 待审核 2审核通过 3 取消 4作废 5退款 6完成
        /// </summary>
        private void BindOrderState()
        {
            List<ItemData> list = new List<ItemData>();
            list.Add(new ItemData() { Id = -1, Text = "==请选择==" });
            list.Add(new ItemData() { Id = 0, Text = "新建" });
            list.Add(new ItemData() { Id = 1, Text = "待审核" });
            list.Add(new ItemData() { Id = 2, Text = "审核通过" });
            list.Add(new ItemData() { Id = 3, Text = "取消" });
            list.Add(new ItemData() { Id = 4, Text = "作废" });
            list.Add(new ItemData() { Id = 5, Text = "退款" });
            list.Add(new ItemData() { Id = 6, Text = "完成" });

            this.ComboBoxStatus.ItemsSource = list;
            if (ComboBoxStatus.HasItems)
                ComboBoxStatus.SelectedIndex = 0;


        }

        private void ButtonQurey_OnClick(object sender, RoutedEventArgs e)
        {
            ItemData item = ComboBoxStatus.SelectedItem as ItemData;
            if (item == null)
                return;
            if (string.IsNullOrEmpty(this.TextCfNo.Text) && string.IsNullOrEmpty(this.TextOrderNo.Text))
            {
                MessageBox.Show("请选择众筹或者填写订单号");
                return;

            }
            var cf = TextCfNo.Tag as CrowData;
            if (cf == null)
            {
                return;
            }
            DataGrid.ItemsSource = OrderMgr.Instance.GetOrders(this.TextOrderNo.Text, cf.Id.ToString(), item.Id);
        }



        private void ButtonSelectCf_OnClick(object sender, RoutedEventArgs e)
        {
            SelectCfWindow window = new SelectCfWindow();
            window.Width = 900;
            window.Height = 800;
            window.OnSelectHandle += Window_OnSelectHandle;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void Window_OnSelectHandle(object sender, ViewModel.CrowFound.CrowData e)
        {
            this.TextCfNo.Text = e.Title;
            this.TextCfNo.Tag = e;
        }

        private void ButtonRefund_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
