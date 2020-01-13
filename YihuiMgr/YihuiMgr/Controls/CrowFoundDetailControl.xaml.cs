using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using YihuiMgr.UI.CrowFounding;
using YihuiMgr.Data;
using AddDetaiCdWindow = YihuiMgr.UI.CrowFounding.AddDetaiCdWindow;

namespace YihuiMgr.Controls
{
    /// <summary>
    /// CrowFoundDetailControl.xaml 的交互逻辑
    /// </summary>
    public partial class CrowFoundDetailControl : UserControl
    {
        public CrowFoundDetailControl()
        {
            InitializeComponent();
            this.GridData.ItemsSource = CtTypes;
        }

        private ObservableCollection<cf_type> _ctTypes = new ObservableCollection<cf_type>();

        public ObservableCollection<cf_type> CtTypes
        {
            get { return _ctTypes; }
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            AddDetaiCdWindow window=new AddDetaiCdWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bool? bl = window.ShowDialog();
            if (bl.HasValue&&bl.Value)
            {
                _ctTypes.Add(window.GetCfType());
            }
        }

        public cf_type DefaultCfType()
        {
            cf_type cfType=new cf_type();
            cfType.cf_cftype = 0;
            cfType.cf_amount = 0;
            cfType.cf_desc = TextBoxDefaultDesc.Text.Trim();
            cfType.cf_type_name = TextBoxDefaultName.Text.Trim();
            cfType.cf_price = 0;

            return cfType;
        }
    }
}
