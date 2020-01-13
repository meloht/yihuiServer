using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YihuiMgr.Data;
using YihuiMgr.DataMgr;
using YihuiMgr.Util;
using YihuiMgr.ViewModel.Order;
using YihuiQiniuSdk;
using YihuiQiniuSdk.Data;

namespace UnitTestYihuiMgr
{
    [TestClass]
    public class UnitHelpFunction
    {
        [TestMethod]
        public void TestDateTimeTolong()
        {
            DateTime dateTime = new DateTime(2016, 4, 5, 12, 33, 42);
            var value = HelpFunction.ConvertToTimestamp(dateTime);
            var valued = HelpFunction.ConvertToDateTime(1463474565);
            Assert.AreEqual(valued, dateTime);
        }

        [TestMethod]
        public void TestGenerateId()
        {
            List<string> ls = new List<string>();
            int count = 0;
            for (int i = 0; i < 1000; i++)
            {
                var id = QiniuUtil.GenerateGuid();
                if (ls.Contains(id))
                {
                    count++;
                }
                ls.Add(id);
            }
            Assert.AreEqual(count, 0);
        }


        [TestMethod]
        public void TestGenerateOrderId()
        {

            List<string> ls = new List<string>();
            int count = 0;
            for (int i = 0; i < 1000; i++)
            {
                var id = HelpFunction.GenerateOrderNumber();
                if (ls.Contains(id))
                {
                    count++;
                }
                ls.Add(id);
            }
            Assert.AreEqual(count, 0);
        }
        [TestMethod]
        public void TestVideoCut()
        {
            string id = QiniuUtil.GenerateGuid();
            var bl = ServerFileMgr.UploadFileToServer(id + "test58.mp4", @"D:\VID_20160429_130223.mp4",
                FileTypeDirEnum.CrowdFundVideoDir, false, null);
            Assert.AreEqual(bl.IsSuccess, true);
            //ServerFileMgr.VideoCutPiece(FileTypeDirEnum.VideoDir, "915d46db795459d6.mp4");

        }
        [TestMethod]
        public void TestVideoImg()
        {
            SearchModel model = new SearchModel();
            ; model.Id = 12;
            var s = HelpFunction.JsonEncode(model);
            //string file = "";
            //bool bl = ServerFileMgr.GetVideoThumbnailImg(FileTypeDirEnum.CrowdFundImgDir, "69937f4589276df4test.mp4", out file);
            //Assert.AreEqual(true, bl);
        }

        [TestMethod]
        public void TestEf()
        {
            string ad = CryptUtil.MD5("123456");
            Assert.AreEqual("e10adc3949ba59abbe56e057f20f883e", ad);
        }
        [TestMethod]
        public void TestDes()
        {
            String skey = "1234567890123456";
            //string ddd = Convert.ToBase64String(Encoding.UTF8.GetBytes("1231313"));
            string dd = CryptUtil.EncryptDes("15611802686", skey);
            string cc = CryptUtil.DecryptDes("SWRjRMW1EH6SiDtUq2Pj+4Mtscup+IFzsu4aj4REJZ9cgSI0zD4kEQ==", skey);
            Assert.AreEqual("Z4bEu0glHasENUzgYotuiw==", dd);
        }

        [TestMethod]
        public void TestTime()
        {
            int num = 570;
            int hh = num%24;
            int day = (575 - hh)/24;
            Assert.AreEqual(hh>0,true);
        }
    }
}
