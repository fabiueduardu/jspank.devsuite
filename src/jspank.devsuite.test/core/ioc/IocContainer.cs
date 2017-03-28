using DryIoc;
using jspank.devsuite.domain.repository;
using jspank.devsuite.domain.service;
using jspank.devsuite.infra.repository;

namespace jspank.devsuite.test.core.ioc
{
    public class IocContainer
    {
        public static void Register(ref Container container)
        {
            container = container ?? new Container();

            var mode = Reuse.Singleton;
            container.Register<IUserRepository, UserRepository>(mode);
            container.Register<IUserService, UserService>(mode);
            container.Register<IAppService, AppService>(mode);
            container.Register<IAppRepository, AppRepository>(mode);
        }
    }
}
