using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YihuiMgr.Data;

namespace YihuiMgr.DataMgr
{
    public class AndroidMgr
    {
        private static readonly object objLocker = new object();

        private static AndroidMgr _instance;

        public static AndroidMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLocker)
                    {
                        if (_instance == null)
                            _instance = new AndroidMgr();
                    }
                }
                return _instance;
            }
        }

        public bool SaveAndroidApk(android_update android)
        {
            using (userEntities userEntities=new userEntities())
            {
                try
                {
                    userEntities.android_update.Add(android);
                    int iRent = userEntities.SaveChanges();
                    return iRent > 0;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
              
            }
            return false;
        }

        public List<android_update> GetAndroidUpdates()
        {
            List<android_update> list = new List<android_update>();

            using (userEntities userEntities = new userEntities())
            {
                try
                {
                    list = userEntities.android_update.OrderByDescending(p => p.create_time).ToList();
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
