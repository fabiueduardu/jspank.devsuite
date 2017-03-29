using JSpank.DevSuite.Domain.Repository;

namespace JSpank.DevSuite.Domain.Service
{
    public class AppService : IAppService
    {
        readonly IAppRepository _IAppRepository;

        public AppService(IAppRepository _IAppRepository)
        {
            this._IAppRepository = _IAppRepository;
        }

        public void CreateOrUpdate(bool forceNewDb = false)
        {
            this._IAppRepository.CreateOrUpdate(forceNewDb);
        }
    }
}
