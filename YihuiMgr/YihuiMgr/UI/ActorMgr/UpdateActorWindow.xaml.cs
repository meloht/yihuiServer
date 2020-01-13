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

namespace YihuiMgr.UI.ActorMgr
{
    /// <summary>
    /// UpdateActorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateActorWindow : Window
    {
        public UpdateActorWindow()
        {
            InitializeComponent();
            this.Loaded += UpdateActorWindow_Loaded;
        }

        private int _id = 0;

        public UpdateActorWindow(int id) : this()
        {
            _id = id;
        }

        private void UpdateActorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var data = DataMgr.ActorMgr.Instance.GetUserDetailById(_id);
            this.TextWebHtml.HtmlContent = data.user_intro;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            bool bl = DataMgr.ActorMgr.Instance.UpdateUserDetailById(_id, this.TextWebHtml.HtmlContent);
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
