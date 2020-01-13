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
using ThicknessConverter = Xceed.Wpf.DataGrid.Converters.ThicknessConverter;

namespace YihuiMgr.Controls
{
    /// <summary>
    /// ArtTagsControl.xaml 的交互逻辑
    /// </summary>
    public partial class ArtTagsControl : UserControl
    {
        public ArtTagsControl()
        {
            InitializeComponent();
        }

        public string Label1 => this.TextBoxTag1.Text;
        public string Label2 => this.TextBoxTag2.Text;

        public string Label3 => this.TextBoxTag3.Text;
    }
}
