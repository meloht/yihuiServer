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

namespace YihuiMgr.UI.CrowFounding
{
    /// <summary>
    /// UpdateCrowdWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateCrowdWindow : Window
    {
        public UpdateCrowdWindow()
        {
            InitializeComponent();
            this.Loaded += UpdateCrowdWindow_Loaded;
        }

        private int _id;
        public UpdateCrowdWindow(int id) : this()
        {
            _id = id;
        }

        private void UpdateCrowdWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var data = CrowFoundMgr.Instance.GetCrowdFundingById(_id);

            if (data != null)
            {
                this.TextServiceIntro.HtmlContent = data.cf_desc;
                this.TextNote.HtmlContent = data.cf_note;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            bool bl = CrowFoundMgr.Instance.UpdateData(_id, this.TextServiceIntro.HtmlContent, this.TextNote.HtmlContent);
            if (bl)
            {
                MessageBox.Show("保存成功");
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }
    }
}
