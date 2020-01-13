using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YihuiMgr.Data;
using YihuiMgr.Util;
using YihuiMgr.ViewModel.Order;

namespace YihuiMgr.DataMgr
{
    public class OrderMgr
    {
        private static readonly object objLocker = new object();

        private static OrderMgr _instance;

        public static OrderMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLocker)
                    {
                        if (_instance == null)
                            _instance = new OrderMgr();
                    }
                }
                return _instance;
            }
        }

        public string GetPayState(int id)
        {
            switch (id)
            {
                case 0:
                    return "待支付";
                case 1:
                    return "已支付";
                case 2:
                    return "支付取消";
                case 3:
                    return "已退款";
            }
            return String.Empty;
        }
        public string GeState(int id)
        {
            switch (id)
            {
                case 0:
                    return "新建";
                case 1:
                    return "待审核";
                case 2:
                    return "审核通过";
                case 3:
                    return "取消";
                case 4:
                    return "作废";
                case 5:
                    return "退款";
                case 6:
                    return "完成";
            }
            return String.Empty;
        }

        /// <summary>
        /// 0 新建 1 待审核 2 审核通过 3 取消 4 作废 5 退款 6 完成
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="cfId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<OrderModel> GetOrders(string orderNo,string cfId, int status)
        {
            List<OrderModel> list = new List<OrderModel>();
          
            using (userEntities dataEntities = new userEntities())
            {
                try
                {
                    var cf = dataEntities.crowd_funding.SingleOrDefault(p => p.id.ToString() == cfId);
                    var res = dataEntities.order.Where(p => p.id >= 0);

                    if (!string.IsNullOrEmpty(cfId))
                    {
                        res = res.Where(p => p.order_type_id.ToString() == cfId);
                    }
                    if (status >= 0)
                    {
                        res = res.Where(p => p.status == status);
                    }
                    if (!string.IsNullOrEmpty(orderNo))
                    {
                        res = res.Where(p => p.order_code == orderNo);
                    }
                    int index = 1;
                    var listOrder = res.ToList();
                    foreach (var item in listOrder)
                    {
                        OrderModel model = new OrderModel();
                        model.Buyer = item.buyer;
                        if (item.create_time.HasValue)
                        {
                            model.CreateTime =
                                HelpFunction.ConvertToDateTime(item.create_time.Value).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        model.Num = index;
                        model.OrderNo = item.order_code;
                        
                        model.Status = GeState(item.status);
                        model.OrderType = item.order_type.ToString();
                        model.IsEnableCancelOrder = false;
                        model.PayStatus = item.paystatus;//0 待支付 1已支付 2支付取消 3已退款
                        model.PayStatusText = GetPayState(item.paystatus);

                        if (item.paystatus ==1)
                        {
                            if (item.order_type == 0)
                            {
                                if (cf != null)
                                {
                                    DateTime? end = HelpFunction.GetDateTime(cf.cf_end_time);
                                    if (end.HasValue && end.Value < DateTime.Now)
                                    {
                                        model.IsEnableCancelOrder = true;
                                    }
                                }
                            }
                            else
                            {
                                model.IsEnableCancelOrder = true;
                            }
                         
                           
                        }
                        model.ProductName = item.product_name;
                        model.Seller = item.seller;
                        model.ServerAddress = item.service_location;
                        model.Buyer = item.buyer;
                        if (item.service_time.HasValue)
                        {
                            model.ServerTime = HelpFunction.ConvertToDateTime(item.service_time.Value).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        list.Add(model);
                        index++;
                    }

                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            }
            return list;
        }



    }
}
