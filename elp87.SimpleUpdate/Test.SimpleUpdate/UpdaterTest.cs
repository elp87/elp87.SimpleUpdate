using elp87.SimpleUpdate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.SimpleUpdate
{
    [TestClass]
    public class UpdaterTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Updater upd = new Updater();
            upd.CheckUpdate();
        }
    }
}
