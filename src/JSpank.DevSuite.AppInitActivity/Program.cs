using JSpank.DevSuite.Domain.Service;
using JSpank.DevSuite.Ioc;

namespace JSpank.DevSuite.AppInitActivity
{
    class Program
    {
        static void Main(string[] args)
        {
            var iocResolver = new IocResolver();
            var startActivityService = iocResolver.Resolve<IStartActivityService>();
            startActivityService.Start();
        }
    }
}
