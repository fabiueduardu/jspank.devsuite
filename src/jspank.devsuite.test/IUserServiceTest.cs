using jspank.devsuite.domain.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jspank.devsuite.test
{
    [TestClass]
    public class IUserServiceTest : BaseTest
    {
        [TestMethod]
        public void Me()
        {
            var service = base.Resolve<IUserService>();
            var result = service.Me;
            Assert.IsNotNull(result);
        }

        [TestInitialize]
        public void TestInitialize_Local()
        {
            var service = base.Resolve<IAppService>();
            service.CreateOrUpdate(true);
        }
    }
}
