using JSpank.DevSuite.Domain.Entitie;
using JSpank.DevSuite.Domain.Service;
using JSpank.DevSuite.Infra.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSpank.DevSuite.Test
{
  //  [TestClass]
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
            var service = base.Resolve<IAppService>();
            service.CreateOrUpdate(true);
        }
    }
}
