using jspank.devsuite.domain.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jspank.devsuite.test
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
