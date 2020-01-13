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
using YihuiMgr.Data;
using YihuiMgr.DataMgr;
using YihuiMgr.Util;
using YihuiQiniuSdk;

namespace YihuiMgr.UI.AppUpdate
{
    /// <summary>
    /// AddAppVersion.xaml 的交互逻辑
    /// </summary>
    public partial class AddAppVersion : Window
    {
        public AddAppVersion()
        {
            InitializeComponent();
            this.Loaded += AddAppVersion_Loaded;
        }

        private void AddAppVersion_Loaded(object sender, RoutedEventArgs e)
        {
            string key = QiniuUtil.GenerateGuid();

            TextUrl.SetKey(String.Format("{0}_android", key));
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (!CheckInput())
                return;
            android_update android = new android_update();
            android.create_time = HelpFunction.ConvertToTimestamp(DateTime.Now);
            android.update_intro = this.TextIntro.HtmlContent;
            android.update_url = this.TextUrl.GetUrl();
            android.version_no = this.TextBoxVersionNo.Text;
            android.version_id = this.TextBoxVersionId.Value ?? 0;

            bool bl = AndroidMgr.Instance.SaveAndroidApk(android);
            if (bl)
            {
                MessageBox.Show("保存成功");
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("保存失败");
                this.DialogResult = false;
            }
        }


        private bool CheckInput()
        {
            if (!this.TextBoxVersionId.Value.HasValue)
            {
                MessageBox.Show("请设置版本id");
                this.TextBoxVersionId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextUrl.GetUrl()))
            {
                MessageBox.Show("请设置url");
                this.TextUrl.Focus();
                return false;
            }
            return true;
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
