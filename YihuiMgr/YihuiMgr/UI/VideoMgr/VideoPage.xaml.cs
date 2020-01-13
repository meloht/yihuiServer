using System.Windows;
using System.Windows.Controls;
using YihuiMgr.DataMgr;

namespace YihuiMgr.UI.VideoMgr
{
    /// <summary>
    /// VideoPage.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPage : Page
    {
        public VideoPage()
        {
            InitializeComponent();
            this.Loaded += VideoPage_Loaded;

        }

        private void VideoPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxFirst.LoadData();
        }

        private void ButtonQurey_OnClick(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            if (ComboBoxFirst.GetCategory() == null)
                return;

            DataGrid.ItemsSource = VideoDataMgr.Instance.GetVideoModels(this.TextVideoTitle.Text.Trim(), this.ComboBoxFirst.GetCategory().fc_id);

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddVideoWindow window = new AddVideoWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }


    }
}
