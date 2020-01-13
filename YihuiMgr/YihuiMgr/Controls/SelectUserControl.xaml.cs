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
using YihuiMgr.UI.CrowFounding;
using YihuiMgr.ViewModel.CrowFound;
using SelectUserWindow = YihuiMgr.UI.CrowFounding.SelectUserWindow;

namespace YihuiMgr.Controls
{
    /// <summary>
    /// SelectUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class SelectUserControl : UserControl
    {
        public SelectUserControl()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SelectUserWindow window = new SelectUserWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var bl = window.ShowDialog();
            if (bl.HasValue && bl.Value)
            {
                _selectData = window.GetActorSelectData();

                this.TextBox.Text = _selectData.ActorName;
            }
        }

        private ActorSelectData _selectData;
        public ActorSelectData SelectActor
        {
            get { return _selectData; }
        }
    }
}
