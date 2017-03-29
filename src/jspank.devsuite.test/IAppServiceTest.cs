using JSpank.DevSuite.Domain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSpank.DevSuite.Test
{
    [TestClass]
    public class IAppServiceTest : BaseTest
    {
        [TestMethod]
        public void CreateOrUpdate()
        {
            var service = base.Resolve<IAppService>();
            service.CreateOrUpdate(true);
        }

        [TestMethod]
        public void Update()
        {
            var service = base.Resolve<IAppService>();
            service.CreateOrUpdate(true);
            service.CreateOrUpdate(false);
            service.CreateOrUpdate(false);
        }
    }
}
