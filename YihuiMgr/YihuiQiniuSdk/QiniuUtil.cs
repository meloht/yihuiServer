using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YihuiQiniuSdk.Data;

namespace YihuiQiniuSdk
{
    public class QiniuUtil
    {
        public static string GenerateGuid()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public static string GetFileFilter(FileTypeDirEnum fileTypeDir)
        {
            switch (fileTypeDir)
            {
                case FileTypeDirEnum.CrowdFundImgDir:
                case FileTypeDirEnum.UserImgDir:
                case FileTypeDirEnum.ViderImgDir:
                    return "images |*.jpg;*.png;*.jpeg";
                case FileTypeDirEnum.UserVideoDir:
                case FileTypeDirEnum.CrowdFundVideoDir:
                case FileTypeDirEnum.VideoDir:
                    return "videos |*.mp4";
                case FileTypeDirEnum.AndroidUpdateDir:
                    return "videos |*.apk";

            }
            return String.Empty;
        }



        public static string GetFullUrl(string key, FileTypeDirEnum dir)
        {
            string baseUrl = "";
            switch (dir)
            {
                case FileTypeDirEnum.CrowdFundImgDir:
                    baseUrl = QiniuConfig.BucketUrl.CrowdFundImgDirBaseUrl;
                    break;
                case FileTypeDirEnum.CrowdFundVideoDir:
                    baseUrl = QiniuConfig.BucketUrl.CrowdFundVideoDirBaseUrl;
                    break;
                case FileTypeDirEnum.UserImgDir:
                    baseUrl = QiniuConfig.BucketUrl.UserImgDirBaseUrl;
                    break;
                case FileTypeDirEnum.UserVideoDir:
                    baseUrl = QiniuConfig.BucketUrl.UserVideoDirBaseUrl;
                    break;
                case FileTypeDirEnum.VideoDir:
                    baseUrl = QiniuConfig.BucketUrl.VideoDirBaseUrl;
                    break;
                case FileTypeDirEnum.ViderImgDir:
                    baseUrl = QiniuConfig.BucketUrl.VideoImgDirBaseUrl;
                    break;
                case FileTypeDirEnum.AndroidUpdateDir:
                    baseUrl = QiniuConfig.BucketUrl.AndroidUpdateDirBaseUrl;
                    break;
            }
            return String.Format("{0}{1}", baseUrl, key);
        }

        public static string GetServerBucket(FileTypeDirEnum fileTypeDir)
        {
            switch (fileTypeDir)
            {
                case FileTypeDirEnum.CrowdFundImgDir:
                    return QiniuConfig.Bucket.CrowdFundImgDir;
                case FileTypeDirEnum.CrowdFundVideoDir:
                    return QiniuConfig.Bucket.CrowdFundVideoDir;
                case FileTypeDirEnum.UserImgDir:
                    return QiniuConfig.Bucket.UserImgDir;
                case FileTypeDirEnum.UserVideoDir:
                    return QiniuConfig.Bucket.UserVideoDir;
                case FileTypeDirEnum.VideoDir:
                    return QiniuConfig.Bucket.VideoDir;
                case FileTypeDirEnum.ViderImgDir:
                    return QiniuConfig.Bucket.VideoImgDir;
                case FileTypeDirEnum.AndroidUpdateDir:
                    return QiniuConfig.Bucket.AndroidUpdateDir;
            }
            return String.Empty;
        }
       

        public static string GetServerVideoHlsUrl(string key, FileTypeDirEnum fileTypeDir)
        {
            string baseUrl = "";
            switch (fileTypeDir)
            {
                case FileTypeDirEnum.CrowdFundImgDir:
                    baseUrl = QiniuConfig.BucketUrl.CrowdFundImgDirBaseUrl;
                    break;
                case FileTypeDirEnum.CrowdFundVideoDir:
                    baseUrl = QiniuConfig.BucketVideoHlsUrl.CrowdFundVideoHls;
                    break;
                case FileTypeDirEnum.UserImgDir:
                    baseUrl = QiniuConfig.BucketUrl.UserImgDirBaseUrl;
                    break;
                case FileTypeDirEnum.UserVideoDir:
                    baseUrl = QiniuConfig.BucketVideoHlsUrl.UserVideoHls;
                    break;
                case FileTypeDirEnum.VideoDir:
                    baseUrl = QiniuConfig.BucketVideoHlsUrl.VideoHls;
                    break;
                case FileTypeDirEnum.ViderImgDir:
                    baseUrl = QiniuConfig.BucketUrl.VideoImgDirBaseUrl;
                    break;
              
            }
            return String.Format("{0}{1}", baseUrl, key);
        }

        public static string GetServerBucketHlsName(FileTypeDirEnum fileTypeDir)
        {
            switch (fileTypeDir)
            {
                case FileTypeDirEnum.VideoDir:
                    return QiniuConfig.BucketVideoHls.VideoHls;
                case FileTypeDirEnum.CrowdFundVideoDir:
                    return QiniuConfig.BucketVideoHls.CrowdFundVideoHls;
                case FileTypeDirEnum.UserVideoDir:
                    return QiniuConfig.BucketVideoHls.UserVideoHls;
            }
            return String.Empty;
        }

        public static string GetKeyWithoutExt(string key)
        {
            if (key.Contains("."))
            {
                string[] arr = key.Split(new[] {"."}, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                    return arr[0];
            }
            return key;
        }
        public static string GetKeyWithExt(string key)
        {
            if (key.Contains("."))
            {
                string[] arr = key.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length >=2)
                    return arr[1];
            }
            return "";
        }
    }
}
