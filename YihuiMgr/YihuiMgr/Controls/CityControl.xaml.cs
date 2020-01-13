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
using YihuiMgr.Data;
using YihuiMgr.DataMgr;

namespace YihuiMgr.Controls
{
    /// <summary>
    /// CityControl.xaml 的交互逻辑
    /// </summary>
    public partial class CityControl : UserControl
    {
        public CityControl()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            this.ComboBoxProvince.ItemsSource = CrowFoundMgr.Instance.GetProvince();
            if (ComboBoxProvince.HasItems)
                ComboBoxProvince.SelectedIndex = 0;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            province province = ComboBoxProvince.SelectedItem as province;
            if (province != null)
            {
                this.ComboBoxCity.ItemsSource = CrowFoundMgr.Instance.GetCity(province.province_id);
                if (ComboBoxCity.HasItems)
                    ComboBoxCity.SelectedIndex = 0;
            }
        }

        public province GetProvince()
        {
            province province = ComboBoxProvince.SelectedItem as province;
            return province;
        }

        public city GetCity()
        {
            city city = ComboBoxCity.SelectedItem as city;
            return city;
        }
    }
}
