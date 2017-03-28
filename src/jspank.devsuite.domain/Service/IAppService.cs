namespace jspank.devsuite.domain.service
{
    public interface IAppService
    {
        void CreateOrUpdate(bool forceNewDb = false);
    }
}
