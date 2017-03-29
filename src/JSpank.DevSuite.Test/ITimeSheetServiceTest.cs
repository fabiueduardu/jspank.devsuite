using JSpank.DevSuite.Domain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace JSpank.DevSuite.Test
{
    [TestClass]
    public class ITimeSheetServiceTest : BaseTest
    {
        [TestMethod]
        public void Add()
        {
            this._Add();
        }

        [TestMethod]
        public void AddActivity()
        {
            this._AddActivity();
        }

        [TestMethod]
        public void AddActivity_10()
        {
            this._AddActivity(10);
        }

        [TestMethod]
        public void SearchActivity()
        {
            this.AddActivity_10();

            var service = base.Resolve<ITimeSheetService>();
            var result = service.SearchActivity(UtilService.Date.Date, UtilService.Date.Date.AddDays(1).AddSeconds(-1));
            Assert.IsTrue(result.Any());

        }

        public void _Add(int total = 1)
        {
            var service = base.Resolve<ITimeSheetService>();

            for (int i = 0; i < total; i++)
            {
                var result = service.Add(UtilService.Date.AddMinutes(i * 10));
                Assert.IsNotNull(result);
            }
        }

        public void _AddActivity(int total = 1)
        {
            var service = base.Resolve<ITimeSheetService>();

            for (int i = 0; i < total; i++)
            {
                var result = service.AddActivity(UtilService.Date.AddMinutes(i * 10), base.KeyAll);
                Assert.IsNotNull(result);
            }
        }

        [TestInitialize]
        public void TestInitialize_Local()
        {
            var service = base.Resolve<IAppService>();
            service.CreateOrUpdate(true);
        }
    }
}
