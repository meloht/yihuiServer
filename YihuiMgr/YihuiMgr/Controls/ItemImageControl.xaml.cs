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
using YihuiMgr.Data;
using YihuiMgr.Util;
using YihuiMgr.ViewModel;
using YihuiMgr.ViewModel.Actor;
using YihuiMgr.ViewModel.Top;

namespace YihuiMgr.Controls
{
    /// <summary>
    /// ItemImageControl.xaml 的交互逻辑
    /// </summary>
    public partial class ItemImageControl : UserControl
    {
        public ItemImageControl()
        {
            InitializeComponent();
        }

        public event EventHandler<ItemImageControl> OnSelectedItem;
        public event EventHandler<int> OnItemClick;
        public string NumIndex
        {
            get { return (string)GetValue(NumIndexProperty); }
            set { SetValue(NumIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumIndexProperty =
            DependencyProperty.Register("NumIndex", typeof(string), typeof(ItemImageControl), new PropertyMetadata(string.Empty));


        private TopDataModel _data;
        public void LoadData(TopDataModel data)
        {
            _data = data;
            string bl = "";
            if (data == null)
            {
                this.ImageItem.Source = null;
                this.TextBlockTitle.Text = "";
                this.TextBlockType.Text = "";
                SetDataVisible(Visibility.Collapsed);
            }
            else
            {
                if (_data.SourceTypeInt == 1)
                {
                    bl = "艺人";
                }
                else if (_data.SourceTypeInt == 2)
                {
                    bl = "视频";

                }
                else if (_data.SourceTypeInt == 3)
                {
                    bl = "众筹";
                }
                BindData(_data.ImgaeUrl, _data.Title, bl);
            }



        }

       

        private void SetDataVisible(Visibility visibility)
        {
            this.TextBlockTitle.Visibility = visibility;
            this.TextBlockType.Visibility = visibility;
            this.ImageItem.Visibility = visibility;

            if (visibility == Visibility.Collapsed)
            {
                this.TextBlockNum.Visibility = Visibility.Visible;
            }
            else
            {
                this.TextBlockNum.Visibility = Visibility.Collapsed;
            }
        }

        private void BindData(string url, string title, string typeName)
        {
            this.ImageItem.Source = new BitmapImage(new Uri(url));
            this.TextBlockTitle.Text = title;
            this.TextBlockType.Text = typeName;
            SetDataVisible(Visibility.Visible);
        }

        public TopDataModel GetCurrentData()
        {
            return _data;
        }


        private bool blSelectedflag = false;

        private void ItemImageControl_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            OnItemClick?.Invoke(null, int.Parse(NumIndex));
            if (blSelectedflag)
            {
                blSelectedflag = false;
                GridMain.Background = new SolidColorBrush(Colors.DarkOrange);
            }
            else
            {
                GridMain.Background = new SolidColorBrush(Colors.LightGray);
                blSelectedflag = true;

                OnSelectedItem?.Invoke(null, this);
            }
        }

        public int GetIndexNum()
        {
            return HelpFunction.ConvertToInt(NumIndex);
        }

        public void SetDefaultBg()
        {
            blSelectedflag = false;
            GridMain.Background = new SolidColorBrush(Colors.DarkOrange);
        }
    }
}
