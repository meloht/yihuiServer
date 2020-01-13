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
using YihuiMgr.ViewModel.Top;

namespace YihuiMgr.Controls
{
    /// <summary>
    /// NineItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class NineItemControl : UserControl
    {
        public NineItemControl()
        {
            InitializeComponent();
            this.Item1.OnSelectedItem += Item_OnSelectedItem;
            this.Item2.OnSelectedItem += Item_OnSelectedItem;
            this.Item3.OnSelectedItem += Item_OnSelectedItem;

            this.Item4.OnSelectedItem += Item_OnSelectedItem;
            this.Item5.OnSelectedItem += Item_OnSelectedItem;
            this.Item6.OnSelectedItem += Item_OnSelectedItem;

            this.Item7.OnSelectedItem += Item_OnSelectedItem;
            this.Item8.OnSelectedItem += Item_OnSelectedItem;
            this.Item9.OnSelectedItem += Item_OnSelectedItem;

            this.Item1.OnItemClick += Item_OnItemClick; ;
            this.Item2.OnItemClick += Item_OnItemClick;
            this.Item3.OnItemClick += Item_OnItemClick;

            this.Item4.OnItemClick += Item_OnItemClick;
            this.Item5.OnItemClick += Item_OnItemClick;
            this.Item6.OnItemClick += Item_OnItemClick;

            this.Item7.OnItemClick += Item_OnItemClick;
            this.Item8.OnItemClick += Item_OnItemClick;
            this.Item9.OnItemClick += Item_OnItemClick;

            dictItem.Add(1, Item1);
            dictItem.Add(2, Item2);
            dictItem.Add(3, Item3);
            dictItem.Add(4, Item4);
            dictItem.Add(5, Item5);
            dictItem.Add(6, Item6);
            dictItem.Add(7, Item7);
            dictItem.Add(8, Item8);
            dictItem.Add(9, Item9);

            this.DatePickerTop.DisplayDate = DateTime.Today;
            this.DatePickerTop.SelectedDate = DateTime.Today;

        }

        private void Item_OnItemClick(object sender, int e)
        {
            foreach (var item in dictItem)
            {
                if (item.Key == e)
                    continue;
                item.Value.SetDefaultBg();
            }
        }

        private Dictionary<int, ItemImageControl> dictItem = new Dictionary<int, ItemImageControl>();

        public event EventHandler<ItemImageControl> OnSelectedItem;
        private TopDataModel _selectModel;
        private ItemImageControl _item;
        private void Item_OnSelectedItem(object sender, ItemImageControl e)
        {
            _item = e;
            _selectModel = e.GetCurrentData();
            OnSelectedItem?.Invoke(this, e);
        }

        public TopDataModel SelectModel
        {
            get { return _selectModel; }
        }

        public DateTime GetDateTime
        {
            get { return this.DatePickerTop.SelectedDate ?? DateTime.Now.Date; }
        }

        public ItemImageControl SelectItem
        {
            get { return _item; }
        }

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(NineItemControl), new PropertyMetadata(string.Empty));

        public Visibility DateVisibility
        {
            get { return (Visibility)GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateVisibilityProperty =
            DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(NineItemControl), new PropertyMetadata(Visibility.Collapsed));

        private Dictionary<int, TopDataModel> list = new Dictionary<int, TopDataModel>();

        public void AddItem(TopDataModel model)
        {
            var data = list.Values.Where(p => p.SourceId == model.SourceId && p.SourceTypeInt == model.SourceTypeInt).ToList();
            if (data.Count > 0)
                return;
            if (list.Count == 9)
            {
                return;
            }
            for (int i = 1; i < 10; i++)
            {
                if (!list.ContainsKey(i))
                {
                    list.Add(i, model);
                    dictItem[i].LoadData(model);
                    break;
                }
            }
        }

        public void AddItem(TopDataModel model, int index)
        {
            var data = list.Values.Where(p => p.SourceId == model.SourceId && p.SourceTypeInt == model.SourceTypeInt).ToList();
            if (data.Count > 0)
                return;
            if (list.Count == 9)
            {
                return;
            }
            if (list.ContainsKey(index))
            {
                list[index] = model;
            }
            else
            {
                list.Add(index, model);
            }

            dictItem[index].LoadData(model);
        }

        private void MenuItemRemove_Click(object sender, RoutedEventArgs e)
        {
            if (SelectItem != null)
            {
                list.Remove(SelectItem.GetIndexNum());
                _selectModel = null;
                SelectItem.LoadData(null);
            }
        }



        public bool CheckData()
        {
            return list.Count == 9;
        }

        public Dictionary<int, TopDataModel> GeTopDataModels()
        {
            if (!CheckData())
                return new Dictionary<int, TopDataModel>();


            foreach (var item in list)
            {
                item.Value.Date = GetDateTime.Date;
                item.Value.OrderIndex = item.Key;
            }
            return list;
        }
    }
}
