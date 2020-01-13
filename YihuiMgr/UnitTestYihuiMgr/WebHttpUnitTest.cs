using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YihuiMgr.Http;

namespace UnitTestYihuiMgr
{
    [TestClass]
    public class WebHttpUnitTest
    {
        [TestMethod]
        public async void TestMethod1()
        {
            WebUtils controller = new WebUtils();
            string str = await controller.DoPost(null, null);
        }
    }
}
