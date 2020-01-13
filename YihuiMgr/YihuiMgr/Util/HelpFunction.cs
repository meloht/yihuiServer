using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YihuiMgr.Util
{
    public class HelpFunction
    {
        private static Random rdRandom = new Random();
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();



        public static string JsonEncode(object obj)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(obj, setting);
        }

        public static T ToObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static DateTime ConvertToDateTime(long timestamp)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan tsSpan = TimeSpan.FromSeconds(timestamp);

            var date = time.Add(tsSpan).ToLocalTime();
            return date;
        }

        public static long ConvertToTimestamp(DateTime dateTime)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan dt = dateTime.ToUniversalTime() - time.ToUniversalTime();

            long a = (long)(dt.TotalMilliseconds / 1000);
            return a;
        }

        public static DateTime? GetDateTime(long? date)
        {
            if (date.HasValue)
            {
                return ConvertToDateTime(date.Value);
            }
            return null;
        }

        public static int ConvertToInt(string text)
        {
            int num = 0;
            if (int.TryParse(text, out num))
                return num;
            return 0;
        }

        public static string GenerateFullGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

       

        /// <summary>
        /// 精确到毫秒
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeString()
        {
            string id = DateTime.Now.ToString("yyyyMMdd-HHmmssfff");
            return id;
        }

        

        /// <summary>
        /// 生成指定长度数字串，不足位数用0补齐
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string NextIntNumberRandom(int length)
        {
            int max = 1;
            for (int i = 0; i < length; i++)
            {
                max = max * 10;
            }
            int num = rdRandom.Next(max);
            if (num.ToString().Length < length)
            {
                return num.ToString().PadLeft(length, '0');
            }
            return num.ToString();
        }

        /// <summary>
        /// 唯一订单号生成 20位 后三位是数字与字母随机组合
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrderNumber()
        {
            string strDateTimeNumber = GenerateTimeString();
            if (strDateTimeNumber.Length < 17)
            {
                strDateTimeNumber = strDateTimeNumber.PadLeft(17, '0');
            }
            string strRandomResult = GetRandomString(3);
            return String.Format("{0}{1}", strDateTimeNumber, strRandomResult);
        }

       

        public static string GetRandomString(int stringlength)
        {
            return GetRandomString(null, stringlength);
        }

        //获得长度为stringlength 的随机字符串,以Keyset为字母表
        public static string GetRandomString(string keySet, int stringlength)
        {
            if (keySet == null || keySet.Length < 8) keySet = "abcdefghijklmnopqrstuvwxyz1234567890";
            int keySetLength = keySet.Length;
            StringBuilder str = new StringBuilder(keySetLength);
            for (int i = 0; i < stringlength; ++i)
            {
                str.Append(keySet[Random(keySetLength)]);
            }
            return str.ToString();
        }

        private static int Random(int maxValue)
        {
            decimal _base = (decimal)long.MaxValue;
            byte[] rndSeries = new byte[8];
            rng.GetBytes(rndSeries);
            return (int)(Math.Abs(BitConverter.ToInt64(rndSeries, 0)) / _base * maxValue);
        }




        public static string UrlEncoder(string input)
        {
            return System.Net.WebUtility.UrlEncode(input);
        }

        public static string UrlDecode(string input)
        {
            return System.Net.WebUtility.UrlDecode(input);
        }
    }
}
