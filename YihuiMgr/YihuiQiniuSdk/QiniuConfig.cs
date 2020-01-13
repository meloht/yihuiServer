using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YihuiQiniuSdk
{
    public class QiniuConfig
    {
        public class Bucket
        {
            public const string UserImgDir = "yuyi-user-img";
            public const string UserVideoDir = "yuyi-user-video";
            public const string VideoDir = "yuyi-video";
            public const string VideoImgDir = "yihui-video-img";
            public const string CrowdFundImgDir = "yuyi-crowdfund-img";
            public const string CrowdFundVideoDir = "yuyi-crowdfund-video";
            public const string AndroidUpdateDir = "android-update";//
        }


        public class BucketUrl
        {
            public const string UserImgDirBaseUrl = "http://7xth4k.com2.z0.glb.qiniucdn.com/";
            public const string UserVideoDirBaseUrl = "http://7xth4n.com2.z0.glb.qiniucdn.com/";
            public const string VideoDirBaseUrl = "http://7xth5f.com2.z0.glb.qiniucdn.com/";
            public const string VideoImgDirBaseUrl = "http://7xti1s.com2.z0.glb.qiniucdn.com/";
            public const string CrowdFundImgDirBaseUrl = "http://7xth4r.com2.z0.glb.qiniucdn.com/";
            public const string CrowdFundVideoDirBaseUrl = "http://7xth5c.com2.z0.glb.qiniucdn.com/";
            public const string AndroidUpdateDirBaseUrl = "http://o7q6t05gs.bkt.clouddn.com/";
        }

        public class BucketVideoHls
        {
            public const string UserVideoHls = "yihui-user-video-hls";
            public const string CrowdFundVideoHls = "yihui-crowdfund-video-hls";
            public const string VideoHls = "yihui-video-hls";
        }
        public class BucketVideoHlsUrl
        {
            public const string UserVideoHls = "http://o6mxw50mn.bkt.clouddn.com/";
            public const string CrowdFundVideoHls = "http://o6mxvzylw.bkt.clouddn.com/";
            public const string VideoHls = "http://o6mwd8zxn.bkt.clouddn.com/";
        }

        public const string AccessKey = "f0IhgInAo8Jb9NPoQKPEnmOtXIRFpdXppXO6aHYD";
        public const string SecretKey = "gyi4Zyv0aIx3hCvpZwDWVpBtukVcEmqH8657jPMO";





        public const string UploadCallback = "http://119.29.61.137:8080/yihui/user/addqiniunotify";
        public const string CallBackBody = "filename=$(fname)&filesize=$(fsize)";

        public static string GetWaterText()
        {
            string text = Qiniu.Util.Base64URLSafe.Encode("遇艺");
            string font = Qiniu.Util.Base64URLSafe.Encode("微软雅黑");
            string paras = String.Format(@"wmOffsetX/-16/wmOffsetY/-16/wmText/{0}/wmGravityText/SouthEast/wmFont/{1}/wmFontColor/IzYxNjA2MA==/wmFontSize/50", text, font);
            return paras;
        }
    }
}
