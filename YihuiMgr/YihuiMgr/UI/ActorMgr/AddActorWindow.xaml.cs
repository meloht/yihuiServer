using System;
using System.Collections.Generic;
using System.Windows;
using YihuiMgr.Util;
using YihuiMgr.ViewModel.Actor;
using YihuiQiniuSdk;

namespace YihuiMgr.UI.ActorMgr
{
    /// <summary>
    /// AddActorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddActorWindow : Window
    {
        public AddActorWindow()
        {
            InitializeComponent();
            this.Loaded += AddActorWindow_Loaded;
            TextWebHtml.MaxString = 5000;
        }

        private void AddActorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string key = QiniuUtil.GenerateGuid();

            TextHeadImg.SetKey(String.Format("{0}_userhead",key));
            TextVideo.SetKey(String.Format("{0}_uservideo",key));
            TextFrotImage.SetKey(String.Format("{0}_userfrontimg",key));

            ComboBoxLoaction.LoadData();
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsCheckInput() == false)
                return;
            UserInfo info = new UserInfo();
            info.Alias = this.TextBoxName.Text.Trim();
            if (this.TextBirth.SelectedDate.HasValue)
            {
                info.BirthDay = this.TextBirth.SelectedDate.Value;
            }

            info.Career = this.TextBoxCareer.Text;
            info.FrontIcon = this.TextFrotImage.GetUrl();
            if (this.RadioButtonMale.IsChecked.HasValue && RadioButtonMale.IsChecked.Value)
            {
                info.Gender = 1;
            }
            else
            {
                info.Gender = 0;
            }

            info.HeadImg = this.TextHeadImg.GetUrl();
            info.Intro = this.TextWebHtml.HtmlContent;
            info.ShowVideo = this.TextVideo.GetUrl();
            info.Tags = new List<string>();

            if (this.ArtTags.Label1.Trim().Length > 0)
            {
                info.Tags.Add(ArtTags.Label1.Trim());
            }
            if (this.ArtTags.Label2.Trim().Length > 0)
            {
                info.Tags.Add(ArtTags.Label2.Trim());
            }
            if (this.ArtTags.Label3.Trim().Length > 0)
            {
                info.Tags.Add(ArtTags.Label3.Trim());
            }

            info.Phone = this.TextBoxPhone.Text.Trim();
            var city = this.ComboBoxLoaction.GetCity();
            if (city != null)
            {
                info.CityId = city.city_id;
            }

            bool bl = DataMgr.ActorMgr.Instance.SaveActor(info);

            if (bl)
            {
                MessageBox.Show("保存成功");
                this.DialogResult = true;
            }
            else
            {

                DisposeData();
                MessageBox.Show("保存失败");
                this.DialogResult = false;
            }

        }

        private void DisposeData()
        {
            TextHeadImg.DeleteFile();
            TextVideo.DeleteFile();
            TextFrotImage.DeleteFile();
        }

        private bool IsCheckInput()
        {
            if (string.IsNullOrEmpty(this.TextBoxName.Text.Trim()))
            {
                MessageBox.Show("艺人艺名不能为空！", "提示");
                TextBoxName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextBoxPhone.Text.Trim()))
            {
                MessageBox.Show("手机不能为空！", "提示");
                TextBoxPhone.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextHeadImg.GetUrl().Trim()))
            {
                MessageBox.Show("头像不能为空！", "提示");
                TextHeadImg.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(this.TextVideo.GetUrl().Trim()))
            //{
            //    MessageBox.Show("视频不能为空！", "提示");
            //    TextVideo.Focus();
            //    return false;
            //}
            if (this.ComboBoxLoaction.GetCity() == null)
            {
                MessageBox.Show("所在地不能为空！", "提示");
                ComboBoxLoaction.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextBoxCareer.Text.Trim()))
            {
                MessageBox.Show("职业不能为空！", "提示");
                TextBoxCareer.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextBirth.Text.Trim()))
            {
                MessageBox.Show("出生日期不能为空！", "提示");
                TextBirth.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextFrotImage.GetUrl().Trim()))
            {
                MessageBox.Show("艺人封面图不能为空！", "提示");
                TextFrotImage.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextWebHtml.HtmlContent.Trim()))
            {
                MessageBox.Show("艺人介绍不能为空！", "提示");
                TextWebHtml.Focus();
                return false;
            }
            //if (this.TextWebHtml.HtmlContent.Length>=5000)
            //{
            //    MessageBox.Show("艺人介绍长度超过5000！", "提示");
            //    TextWebHtml.Focus();
            //    return false;
            //}
            return true;
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DisposeData();
            this.DialogResult = false;
        }
    }
}
