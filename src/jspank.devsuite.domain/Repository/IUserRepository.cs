using JSpank.DevSuite.Domain.Entitie;

namespace JSpank.DevSuite.Domain.Repository
{
    public interface IUserRepository
    {
        bool Add(User model);

        User Get(string lg_user);
    }
}
