using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YihuiMgr.Log;

namespace YihuiMgr
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string path = System.Windows.Forms.Application.StartupPath;
            string logFolder = System.IO.Path.Combine(path, "Logs");
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            string file = $"{DateTime.Now.ToString("yyyy-MM-dd")}.log";
            string filepath = System.IO.Path.Combine(logFolder, file);

            Trace.Listeners.Add(new FileTextTraceListener(filepath));
            Trace.AutoFlush = true;
            Trace.IndentSize = 4;
            Trace.WriteLine("app start");

        }
    }
}
