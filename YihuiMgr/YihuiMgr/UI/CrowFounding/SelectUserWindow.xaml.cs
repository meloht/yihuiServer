using System.Windows;
using YihuiMgr.DataMgr;
using YihuiMgr.ViewModel.CrowFound;

namespace YihuiMgr.UI.CrowFounding
{
    /// <summary>
    /// SelectUserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectUserWindow : Window
    {
        public SelectUserWindow()
        {
            InitializeComponent();

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.TextBoxKey.Text.Trim().Length == 0)
                return;
            var data = CrowFoundMgr.Instance.GetActorSelectDatas(TextBoxKey.Text.Trim());
            this.ListBox.ItemsSource = data;
        }

        private void ButtonOK_OnClick(object sender, RoutedEventArgs e)
        {
            if(this.ListBox.SelectedItem==null)
                return;
            ActorSelectData actor = ListBox.SelectedItem as ActorSelectData;
            if (actor == null)
                return;
            this.DialogResult = true;
        }

        public ActorSelectData GetActorSelectData()
        {
            ActorSelectData actor = ListBox.SelectedItem as ActorSelectData;
            return actor;
        }
    }
}
