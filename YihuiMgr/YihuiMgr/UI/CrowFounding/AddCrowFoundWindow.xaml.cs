using System;
using System.Collections.Generic;
using System.Windows;
using YihuiMgr.Data;
using YihuiMgr.DataMgr;
using YihuiMgr.Util;
using YihuiQiniuSdk;
using YihuiQiniuSdk.Data;

namespace YihuiMgr.UI.CrowFounding
{
    /// <summary>
    /// AddCrowFoundWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddCrowFoundWindow : Window
    {
        public AddCrowFoundWindow()
        {
            InitializeComponent();
            this.Loaded += AddCrowFoundWindow_Loaded;
            TextNote.MaxString = 2000;
            TextServiceIntro.MaxString = 5000;

        }

        private void AddCrowFoundWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string key = QiniuUtil.GenerateGuid();
            TextFrontImg.SetKey(String.Format("{0}_crowdfundfrontimg", key));
            CityControl.LoadData();
            TextArtType.LoadData();

        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsCheckInput() == false)
                return;
            crowd_funding crowdFunding = new crowd_funding();
            if (TextBeginDate.Value.HasValue)
                crowdFunding.cf_begin_time = HelpFunction.ConvertToTimestamp(TextBeginDate.Value.Value);
            category category = TextArtType.GetCategory();
            if (category != null)
                crowdFunding.cf_category = category.fc_id;

            crowdFunding.cf_city_id = this.CityControl.GetCity().city_id;
            crowdFunding.cf_created_time = HelpFunction.ConvertToTimestamp(DateTime.Now);
            crowdFunding.cf_desc = this.TextServiceIntro.HtmlContent;
            if (TextEndDate.Value.HasValue)
            {
                crowdFunding.cf_end_time = HelpFunction.ConvertToTimestamp(this.TextEndDate.Value.Value);
            }
           
            crowdFunding.cf_front_icon = this.TextFrontImg.GetUrl();

            if (this.TextServiceCost.Value.HasValue)
                crowdFunding.cf_fund_end = (decimal)this.TextServiceCost.Value.Value;
            crowdFunding.cf_name = this.TextCrowName.Text.Trim();
            crowdFunding.cf_note = this.TextNote.HtmlContent;
            crowdFunding.cf_owner_id = this.TextActor.SelectActor.UserId;
            crowdFunding.cf_province_id = this.CityControl.GetProvince().province_id;
            crowdFunding.cf_service_address = this.TextAddress.Text.Trim();

            crowdFunding.cf_service_duration = this.TextServiceDuration.Value;
            if (TextServiceTime.Value.HasValue)
            {
                crowdFunding.cf_service_time = HelpFunction.ConvertToTimestamp(this.TextServiceTime.Value.Value);
            }
            

            var sc = TextArtType.GetSecondCategory();
            if (sc != null)
            {
                crowdFunding.cf_second_category = sc.cs_id;
            }
            var tc = TextArtType.GetThirdCategory();
            if (tc != null)
            {
                crowdFunding.cf_third_category = tc.ct_id;
            }

            crowdFunding.cf_state = 0;

            List<cf_type> listTypes = new List<cf_type>();
            listTypes.Add(TextCrowDetail.DefaultCfType());
            foreach (var item in TextCrowDetail.CtTypes)
            {
                listTypes.Add(item);
            }

            List<cf_label> listTag = new List<cf_label>();

            if (!string.IsNullOrEmpty(this.ArtTags.Label1.Trim()))
            {
                listTag.Add(new cf_label() { label_text = ArtTags.Label1.Trim() });
            }
            if (!string.IsNullOrEmpty(this.ArtTags.Label2.Trim()))
            {
                listTag.Add(new cf_label() { label_text = ArtTags.Label2.Trim() });
            }
            if (!string.IsNullOrEmpty(this.ArtTags.Label3.Trim()))
            {
                listTag.Add(new cf_label() { label_text = ArtTags.Label3.Trim() });
            }
            if (RadioButtonImg.IsChecked.HasValue && RadioButtonImg.IsChecked.Value)
            {
                crowdFunding.cf_front_icon_type = 0;
            }
            else
            {
                crowdFunding.cf_front_icon_type = 1;
            }

            if (TextFrontImg.UploadType == FileTypeDirEnum.CrowdFundVideoDir)
            {
                string img = "";
                bool bll = ServerFileMgr.GetVideoThumbnailImg(FileTypeDirEnum.CrowdFundVideoDir, TextFrontImg.ServerKey, out img);
                if (bll)
                {
                    crowdFunding.cf_video_thumbnail = img;
                }
            }

            bool bl = CrowFoundMgr.Instance.SaveCrowFound(crowdFunding, listTypes, listTag);
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
            TextFrontImg.DeleteFile();
            TextServiceIntro.DeleteFile();
        }

        private bool IsCheckInput()
        {
            if (string.IsNullOrEmpty(this.TextCrowName.Text))
            {
                MessageBox.Show("众筹名称不能为空！", "提示");
                TextCrowName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextBeginDate.Text))
            {
                MessageBox.Show("开始时间不能为空！", "提示");
                TextBeginDate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextEndDate.Text))
            {
                MessageBox.Show("结束时间不能为空！", "提示");
                TextEndDate.Focus();
                return false;
            }

            if (TextBeginDate.Value <= DateTime.Now)
            {
                MessageBox.Show("开始时间必须晚于今天！", "提示");
                TextBeginDate.Focus();
                return false;
            }
            if (TextBeginDate.Value >= TextEndDate.Value)
            {
                MessageBox.Show("开始时间必须早于结束时间！", "提示");
                TextEndDate.Focus();
                return false;
            }

            if (this.CityControl.GetCity() == null)
            {
                MessageBox.Show("城市不能为空！", "提示");
                CityControl.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextAddress.Text.Trim()))
            {
                MessageBox.Show("服务地点详细地址不能为空！", "提示");
                TextAddress.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextServiceDuration.Text))
            {
                MessageBox.Show("服务时长不能为空！", "提示");
                TextServiceDuration.Focus();
                return false;
            }
            
            if (this.TextActor.SelectActor == null)
            {
                MessageBox.Show("服务艺人不能为空！", "提示");
                TextActor.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextServiceCost.Text.Trim()))
            {
                MessageBox.Show("服务费用不能为空！", "提示");
                TextServiceCost.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextNote.HtmlContent.Trim()))
            {
                MessageBox.Show("注意事项不能为空！", "提示");
                TextNote.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextFrontImg.GetUrl().Trim()))
            {
                MessageBox.Show("封面图不能为空！", "提示");
                TextFrontImg.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.TextServiceIntro.HtmlContent.Trim()))
            {
                MessageBox.Show("封面图不能为空！", "提示");
                TextServiceIntro.Focus();
                return false;
            }
            if (this.TextCrowDetail.CtTypes.Count == 0)
            {
                MessageBox.Show("众筹详情不能为空！", "提示");
                TextCrowDetail.Focus();
                return false;
            }
            return true;
        }

        private void ToggleButtonImg_OnChecked(object sender, RoutedEventArgs e)
        {
            TextFrontImg.UploadType = FileTypeDirEnum.CrowdFundImgDir;
        }

        private void ToggleButtonVideo_OnChecked(object sender, RoutedEventArgs e)
        {
            TextFrontImg.UploadType = FileTypeDirEnum.CrowdFundVideoDir;
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DisposeData();
            this.DialogResult = false;
        }
    }
}
