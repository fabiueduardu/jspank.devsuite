using JSpank.DevSuite.AppInitActivity.Services;
using JSpank.DevSuite.Domain.Abstraction;

namespace JSpank.DevSuite.AppInitActivity
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = null;
            new StartActivityService(logger).Start();
        }
    }
}
