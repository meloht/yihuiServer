using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Qiniu.FileOp;
using Qiniu.IO;
using Qiniu.RPC;
using Qiniu.RS;
using YihuiQiniuSdk.Data;

namespace YihuiQiniuSdk
{
    public class ServerFileMgr
    {
        public static ResultData UploadFileToServer(string filekey, string fpath, FileTypeDirEnum dir, bool isOverride, Action<int> proAction)
        {
            ResultData result = new ResultData();
            //设置账号的AK和SK
            Qiniu.Conf.Config.ACCESS_KEY = QiniuConfig.AccessKey;
            Qiniu.Conf.Config.SECRET_KEY = QiniuConfig.SecretKey;

            IOClient target = new IOClient();
            target.ProgressHandler += (x, y) =>
            {
                proAction?.Invoke(y.Progress);
            };
            PutExtra extra = new PutExtra();
            //设置上传的空间
            string bucket = QiniuUtil.GetServerBucket(dir);
            //设置上传的文件的key值
            string key = filekey;


            //覆盖上传,<bucket>:<key>，表示只允许用户上传指定key的文件。在这种格式下文件默认允许“修改”，已存在同名资源则会被本次覆盖。
            PutPolicy put = new PutPolicy(bucket, 3600);
            if (isOverride)
            {
                put = new PutPolicy(string.Format("{0}:{1}", bucket, key));
            }

            //VideoWaterMark(put, key, bucket);
            //调用Token()方法生成上传的Token
            string upToken = put.Token();
            //上传文件的路径
            string filePath = fpath;

            //调用PutFile()方法上传
            PutRet ret = target.PutFile(upToken, key, filePath, extra);
            //打印出相应的信息
            string json = ret.Response.ToString();

            if (json.Contains("hash") && json.Contains("key"))
            {
                ResData user = (ResData)JsonConvert.DeserializeObject(json, typeof(ResData));
                result.HashCode = user.hash;
                result.IsSuccess = true;
                result.Key = user.key;
                result.FullUrl = QiniuUtil.GetFullUrl(user.key, dir);
            }
            else
            {
                result.IsSuccess = false;
                var resData = (ErrData)JsonConvert.DeserializeObject(json, typeof(ErrData));
                result.ErrCode = resData.code;
                result.ErrMsg = resData.error;

            }

            return result;
        }

