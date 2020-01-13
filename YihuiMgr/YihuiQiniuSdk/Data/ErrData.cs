using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YihuiQiniuSdk.Data
{
    [DataContract]
    public class ErrData
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string error { get; set; }
    }
}
