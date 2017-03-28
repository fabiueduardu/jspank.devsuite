namespace jspank.devsuite.domain.repository
{
    public interface IAppRepository
    {
        void CreateOrUpdate(bool forceNewDb = false);
    }
}
