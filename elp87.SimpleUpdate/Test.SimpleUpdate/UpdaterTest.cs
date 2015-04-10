using elp87.SimpleUpdate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.SimpleUpdate
{
    [TestClass]
    public class UpdaterTest
    {
        Updater upd;

        public UpdaterTest()
        {
            int curBuild = 10;
            upd = new Updater(curBuild);
            upd.CheckUpdate();
        }

        [TestMethod]
        public void TestStableBuildNumber()
        {
            int expStableBuild = 20;
            Assert.AreEqual(expStableBuild, upd.StableBuildNumber);
        }

        [TestMethod]
        public void TestStableLink()
        {
            string expStableLink = "http://google.com";
            Assert.AreEqual(expStableLink, upd.StableLink);
        }

        [TestMethod]
        public void TestStableNews()
        {
            string expStableNews = "Новости";
            Assert.AreEqual(expStableNews, upd.StableNews);
        }

        [TestMethod]
        public void TestBetaBuildNumber()
        {
            int expBetaBuild = 23;
            Assert.AreEqual(expBetaBuild, upd.BetaBuildNumber);
        }

        [TestMethod]
        public void TestBetaLink()
        {
            string expBetaLink = "http://yandex.ru";
            Assert.AreEqual(expBetaLink, upd.BetaLink);
        }

        [TestMethod]
        public void TestBetaNews()
        {
            string expBetaNews = "Бета новости";
            Assert.AreEqual(expBetaNews, upd.BetaNews);
        }
    }
}
