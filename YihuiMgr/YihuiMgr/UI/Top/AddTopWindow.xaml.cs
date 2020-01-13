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
using YihuiMgr.Util;
using YihuiMgr.ViewModel.Actor;
using YihuiMgr.ViewModel.CrowFound;
using YihuiMgr.ViewModel.Top;
using YihuiMgr.ViewModel.Video;

namespace YihuiMgr.UI.Top
{
    /// <summary>
    /// AddTopWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddTopWindow : Window
    {
        public AddTopWindow()
        {
            InitializeComponent();
            this.Loaded += AddTopWindow_Loaded;
        }

        public event EventHandler<TopDataModel> OnItemSelected;
        private void AddTopWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxCfFirst.LoadData();
            this.ComboBoxVideoFirst.LoadData();
        }

        private void ButtonVideoQurey_OnClick(object sender, RoutedEventArgs e)
        {
            VideoDataGrid.ItemsSource = null;
            if (ComboBoxVideoFirst.GetCategory() == null)
                return;

            VideoDataGrid.ItemsSource = VideoDataMgr.Instance.GetVideoModels(this.TextVideoTitle.Text.Trim(), this.ComboBoxVideoFirst.GetCategory().fc_id);
        }

        private void ButtonActorQurey_OnClick(object sender, RoutedEventArgs e)
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
            ActorDataGrid.ItemsSource = DataMgr.ActorMgr.Instance.GetActorList(name, phone, gender);
        }

        private void ButtonCfQurey_OnClick(object sender, RoutedEventArgs e)
        {
            CfDataGrid.ItemsSource = null;
            if (ComboBoxCfFirst.GetCategory() == null)
                return;

            int num = -1;
            if (TextId.Text.Trim().Length > 0)
            {
                num = HelpFunction.ConvertToInt(TextId.Text.Trim());
            }
            CfDataGrid.ItemsSource = CrowFoundMgr.Instance.QueryCrowDatas(num, TextActorName.Text.Trim(), this.ComboBoxCfFirst.GetCategory().fc_id);
        }

        private void ButtonVideoBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;
            VideoModel data = btn.DataContext as VideoModel;
            if (data != null)
            {
                TopDataModel model = new TopDataModel();
                model.ImgaeUrl = data.FrontIcon;
                model.SourceId = data.Id;
                
                model.SourceType = "视频";
                model.SourceTypeInt = 3;
                model.Title = data.VideoTitle;
                OnItemSelected?.Invoke(null, model);
            }
        }

        private void ButtonCfBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;
            CrowData data = btn.DataContext as CrowData;
            if (data != null)
            {
                TopDataModel model = new TopDataModel();
                model.ImgaeUrl = data.FrontImg;
                model.SourceId = data.Id;
                model.SourceType = "众筹";
                model.SourceTypeInt = 2;
                model.Title = data.Title;

                OnItemSelected?.Invoke(null, model);
            }
        }

        private void ButtonActorBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;
            ActorModel data = btn.DataContext as ActorModel;
            if (data != null)
            {
                TopDataModel model=new TopDataModel();
                model.ImgaeUrl = data.FrontIcon;
                model.SourceId = data.Id;
                model.SourceType = "艺人";
                model.SourceTypeInt = 1;
                model.Title = data.ActorName;
                OnItemSelected?.Invoke(null, model);
            }
        }
    }
}
