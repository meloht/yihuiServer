using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using YihuiMgr.Util;
using YihuiMgr.ViewModel;
using YihuiQiniuSdk;
using YihuiQiniuSdk.Data;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace YihuiMgr.Controls
{
    /// <summary>
    /// UploadControl.xaml 的交互逻辑
    /// </summary>
    public partial class UploadControl : UserControl
    {
       
        public UploadControl()
        {
            InitializeComponent();
        }


        public FileTypeDirEnum UploadType
        {
            get { return (FileTypeDirEnum)GetValue(UploadTypeProperty); }
            set { SetValue(UploadTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UploadTypeProperty =
            DependencyProperty.Register("UploadType", typeof(FileTypeDirEnum), typeof(UploadControl), new PropertyMetadata(FileTypeDirEnum.UserImgDir));

        public string GetUrl()
        {
            return this.TextBox.Text;
        }

        private string _guidKey = String.Empty;

        public void SetKey(string key)
        {
            if (_guidKey != key)
            {
                _isOverride = false;

            }
            _guidKey = key;

        }

        private string GetKey()
        {
            return _guidKey;
        }

        private bool _isOverride = false;

        private void OnProgress(int per)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.ProBar.Value = per;
            });
           
        }

        public string ServerKey { get; private set; }

        private ResultData Upload(string filekey, string fpath, FileTypeDirEnum dir, bool isOverride, Action<int> proAction)
        {
            try
            {
                var res = ServerFileMgr.UploadFileToServer(filekey, fpath, UploadType, isOverride, proAction);
                return res;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return null;

        }

        private delegate ResultData Dowork(UploadData data);

        public ResultData DoUploadData(UploadData data)
        {
            try
            {
                ResultData result = ServerFileMgr.UploadFileToServer(data.FileKey, data.Fpath, data.Dir, data.IsOverride,
                    data.ProAction);

                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return null;
        }

        private void Callback(IAsyncResult ar)
        {
            if (!ar.IsCompleted)
                return;
            var data = ar.AsyncState as Dowork;
            if (data == null)
                return;
            try
            {
                var res = data.EndInvoke(ar);
                this.Dispatcher.Invoke(() =>
                {

                    this.ProBar.Visibility=Visibility.Collapsed;
                    if (res == null)
                    {
                        MessageBox.Show("失败！");
                        return;
                    }
                    if (res.IsSuccess)
                    {
                        this.TextBox.Text = res.FullUrl;
                        ServerKey = res.Key;
                        _isOverride = true;
                    }
                    else
                    {
                        MessageBox.Show(String.Format("{0} {1}", res.ErrCode, res.ErrMsg));
                    }
                });
            }
            catch (Exception ex)
            {

                Trace.WriteLine(ex);
            }

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (GetKey() == String.Empty)
                return;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = QiniuUtil.GetFileFilter(UploadType);

            openFileDialog1.Multiselect = false;

            var re = openFileDialog1.ShowDialog();
            if (re == System.Windows.Forms.DialogResult.OK)
            {
                var file = openFileDialog1.FileName;
                string filekey = string.Format("{0}{1}", GetKey(), System.IO.Path.GetExtension(file));

                try
                {
                    openFileDialog1.Dispose();
                    this.ProBar.Visibility = Visibility.Visible;
                    Dowork dowork = DoUploadData;
                    UploadData data = new UploadData();
                    data.Dir = UploadType;
                    data.FileKey = filekey;
                    data.Fpath = file;
                    data.IsOverride = _isOverride;
                    data.ProAction = OnProgress;

                    dowork.BeginInvoke(data, Callback, dowork);

                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }


            }
        }

        public void DeleteFile()
        {
            ServerFileMgr.DeleteServerFile(UploadType, ServerKey);

        }
    }
}
