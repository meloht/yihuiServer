using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using YihuiMgr.Data;
using YihuiMgr.Util;
using YihuiMgr.ViewModel.Actor;
using YihuiQiniuSdk;
using YihuiQiniuSdk.Data;

namespace YihuiMgr.DataMgr
{
    public class ActorMgr
    {
        private static readonly object objLocker = new object();

        private static ActorMgr _instance;

        public static ActorMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLocker)
                    {
                        if (_instance == null)
                            _instance = new ActorMgr();
                    }
                }
                return _instance;
            }
        }

        public List<UserRequstModel> GetRequstModels()
        {
            List<UserRequstModel> list = new List<UserRequstModel>();
            using (userEntities data=new userEntities())
            {
                int index = 1;
                var res = from a in data.actor_requst
                    join b in data.user_base on a.user_uid equals b.user_uid 
                    select new
                    {
                        a.career,
                        a.experience,
                        a.gender,
                        a.org,
                        a.requst_time,
                        a.tel,
                        a.user_uid,
                        b.user_nick
                    };
                ;
                foreach (var item in res)
                {
                    UserRequstModel model=new UserRequstModel();
                    model.Career = item.career;
                    model.Experience = item.experience;
                    model.Gender = item.gender;
                    model.Org = item.org;
                    model.Tel = item.tel;
                    model.UserUid = item.user_uid;
                    model.UserNick = item.user_nick;
                    model.RequstTime = HelpFunction.GetDateTime(item.requst_time);
                    model.Num = index;

                    index++;
                    list.Add(model);
                }
            }
            return list;
        }

        public List<ActorModel> GetActorList(string name = null, string phone = null, int gender = -1)
        {
            List<ActorModel> list = new List<ActorModel>();

            using (userEntities data = new userEntities())
            {
                var res = from u in data.user_detail
                          join b in data.user_base on u.user_uid equals b.user_uid
                          where b.is_actor == 1
                          select new
                          {
                              u.user_uid,
                              b.user_nick,
                              u.user_gender,
                              u.user_career,
                              b.phone,
                              u.user_birth,
                              u.user_fronticon,
                              b.reg_time
                          };

                if (!string.IsNullOrEmpty(name))
                {
                    res = res.Where(p => p.user_nick.Contains(name));
                }
                if (!string.IsNullOrEmpty(phone))
                {
                    res = res.Where(p => p.phone == phone);
                }
                if (gender != -1)
                {
                    res = res.Where(p => p.user_gender == gender);
                }

                var tags = from t in data.user_label
                           join re in res on t.user_uid equals re.user_uid
                           select new
                           {
                               t.user_uid,
                               t.label_text
                           };
                int index = 1;
                var listTag = tags.ToList();
                Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
                foreach (var item in listTag)
                {
                    if (!dictionary.ContainsKey(item.user_uid))
                        dictionary.Add(item.user_uid, new List<string>());

                    dictionary[item.user_uid].Add(item.label_text);
                }
                foreach (var item in res)
                {
                    string labels = "";
                    if (dictionary.ContainsKey(item.user_uid))
                    {
                        labels = String.Join(",", dictionary[item.user_uid]);
                    }
                    list.Add(new ActorModel()
                    {
                        ActorCareer = item.user_career,
                        ActorContact = item.phone,
                        ActorGender = item.user_gender,
                        ActorName = item.user_nick,
                        ActorTags = labels,
                        BirthDay = item.user_birth,
                        JoinDate = HelpFunction.ConvertToDateTime(item.reg_time),
                        Id = item.user_uid,
                        FrontIcon =item.user_fronticon,
                        Num = index
                    });
                    index++;
                }
            }

            return list;
        }

        public user_detail GetUserDetailById(int id)
        {
            using (userEntities userEntities = new userEntities())
            {
                try
                {
                    return userEntities.user_detail.SingleOrDefault(p => p.user_uid == id);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            }
            return null;
        }

        public bool UpdateUserDetailById(int id, string text)
        {
            using (userEntities userEntities = new userEntities())
            {
                try
                {
                    var data = userEntities.user_detail.SingleOrDefault(p => p.user_uid == id);
                    if (data != null)
                    {
                        data.user_intro = text;
                        userEntities.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            }
            return false;
        }

        public void TestEf()
        {
            using (userEntities data = new userEntities())
            {
                //using (var dbContextTransaction = data.Database.BeginTransaction())
                using (TransactionScope trnaScope = new TransactionScope())
                {
                    try
                    {
                        user_base user = new user_base();
                        user.phone = "13022003660";
                        user.is_actor = 1;
                        user.reg_time = 11111;
                        user.status = 1;
                        user.user_email = "1231";
                        user.user_nick = "nick1111";
                        user.user_pwd = "123";

                        data.user_base.Add(user);
                        int irent = data.SaveChanges();

                        if (irent == 1)
                        {
                            var da = data.user_base.Remove(user);
                            int nn = data.SaveChanges();

                        }
                        trnaScope.Complete();
                        // dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        //dbContextTransaction.Rollback();
                    }
                }


            }
        }
        public bool SaveActor(UserInfo userInfo)
        {

            using (userEntities data = new userEntities())
            {
                using (var dbContextTransaction = data.Database.BeginTransaction())
                {
                    try
                    {
                        var exsit = data.user_base.Where(p => p.phone == userInfo.Phone).ToList();
                        if (exsit.Count > 0)
                        {
                            exsit[0].is_actor = 1;
                            exsit[0].status = 1;
                            exsit[0].user_nick = userInfo.Alias;
                        }
                        else
                        {
                            user_base userBase = new user_base();
                            userBase.is_actor = 1;
                            userBase.phone = userInfo.Phone;
                            userBase.reg_time = HelpFunction.ConvertToTimestamp(DateTime.Now);
                            userBase.status = 1;
                            userBase.user_pwd = CryptUtil.MD5("123456");
                            userBase.user_nick = userInfo.Alias;
                            data.user_base.Add(userBase);
                        }

                        int num = data.SaveChanges();
                        if (num > 0)
                        {
                            var us = data.user_base.Where(p => p.phone == userInfo.Phone).ToList();
                            if (us.Count > 0)
                            {
                                user_detail userDetail = new user_detail();
                                userDetail.user_uid = us[0].user_uid;
                                userDetail.user_city_id = userInfo.CityId;
                                userDetail.user_birth = userInfo.BirthDay;
                                userDetail.user_career = userInfo.Career;
                                userDetail.user_fronticon = userInfo.FrontIcon;
                                userDetail.user_gender = userInfo.Gender;
                                userDetail.user_icon = userInfo.HeadImg;
                                userDetail.user_intro = userInfo.Intro;
                                userDetail.user_video = userInfo.ShowVideo;

                                data.user_detail.Add(userDetail);

                                foreach (var tag in userInfo.Tags)
                                {
                                    user_label label = new user_label();
                                    label.user_uid = us[0].user_uid;
                                    label.label_text = tag;
                                    data.user_label.Add(label);
                                }
                                data.SaveChanges();

                            }
                        }
                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        Trace.WriteLine(ex);
                    }
                    return false;
                }
            }
        }

        public void DeleteServerFile(string key, FileTypeDirEnum dir)
        {
            ServerFileMgr.DeleteServerFile(dir, key);
        }
    }
}