        public static void WaterMark(string fullUrl)
        {
            try
            {
                string text = "遇艺"; //
                string fontname = "微软雅黑"; //
                string color = "#616060"; // 
                int fontsize = 50; // 
                int dissolve = 70; //             
                int dx = 16; // 
                int dy = 16; // 
                TextWaterMarker target = new TextWaterMarker(text, fontname, color, fontsize, dissolve, MarkerGravity.SouthEast, dx, dy); // TODO: 初始化为适当的值

                string actual = target.MakeRequest(fullUrl);
                // WaterMarker.Call(actual);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

        }

        /// <summary>
        /// 视频切片
        /// </summary>
        private static void CutVideo(PutPolicy put, string key, FileTypeDirEnum dir)
        {
            var bucket = QiniuUtil.GetServerBucketHlsName(dir);
            if (bucket == String.Empty)
                return;

            //设置切片操作参数
            String fops = String.Format("avthumb/m3u8/segtime/10/ab/128k/ar/44100/acodec/libfaac/r/30/vb/240k/vcodec/libx264/stripmeta/0/{0}", QiniuConfig.GetWaterText());
            //设置切片的队列
            string pipeline = "yihui_queue";

            //可以对转码后的文件进行使用saveas参数自定义命名，当然也可以不指定文件会默认命名并保存在当前空间。
            String urlbase64 = Qiniu.Util.Base64URLSafe.Encode(String.Format("{0}:{1}", bucket, key));
            String pfops = fops + "|saveas/" + urlbase64;

            put.FsizeLimit = 1024 * 1024 * 800;

            put.PersistentNotifyUrl = QiniuConfig.UploadCallback;

            put.PersistentOps = pfops;
            put.PersistentPipeline = pipeline;
        }

        private static string VideoWaterMark(PutPolicy put, string key, string bucket)
        {
            string fops = string.Format("avthumb/mp4/{0}", QiniuConfig.GetWaterText());
            string pipeline = "yihui_queue";

            //可以对转码后的文件进行使用saveas参数自定义命名，当然也可以不指定文件会默认命名并保存在当前空间。
            String urlbase64 = Qiniu.Util.Base64URLSafe.Encode(String.Format("{0}:{1}", bucket, key));
            String pfops = fops + "|saveas/" + urlbase64;
            put.FsizeLimit = 1024 * 1024 * 800;

            put.PersistentNotifyUrl = QiniuConfig.UploadCallback;

            put.PersistentOps = pfops;
            put.PersistentPipeline = pipeline;

            return key;
        }

        public static void WaterMarkPop(FileTypeDirEnum dir, string key)
        {
            Qiniu.Conf.Config.ACCESS_KEY = QiniuConfig.AccessKey;
            Qiniu.Conf.Config.SECRET_KEY = QiniuConfig.SecretKey;

            //设置空间
            string bucket = QiniuUtil.GetServerBucket(dir);

            //实例化一个entry对象
            EntryPath entry = new EntryPath(bucket, key);
            string ext = QiniuUtil.GetKeyWithExt(key);
            string fops = string.Format("avthumb/{0}/{1}", ext, QiniuConfig.GetWaterText());

            //设置切片的队列
            string pipeline = "yihui_queue";

            //可以对转码后的文件进行使用saveas参数自定义命名，当然也可以不指定文件会默认命名并保存在当前空间。
            String urlbase64 = Qiniu.Util.Base64URLSafe.Encode(String.Format("{0}:{1}", bucket, key));
            String pfops = fops + "|saveas/" + urlbase64;

            //实例化一个fop对象主要进行后续转码操作
            Qiniu.RS.Pfop fop = new Qiniu.RS.Pfop();

            Uri uri = new Uri(QiniuConfig.UploadCallback);
            try
            {
                string s = fop.Do(entry, new[] { pfops }, uri, pipeline, 1);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

        }

        public static bool DeleteServerFile(FileTypeDirEnum dir, string key)
        {
            Qiniu.Conf.Config.ACCESS_KEY = QiniuConfig.AccessKey;
            Qiniu.Conf.Config.SECRET_KEY = QiniuConfig.SecretKey;

            try
            {
                //设置空间
                string bucket = QiniuUtil.GetServerBucket(dir);

                //实例化一个RSClient对象，用于操作BucketManager里面的方法
                RSClient client = new RSClient();
                CallRet ret = client.Delete(new EntryPath(bucket, key));
                if (!ret.OK)
                {
                    Trace.WriteLine(ret.Response);
                    Trace.WriteLine(ret.Exception);
                }
                return ret.OK;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return false;
        }


        /// <summary>
        /// 处理视频缩略图
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="key"></param>
        /// <param name="fileImg"></param>
        /// <returns></returns>
        public static bool GetVideoThumbnailImg(FileTypeDirEnum dir, string key, out string fileImg)
        {
            Qiniu.Conf.Config.ACCESS_KEY = QiniuConfig.AccessKey;
            Qiniu.Conf.Config.SECRET_KEY = QiniuConfig.SecretKey;

            //设置空间
            string bucket = QiniuUtil.GetServerBucket(dir);

            //实例化一个entry对象
            EntryPath entry = new EntryPath(bucket, key);

            string name = String.Format("thumbnail-{0}.jpg", QiniuUtil.GetKeyWithoutExt(key));
            fileImg = QiniuUtil.GetFullUrl(name, dir);

            //设置操作参数
            String fops = "vframe/jpg/offset/5";
            //设置队列
            string pipeline = "yihui_queue";

            //可以对转码后的文件进行使用saveas参数自定义命名，当然也可以不指定文件会默认命名并保存在当前空间。
            String urlbase64 = Qiniu.Util.Base64URLSafe.Encode(String.Format("{0}:{1}", bucket, name));
            String pfops = fops + "|saveas/" + urlbase64;

            //实例化一个fop对象主要进行后续转码操作
            Qiniu.RS.Pfop fop = new Qiniu.RS.Pfop();

            Uri uri = new Uri(QiniuConfig.UploadCallback);
            try
            {
                string s = fop.Do(entry, new[] { pfops }, uri, pipeline);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return false;
        }

        public static bool VideoCutPiece(FileTypeDirEnum dir, string key)
        {
            Qiniu.Conf.Config.ACCESS_KEY = QiniuConfig.AccessKey;
            Qiniu.Conf.Config.SECRET_KEY = QiniuConfig.SecretKey;

            //设置空间
            string bucket = QiniuUtil.GetServerBucket(dir);

            //实例化一个entry对象
            EntryPath entry = new EntryPath(bucket, key);

            //设置切片操作参数
            String fops = "avthumb/m3u8/segtime/15/vb/240k";
            //设置切片的队列
            string pipeline = "yihui_queue";

            //可以对转码后的文件进行使用saveas参数自定义命名，当然也可以不指定文件会默认命名并保存在当前空间。
            String urlbase64 = Qiniu.Util.Base64URLSafe.Encode(String.Format("{0}:cut_{1}", bucket, key));
            String pfops = fops + "|saveas/" + urlbase64;

            //实例化一个fop对象主要进行后续转码操作
            Qiniu.RS.Pfop fop = new Qiniu.RS.Pfop();

            Uri uri = new Uri(QiniuConfig.UploadCallback);
            try
            {
                string s = fop.Do(entry, new[] { pfops }, uri, pipeline);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return false;
        }

    }
}
