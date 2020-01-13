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
using YihuiMgr.Controls;
using YihuiMgr.DataMgr;

namespace YihuiMgr.UI.Top
{
    /// <summary>
    /// TopMgrPage.xaml 的交互逻辑
    /// </summary>
    public partial class TopMgrPage : Page
    {
        public TopMgrPage()
        {
            InitializeComponent();
            ItemControlOrgin.OnSelectedItem += ItemControlOrgin_OnSelectedItem;
            ItemControlEnd.OnSelectedItem += ItemControlEnd_OnSelectedItem;
        }


        private ItemImageControl _orginItem;

        private void ItemControlEnd_OnSelectedItem(object sender, Controls.ItemImageControl e)
        {
            if (_orginItem != null && _orginItem.GetCurrentData() != null)
            {
                ItemControlEnd.AddItem(_orginItem.GetCurrentData(), e.GetIndexNum());
            }
        }

        private void ItemControlOrgin_OnSelectedItem(object sender, Controls.ItemImageControl e)
        {
            _orginItem = e;
        }

        private void ButtonQurey_OnClick(object sender, RoutedEventArgs e)
        {
            if (!DatePickerTop.SelectedDate.HasValue)
            {
                return;
            }
            this.DataGrid.ItemsSource = TopDataMgr.Instance.TopQureyList(this.DatePickerTop.SelectedDate.Value);

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            AddTopWindow window = new AddTopWindow();
            window.Height = 800;
            window.Width = 900;
            window.OnItemSelected += Window_OnItemSelected;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void Window_OnItemSelected(object sender, ViewModel.Top.TopDataModel e)
        {
            ItemControlOrgin.AddItem(e);
        }

        private void ButtonOK_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ItemControlEnd.CheckData())
            {
                MessageBox.Show("精选数量未达到9个");
                return;
            }
            bool bl = TopDataMgr.Instance.SaveTopData(ItemControlEnd.GeTopDataModels(), ItemControlEnd.GetDateTime);
            if (bl)
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }
    }
}
