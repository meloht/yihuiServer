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
    /// ArtTypeControl.xaml 的交互逻辑
    /// </summary>
    public partial class ArtTypeControl : UserControl
    {
        public ArtTypeControl()
        {
            InitializeComponent();
            
        }

        public void LoadData()
        {
            ComboBoxFirst.ItemsSource = CrowFoundMgr.Instance.GetCategories();
            if (ComboBoxFirst.HasItems)
                ComboBoxFirst.SelectedIndex = 0;
        }

        private void ComboBoxFirst_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            category category = ComboBoxFirst.SelectedItem as category;
            if (category != null)
            {
                ComboBoxSecond.ItemsSource = CrowFoundMgr.Instance.GetCategorySeconds(category.fc_id);
                if (ComboBoxSecond.HasItems)
                    ComboBoxSecond.SelectedIndex = 0;
            }
        }

        private void ComboBoxSecond_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            category_second categorySecond = ComboBoxSecond.SelectedItem as category_second;
            if (categorySecond != null)
            {
                ComboBoxThird.ItemsSource = CrowFoundMgr.Instance.GetCategoryThirds(categorySecond.cs_id);
                if (ComboBoxThird.HasItems)
                    ComboBoxThird.SelectedIndex = 0;
            }
        }

        public category GetCategory()
        {
            category category = ComboBoxFirst.SelectedItem as category;
            return category;
        }
        public category_second GetSecondCategory()
        {
            category_second category = ComboBoxSecond.SelectedItem as category_second;
            return category;
        }
        public category_third GetThirdCategory()
        {
            category_third category = ComboBoxThird.SelectedItem as category_third;
            return category;
        }
    }
}
