namespace JSpank.DevSuite.Domain.Service
{
    public interface IAppService
    {
        void CreateOrUpdate(bool forceNewDb = false);
    }
}
