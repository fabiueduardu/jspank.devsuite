namespace JSpank.DevSuite.Domain.Repository
{
    public interface IAppRepository
    {
        void CreateOrUpdate(bool forceNewDb = false);
    }
}
