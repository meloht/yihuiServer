using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YihuiMgr.Http.JsonData
{

    public class AndroidUpdate
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("versionNo")]
        public String versionNo { get; set; }
        [JsonProperty("versionId")]
        public int versionId { get; set; }

        //更新介绍
        [JsonProperty("updateIntro")]
        public String updateIntro { get; set; }
        [JsonProperty("updateUrl")]
        public String updateUrl { get; set; }
    }
}
