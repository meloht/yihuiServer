using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YihuiMgr.Data;
using YihuiMgr.Http.JsonData;
using YihuiMgr.Util;
using YihuiMgr.ViewModel.Order;

namespace YihuiMgr.Http
{
    public class OrderController
    {

        public async void GetAndroidUpdate()
        {
            WebUtils webUtils = new WebUtils();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("sign", webUtils.GetSign(""));
            dict.Add("data", "");
            dict.Add("source", "C31B32364CE19CA8FCD150A417ECCE58");

            string json =await webUtils.DoPost("http://172.168.6.186:8088/comm/getandroidupdate", dict);
            var android = HelpFunction.ToObject<AndroidUpdate>(json);
            if (android != null)
            {

            }
        }

        public bool CancelOrder(int orderId)
        {
            SearchModel model = new SearchModel();
            model.Id = orderId;

            return false;

        }

        public bool RefundWxWebPay(order order)
        {

            return false;

        }
        public bool RefundWxAppPay(order order)
        {

            return false;

        }
        public bool RefundAliAppPay(order order)
        {

            return false;

        }
    }
}
