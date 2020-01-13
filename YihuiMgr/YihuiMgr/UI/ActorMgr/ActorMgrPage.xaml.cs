using System;
using System.Windows;
using System.Windows.Controls;

namespace YihuiMgr.UI.ActorMgr
{
    /// <summary>
    /// ActorMgrPage.xaml 的交互逻辑
    /// </summary>
    public partial class ActorMgrPage : Page
    {
        public ActorMgrPage()
        {
            InitializeComponent();
            this.Loaded += ActorMgrPage_Loaded;
        }

        private void ActorMgrPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddActorWindow win = new AddActorWindow();
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win.ShowDialog();
        }

        private void ButtonQurey_OnClick(object sender, RoutedEventArgs e)
        {
            Qurey();
        }

        private void Qurey()
        {
            string name = null;
            if (this.txtActorName.Text.Trim() != String.Empty)
                name = txtActorName.Text.Trim();
            string phone = null;
            if (this.txtContact.Text.Trim() != String.Empty)
                phone = this.txtContact.Text.Trim();

            int gender = -1;
            if (ComboBoxGender.SelectedIndex == 0)
            {
                gender = 1;
            }
            else if (ComboBoxGender.SelectedIndex == 1)
            {
                gender = 0;
            }
            DataGrid.ItemsSource = DataMgr.ActorMgr.Instance.GetActorList(name, phone, gender);
        }

        private void ButtoncfBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;

            UpdateActorWindow win = new UpdateActorWindow((int)button.Tag);
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win.WindowState=WindowState.Maximized;
            win.ShowDialog();
        }
    }
}
