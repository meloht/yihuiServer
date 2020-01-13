using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using YihuiMgr.Data;
using YihuiMgr.Util;
using YihuiMgr.ViewModel.CrowFound;

namespace YihuiMgr.DataMgr
{
    public class CrowFoundMgr
    {
        private static readonly object objLocker = new object();

        private static CrowFoundMgr _instance;

        public static CrowFoundMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLocker)
                    {
                        if (_instance == null)
                            _instance = new CrowFoundMgr();
                    }
                }
                return _instance;
            }
        }

        public bool SaveCrowFound(crowd_funding crowdFunding, List<cf_type> cfTypes, List<cf_label> labels)
        {
            using (userEntities userEntities = new userEntities())
            {
                using (var tran = userEntities.Database.BeginTransaction())
                {
                    try
                    {
                        crowdFunding.cf_fund_amount = 0;
                        crowdFunding.support_amount = 0;
                        userEntities.crowd_funding.Add(crowdFunding);
                        int iRent = userEntities.SaveChanges();
                        if (iRent > 0)
                        {
                            foreach (var item in cfTypes)
                            {
                                item.cf_id = crowdFunding.id;
                            }
                            userEntities.cf_type.AddRange(cfTypes);


                            foreach (var item in labels)
                            {
                                item.cf_id = crowdFunding.id;
                            }
                            userEntities.cf_label.AddRange(labels);

                            userEntities.SaveChanges();
                        }

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

        public List<CrowData> QueryCrowDatas(int id, string actorName, int category)
        {
            List<CrowData> listResult = new List<CrowData>();

            using (userEntities userEntities = new userEntities())
            {
                try
                {
                    var res =
                        from a in userEntities.crowd_funding
                        join u in userEntities.user_base on a.cf_owner_id equals u.user_uid
                        join t in userEntities.category on a.cf_category equals t.fc_id
                        

                        where a.cf_category == category
                        select new
                        {
                            a.id,
                            a.cf_category,
                            t.fc_name,
                            a.cf_name,
                            u.user_nick,
                            a.cf_begin_time,
                            a.cf_end_time,
                            a.cf_fund_end,
                            a.cf_front_icon,
                            a.cf_front_icon_type,
                            a.cf_video_thumbnail,
                            a.cf_created_time
                        };


                    if (id != -1)
                    {
                        res = res.Where(p => p.id == id);
                    }
                    if (!string.IsNullOrEmpty(actorName))
                    {
                        res = res.Where(p => p.user_nick.Contains(actorName));
                    }

                    int index = 1;
                    foreach (var item in res)
                    {
                        string img = item.cf_front_icon;
                        if (item.cf_front_icon_type == 1)
                        {
                            img = item.cf_video_thumbnail;
                        }
                        listResult.Add(new CrowData()
                        {
                            Id = item.id,
                            ActorName = item.user_nick,
                            BeginTime = HelpFunction.GetDateTime(item.cf_begin_time),
                            EndTime = HelpFunction.GetDateTime(item.cf_end_time),
                            Category = item.fc_name,
                            CreatTime = HelpFunction.GetDateTime(item.cf_created_time),
                            FundEnd = item.cf_fund_end,
                            Title = item.cf_name,
                            FrontImg = img,
                            Num = index
                        });

                        index++;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            }
            return listResult;
        }


        public List<ActorSelectData> GetActorSelectDatas(string key)
        {
            List<ActorSelectData> list = new List<ActorSelectData>();
            using (userEntities userEntity = new userEntities())
            {
                try
                {
                    var data = from a in userEntity.user_detail
                               join b in userEntity.city on a.user_city_id equals b.city_id
                               join c in userEntity.user_base on a.user_uid equals c.user_uid
                               where c.user_nick.Contains(key) && c.is_actor == 1
                               select new
                               {
                                   c.user_nick,
                                   a.user_uid,
                                   b.city_name,
                                   a.user_city_id,
                                   a.user_career,
                                   c.phone
                               };

                    foreach (var item in data)
                    {
                        list.Add(new ActorSelectData()
                        {
                            Phone = item.phone,
                            ActorName = item.user_nick,
                            Career = item.user_career,
                            CityName = item.city_name,
                            UserId = item.user_uid

                        });
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            }

            return list;
        }

        public List<city> GetCity(int provinceId)
        {
            List<city> list = new List<city>();
            try
            {
                using (userEntities userEntities = new userEntities())
                {
                    list = userEntities.city.Where(p => p.province_id == provinceId).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return list;
        }

        public List<province> GetProvince()
        {
            List<province> list = new List<province>();
            try
            {
                using (userEntities userEntities = new userEntities())
                {
                    list = userEntities.province.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return list;
        }

        public List<category> GetCategories()
        {
            using (userEntities userEntities = new userEntities())
            {
                return userEntities.category.ToList();
            }
        }

        public List<category_second> GetCategorySeconds(int fid)
        {
            using (userEntities userEntities = new userEntities())
            {
                return userEntities.category_second.Where(p => p.fc_id == fid).ToList();
            }
        }

        public List<category_third> GetCategoryThirds(int sid)
        {
            using (userEntities userEntities = new userEntities())
            {
                return userEntities.category_third.Where(p => p.cs_id == sid).ToList();
            }
        }

        public crowd_funding GetCrowdFundingById(int id)
        {
            using (userEntities userEntities = new userEntities())
            {
                var data = userEntities.crowd_funding.SingleOrDefault(p => p.id == id);
                return data;
            }
        }

        public bool UpdateData(int id,string text)
        {
            using (userEntities userEntities = new userEntities())
            {
                try
                {
                    var data = userEntities.crowd_funding.SingleOrDefault(p => p.id == id);
                    if (data != null)
                    {
                        data.cf_desc = text;
                        userEntities.SaveChanges();
                    }
                   
                    return true;
                }
                catch (Exception ex)
                {
                    
                    Trace.WriteLine(ex);
                }
               
            }

            return false;
        }
        public bool UpdateData(int id, string textdec,string note)
        {
            using (userEntities userEntities = new userEntities())
            {
                try
                {
                    var data = userEntities.crowd_funding.SingleOrDefault(p => p.id == id);
                    if (data != null)
                    {
                        data.cf_desc = textdec;
                        data.cf_note = note;
                        userEntities.SaveChanges();
                    }

                    return true;
                }
                catch (Exception ex)
                {

                    Trace.WriteLine(ex);
                }

            }

            return false;
        }
    }
}
