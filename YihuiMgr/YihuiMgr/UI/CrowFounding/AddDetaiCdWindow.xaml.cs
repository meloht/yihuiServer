using System.Windows;
using YihuiMgr.Data;
using YihuiMgr.Util;

namespace YihuiMgr.UI.CrowFounding
{
    /// <summary>
    /// AddDetaiCdWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddDetaiCdWindow : Window
    {
        public AddDetaiCdWindow()
        {
            InitializeComponent();
        }

        public cf_type GetCfType()
        {
            cf_type cfType = new cf_type();
            cfType.cf_amount = TextBoxNum.Value;
            cfType.left_amount = TextBoxNum.Value ?? 0;
            cfType.cf_desc = this.TextBoxDes.Text;
            if (this.TextBoxPrice.Value.HasValue)
            {
                cfType.cf_price = (decimal)TextBoxPrice.Value.Value;
            }
          
            cfType.cf_type_name = this.TextBoxName.Text.Trim();
            cfType.cf_cftype = 1;
            return cfType;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
