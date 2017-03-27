using Microsoft.VisualStudio.TestTools.UnitTesting;
using jspank.devsuite.infra.Repository;
using jspank.devsuite.domain.Entitie;

namespace jspank.devsuite.test
{
    [TestClass]
    public class UserRepositoryTest : BaseTest
    {
        [TestMethod]
        public void add_1()
        {
            this.add();
        }

        [TestMethod]
        public void add_100()
        {
            this.add(100);
        }

        [TestMethod]
        public void add(int total = 1)
        {
            using (var repository = new UserRepository())
            {
                for (var i = 0; i < total; i++)
                {
                    var model = new User
                    {
                        lg_user = string.Concat(base.Key5, i)
                    };
                    var result = repository.Add(model);
                    Assert.IsTrue(result);
                }
            }
        }

        [TestInitialize]
        public void TestInitialize_Local()
        {
            new AppRepositoryTest().create();
        }
    }
}
