using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YihuiMgr.Http.JsonData
{
    public class RefundJson
    {
        [JsonProperty("orderId")]
        public int OrderId { get; set; }

        /// <summary>
        /// 0微信,1支付宝 2 微信公众号
        /// </summary>
        [JsonProperty("payOrderType")]
        public int PayOrderType { get; set; }

       


    }
}
