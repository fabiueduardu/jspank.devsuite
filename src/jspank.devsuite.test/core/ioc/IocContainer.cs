using DryIoc;
using JSpank.DevSuite.Domain.Repository;
using JSpank.DevSuite.Domain.Service;
using JSpank.DevSuite.Infra.Repository;
using JSpank.DevSuite.Infra.Repository;

namespace JSpank.DevSuite.Test.Core.Ioc
{
    public class IocContainer
    {
        public static void Register(ref Container container)
        {
            container = container ?? new Container();

            var mode = Reuse.Singleton;

            //Repo
            container.Register<IUserRepository, UserRepository>(mode);
            container.Register<IAppRepository, AppRepository>(mode);
            container.Register<ITimeSheetRepository, TimeSheetRepository>(mode);

            //Services
            container.Register<IUserService, UserService>(mode);
            container.Register<IAppService, AppService>(mode);
            container.Register<ITimeSheetService, TimeSheetService>(mode);

        }
    }
}
