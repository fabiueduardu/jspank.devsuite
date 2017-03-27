using Microsoft.VisualStudio.TestTools.UnitTesting;
using jspank.devsuite.infra.Repository;

namespace jspank.devsuite.test
{
    [TestClass]
    public class AppRepositoryTest : BaseTest
    {
        [TestMethod]
        public void create()
        {
            using (var repository = new AppRepository())
            {
                var result = repository.Create(true);
                Assert.IsTrue(result);
            }
        }

    }
}
