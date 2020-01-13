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
    /// CategoryControl.xaml 的交互逻辑
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        public CategoryControl()
        {
            InitializeComponent();
            
        }
        public void LoadData()
        {
            ComboBoxFirst.ItemsSource = CrowFoundMgr.Instance.GetCategories();
            if (ComboBoxFirst.HasItems)
                ComboBoxFirst.SelectedIndex = 0;
        }
   

        public category GetCategory()
        {
            category category = ComboBoxFirst.SelectedItem as category;
            return category;
        }
    }
}
