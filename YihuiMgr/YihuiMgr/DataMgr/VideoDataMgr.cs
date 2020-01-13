using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using YihuiMgr.Data;
using YihuiMgr.Util;
using YihuiMgr.ViewModel.Video;

namespace YihuiMgr.DataMgr
{
    public class VideoDataMgr
    {
        private static readonly object objLocker = new object();

        private static VideoDataMgr _instance;

        public static VideoDataMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLocker)
                    {
                        if (_instance == null)
                            _instance = new VideoDataMgr();
                    }
                }
                return _instance;
            }
        }

        public List<VideoModel> GetVideoModels(string title, int categoryId)
        {
            List<VideoModel> list = new List<VideoModel>();

            using (userEntities userEntities = new userEntities())
            {
                try
                {
                    var data = from a in userEntities.video_info
                               join b in userEntities.category on a.v_category equals b.fc_id
                               where a.v_title.Contains(title) && a.v_category == categoryId
                               select new
                               {
                                   a.v_title,
                                   a.id,
                                   a.v_brief,
                                   a.v_duration,
                                   a.v_front_icon,
                                   a.v_video_url,
                                   b.fc_name,
                                   a.v_create_time
                               };

                    var labels = from a in userEntities.video_info
                                 join b in userEntities.video_label on a.id equals b.v_id
                                 where a.v_title.Contains(title) && a.v_category == categoryId
                                 select new
                                 {
                                     b.v_id,
                                     b.label_text
                                 };

                    Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
                    foreach (var item in labels)
                    {
                        if (!dictionary.ContainsKey(item.v_id))
                        {
                            dictionary.Add(item.v_id, new List<string>());
                        }
                        dictionary[item.v_id].Add(item.label_text);
                    }
                    int index = 1;
                    foreach (var item in data)
                    {
                        VideoModel model = new VideoModel();
                        model.ArtType = item.fc_name;
                        model.CreateDate = HelpFunction.ConvertToDateTime(item.v_create_time);
                        model.FrontIcon = item.v_front_icon;
                        model.Num = index;
                        if (dictionary.ContainsKey(item.id))
                        {
                            model.Tags = String.Join(",", dictionary[item.id]);
                        }
                        model.VideoBrief = item.v_brief;
                        model.VideoDuration = GetTimeLongText(item.v_duration);
                        model.VideoTitle = item.v_title;
                        model.Id = item.id;
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

        private string GetTimeLongText(int? seconds)
        {
            if (seconds.HasValue)
            {
                TimeSpan tsSpan = TimeSpan.FromSeconds(seconds.Value);
                return tsSpan.ToString("g");
            }
           return String.Empty;
        }


        public bool SaveVideoInfo(video_info videoInfo, List<video_label> videoLabels)
        {
            using (userEntities user = new userEntities())
            {
                using (var tran = user.Database.BeginTransaction())
                {
                    try
                    {

                        videoInfo.v_create_time = HelpFunction.ConvertToTimestamp(DateTime.Now);

                        user.video_info.Add(videoInfo);
                        int iRent = user.SaveChanges();
                        if (iRent > 0)
                        {
                            if (videoLabels.Count > 0)
                            {
                                foreach (var item in videoLabels)
                                {
                                    item.v_id = videoInfo.id;
                                }
                                user.video_label.AddRange(videoLabels);
                                user.SaveChanges();
                            }

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

    }
}
