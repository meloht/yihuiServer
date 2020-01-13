using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YihuiMgr.Data;
using YihuiMgr.Util;
using YihuiMgr.ViewModel.Top;

namespace YihuiMgr.DataMgr
{
    public class TopDataMgr
    {
        private static readonly object objLocker = new object();

        private static TopDataMgr _instance;

        public static TopDataMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLocker)
                    {
                        if (_instance == null)
                            _instance = new TopDataMgr();
                    }
                }
                return _instance;
            }
        }

        public List<TopDataModel> TopQureyList(DateTime dateTime)
        {
            List<TopDataModel> list = new List<TopDataModel>();

            using (userEntities userEntities = new userEntities())
            {
                var res = userEntities.recommendation.Where(p => p.data_date == dateTime).ToList();
                foreach (var item in res)
                {
                    TopDataModel model = new TopDataModel();
                    model.Date = item.data_date;
                    model.ImgaeUrl = item.icon;
                    model.Id = item.id;
                    model.SourceId = item.source_id;
                    //推荐来源类型，1、艺人 2、众筹 3视频

                    model.SourceTypeInt = item.source_type;
                    model.Title = item.source_title;
                    if (item.source_type == 1)
                    {
                        model.SourceType = "艺人";
                    }
                    else if (item.source_type == 2)
                    {
                        model.SourceType = "众筹";
                    }
                    else if (item.source_type == 3)
                    {
                        model.SourceType = "视频";
                    }

                    model.OrderIndex = item.order_index;

                    list.Add(model);
                }
            }

            return list;
        }

        public bool SaveTopData(Dictionary<int, TopDataModel> dict, DateTime dateTime)
        {
            using (userEntities userEntities = new userEntities())
            {
                using (var tran = userEntities.Database.BeginTransaction())
                {
                    try
                    {
                        var dataList = userEntities.recommendation.Where(p => p.data_date == dateTime).ToList();
                        if (dataList.Count > 0)
                        {
                            userEntities.recommendation.RemoveRange(dataList);
                            userEntities.SaveChanges();
                        }
                        foreach (var item in dict)
                        {
                            recommendation data = new recommendation();
                            data.create_id = 1;
                            data.create_name = "admin";
                            data.create_time = HelpFunction.ConvertToTimestamp(DateTime.Now);
                            data.data_date = item.Value.Date;
                            data.icon = item.Value.ImgaeUrl;
                            data.order_index = item.Value.OrderIndex;
                            data.source_id = item.Value.SourceId;
                            data.source_type = item.Value.SourceTypeInt;
                            data.source_title = item.Value.Title;
                            userEntities.recommendation.Add(data);
                        }
                        userEntities.SaveChanges();
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        Trace.WriteLine(ex);
                    }
                }

            }
            return false;
        }
    }
}
