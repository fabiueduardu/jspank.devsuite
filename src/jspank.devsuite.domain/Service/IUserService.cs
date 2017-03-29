using JSpank.DevSuite.Domain.Entitie;

namespace JSpank.DevSuite.Domain.Service
{
    public interface IUserService
    {
        User Me { get; }
    }
}
