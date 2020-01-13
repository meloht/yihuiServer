using System;
using System.Collections.Generic;
using System.Windows;
using YihuiMgr.Controls;
using YihuiMgr.Data;
using YihuiMgr.DataMgr;
using YihuiMgr.Util;
using YihuiQiniuSdk;

namespace YihuiMgr.UI.VideoMgr
{
    /// <summary>
    /// AddVideoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddVideoWindow : Window
    {
        public AddVideoWindow()
        {
            InitializeComponent();
            this.Loaded += AddVideoWindow_Loaded;
          

        }

        private void AddVideoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string key = QiniuUtil.GenerateGuid();
            TextFrontImg.SetKey(String.Format("{0}_videofrontimg", key));
            TextVideo.SetKey(String.Format("{0}_video", key));
            TextArtType.LoadData();
        }

        private void ButtonOK_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsCheckInput())
                return;
            video_info videoInfo = new video_info();
            videoInfo.v_brief = this.TextBrief.Text;
            if (TextArtType.GetCategory() != null)
                videoInfo.v_category = this.TextArtType.GetCategory().fc_id;

            if (TextArtType.GetSecondCategory() != null)
            {
                videoInfo.v_second_category = this.TextArtType.GetSecondCategory().cs_id;
            }
            if (TextArtType.GetThirdCategory() != null)
            {
                videoInfo.v_third_category = this.TextArtType.GetThirdCategory().ct_id;
            }

            videoInfo.v_create_time = HelpFunction.ConvertToTimestamp(DateTime.Now) ;
            videoInfo.v_detail_intro = this.TextIntro.Text;
            videoInfo.v_duration = this.TextVideoDuration.Value;
            videoInfo.v_front_icon = this.TextFrontImg.GetUrl();
            videoInfo.v_title = this.TextBoxTitle.Text;
            videoInfo.v_video_url = this.TextVideo.GetUrl();

            List<video_label> listTag = new List<video_label>();

            if (!string.IsNullOrEmpty(this.ArtTags.Label1.Trim()))
            {
                listTag.Add(new video_label() { label_text = ArtTags.Label1.Trim() });
            }
            if (!string.IsNullOrEmpty(this.ArtTags.Label2.Trim()))
            {
                listTag.Add(new video_label() { label_text = ArtTags.Label2.Trim() });
            }
            if (!string.IsNullOrEmpty(this.ArtTags.Label3.Trim()))
            {
                listTag.Add(new video_label() { label_text = ArtTags.Label3.Trim() });
            }

            bool bl = VideoDataMgr.Instance.SaveVideoInfo(videoInfo, listTag);
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

        private bool IsCheckInput()
        {
            if (string.IsNullOrEmpty(this.TextBoxTitle.Text.Trim()))
            {
                MessageBox.Show("视频标题不能为空！", "提示");
                TextBoxTitle.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextFrontImg.GetUrl().Trim()))
            {
                MessageBox.Show("封面图不能为空！", "提示");
                TextFrontImg.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextVideo.GetUrl().Trim()))
            {
                MessageBox.Show("视频信息不能为空！", "提示");
                TextVideo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextVideoDuration.Text.Trim()))
            {
                MessageBox.Show("视频时长不能为空！", "提示");
                TextVideoDuration.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextBrief.Text.Trim()))
            {
                MessageBox.Show("简易描述不能为空！", "提示");
                TextBrief.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextIntro.Text.Trim()))
            {
                MessageBox.Show("详细描述不能为空！", "提示");
                TextIntro.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.ArtTags.Label1.Trim()))
            {
                MessageBox.Show("艺术标签不能为空！", "提示");
                ArtTags.Focus();
                return false;
            }
            return true;
        }
    }
}
