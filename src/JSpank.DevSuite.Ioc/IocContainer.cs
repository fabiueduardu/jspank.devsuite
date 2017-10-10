using DryIoc;
using JSpank.DevSuite.Domain.Abstraction;
using JSpank.DevSuite.Domain.Repository;
using JSpank.DevSuite.Domain.Service;
using JSpank.DevSuite.Infra.Repository;


namespace JSpank.DevSuite.Ioc
{
    internal class IocContainer
    {
        public void Register(ref Container container)
        {
            container = container ?? new Container();

            var mode = Reuse.Singleton;

            //Abstraction
            container.Register<ILogger, Logger>(mode);

            //Repo
            container.Register<IUserRepository, UserRepository>(mode);
            container.Register<IAppRepository, AppRepository>(mode);
            container.Register<ITimeSheetRepository, TimeSheetRepository>(mode);

            //Services
            container.Register<IUserService, UserService>(mode);
            container.Register<IAppService, AppService>(mode);
            container.Register<ITimeSheetService, TimeSheetService>(mode);
            container.Register<IStartActivityService, StartActivityService>(mode);

        }
    }
}
