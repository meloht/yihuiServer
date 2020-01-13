using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YihuiQiniuSdk.Data;

namespace YihuiMgr.ViewModel
{
    public class UploadData
    {

        public string FileKey { get; set; }
        public string Fpath { get; set; }

        public FileTypeDirEnum Dir { get; set; }

        public bool IsOverride { get; set; }

        public Action<int> ProAction { get; set; }
    }
}
