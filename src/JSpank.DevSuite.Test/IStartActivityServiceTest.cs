using JSpank.DevSuite.AppInitActivity.Services;
using JSpank.DevSuite.Domain.Abstraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSpank.DevSuite.Test
{
    [TestClass]
    public class IStartActivityServiceTest : BaseTest
    {
        [TestMethod]
        public void Start()
        {
            var logger = base.Resolve<ILogger>();
            //   var service = base.Resolve<IStartActivityService>();

            var service = new StartActivityService(logger);//TODO - ioc this;
            service.Start();
        }
    }
}
