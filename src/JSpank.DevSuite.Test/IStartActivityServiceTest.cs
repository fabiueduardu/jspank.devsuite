using JSpank.DevSuite.Domain.Abstraction;
using JSpank.DevSuite.Domain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSpank.DevSuite.Test
{
    [TestClass]
    public class IStartActivityServiceTest : BaseTest
    {
        [TestMethod]
        public void Start()
        {
            var service = base.Resolve<IStartActivityService>();
            service.Start();
        }
    }
}
