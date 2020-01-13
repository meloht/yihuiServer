using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YihuiMgr.Util
{
    public class CryptUtil
    {
        public static string EncryptDes(string data,string key)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0,8));
                //rgbIV与rgbKey可以不一样，这里只是为了简便，读者可以自行修改
                byte[] rgbIV = Encoding.UTF8.GetBytes(key.Substring(0, 8));

                byte[] inputByteArray = Encoding.UTF8.GetBytes(data);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();

                dCSP.Mode = CipherMode.ECB;

                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cStream.FlushFinalBlock();
                        
                    }
                    return Convert.ToBase64String(mStream.ToArray());
                }
            }
            catch
            {
                return "";
            }
        }
        public static string DecryptDes(string encryptedString, string key)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));

            byte[] btIV = Encoding.UTF8.GetBytes(key.Substring(0, 8));

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            des.Mode = CipherMode.ECB;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);

                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        public static string MD5(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (var item in result)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
