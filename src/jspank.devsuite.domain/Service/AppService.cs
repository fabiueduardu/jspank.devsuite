using jspank.devsuite.domain.repository;

namespace jspank.devsuite.domain.service
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
