using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using YihuiQiniuSdk.Data;

namespace YihuiMgr.Controls
{
    /// <summary>
    /// RichTextControl.xaml 的交互逻辑
    /// </summary>
    public partial class RichTextControl : UserControl
    {
        public RichTextControl()
        {
            InitializeComponent();
            TextHtml.OnHtmlChanged += TextHtml_OnHtmlChanged;
        }

        public int MaxString = 0;

        private void TextHtml_OnHtmlChanged(object sender, TextChangedEventArgs e)
        {
            if (TextHtml.ContentHtml == null)
            {

                this.TextBlock.Text = String.Format("点击编辑区域获取字符长度(最大{1})：{0}", 0, MaxString);
            }
            else
            {
                this.TextBlock.Text = String.Format("点击编辑区域获取字符长度(最大{1})：{0}", TextHtml.ContentHtml.Length,MaxString);
            }
        }

        private void ToggleButtonCode_OnChecked(object sender, RoutedEventArgs e)
        {
            if (TextWebHtml == null || TextHtml == null)
                return;

            this.TextWebHtml.Visibility = Visibility.Collapsed;
            this.TextHtml.Visibility = Visibility.Visible;
        }


        private void ToggleButtonWeb_OnChecked(object sender, RoutedEventArgs e)
        {
            //this.TextWebHtml.Visibility = Visibility.Visible;
            //this.TextHtml.Visibility = Visibility.Collapsed;
            //if (TextHtml.Text.Trim().Length == 0)
            //    return;
            //try
            //{
            //    this.TextWebHtml.NavigateToString(TextHtml.Text);
            //}
            //catch (Exception ex)
            //{
            //    Trace.WriteLine(ex);
            //}

        }

        public FileTypeDirEnum UploadType
        {
            get { return (FileTypeDirEnum)GetValue(UploadTypeProperty); }
            set { SetValue(UploadTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UploadTypeProperty =
            DependencyProperty.Register("UploadType", typeof(FileTypeDirEnum), typeof(RichTextControl), new PropertyMetadata(FileTypeDirEnum.UserImgDir, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as RichTextControl;
            if (me != null)
            {
                me.BindUploadType((FileTypeDirEnum)e.NewValue);
            }
        }

        public void BindUploadType(FileTypeDirEnum e)
        {
            TextHtml.UploadType = e;
        }

        public void DeleteFile()
        {
            TextHtml.DeleteServerFile();
        }

        public string HtmlContent
        {
            get { return this.TextHtml.ContentHtml; }
            set { this.TextHtml.ContentHtml = value; }
        }
    }
}
