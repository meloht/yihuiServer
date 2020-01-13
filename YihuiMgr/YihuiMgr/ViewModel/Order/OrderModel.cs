using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YihuiMgr.ViewModel.Order
{
    public class OrderModel
    {
        public int Num { get; set; }

        public string OrderNo { get; set; }

        public string OrderType { get; set; }

        public string Buyer { get; set; }

        public string Seller { get; set; }

        public string ProductName { get; set; }

        public string CreateTime { get; set; }

        public string PayStatusText { get; set; }
        public int PayStatus { get; set; }

        public string ServerTime { get; set; }

        public string ServerAddress { get; set; }

        public string Status { get; set; }

        public bool IsEnableCancelOrder { get; set; }
    }
}
