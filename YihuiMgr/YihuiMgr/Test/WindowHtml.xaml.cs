﻿using System;
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

namespace YihuiMgr.Test
{
    /// <summary>
    /// WindowHtml.xaml 的交互逻辑
    /// </summary>
    public partial class WindowHtml : Window
    {
        public WindowHtml()
        {
            InitializeComponent();
            this.Loaded += WindowHtml_Loaded;
        }

        private void WindowHtml_Loaded(object sender, RoutedEventArgs e)
        {
            var data = CrowFoundMgr.Instance.GetCrowdFundingById(3);

            if (data != null)
            {
                this.HtmlEditor.HtmlContent = data.cf_desc;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var stt = this.HtmlEditor.HtmlContent;
            CrowFoundMgr.Instance.UpdateData(3, stt);
        }
    }
}
