using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YihuiMgr.ViewModel.Order
{

    //[JsonObject(MemberSerialization.OptIn)]
    public class SearchModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("pageindex")]
        public int Pageindex { get; set; }
        [JsonProperty("pagesize")]
        public int Pagesize { get; set; }
        //最新 最热
        [JsonProperty("ordercolumn")]
        public String Ordercolumn { get; set; }
        [JsonProperty("ordertype")]
        public String Ordertype { get; set; }
        //分类id
        [JsonProperty("category")]
        public int Category { get; set; }
        //分类级别 2 或者 3
        [JsonProperty("categoryIndex")]
        public int CategoryIndex { get; set; }

        [JsonProperty("userid")]
        public int Userid { get; set; }

        [JsonProperty("keywords")]
        public String Keywords { get; set; }

        [JsonProperty("starttime")]
        public int Starttime { get; set; }

        [JsonProperty("endtime")]
        public int Endtime { get; set; }

        [JsonProperty("extionwhere")]
        public String Extionwhere { get; set; }

        [JsonProperty("currentLoginUserId")]
        public int CurrentLoginUserId { get; set; }

    }
}
