using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qiniu.IO
{
    /// <summary>
    /// 上传进度信息
    /// </summary>
    public class ProgressEventArgs : EventArgs
    {
        public int Progress { get; set; }
    }
}
