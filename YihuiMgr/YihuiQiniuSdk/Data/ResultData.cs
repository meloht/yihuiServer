using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YihuiQiniuSdk.Data
{
    public class ResultData
    {
        public bool IsSuccess { get; set; }

        public string Key { get; set; }

        public string HashCode { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public string FullUrl { get; set; }
    }
}
