using JSpank.DevSuite.Domain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSpank.DevSuite.Test
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
