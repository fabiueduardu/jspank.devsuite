using jspank.devsuite.domain.Entitie;

namespace jspank.devsuite.domain.Repository
{
    public interface IUserRepository
    {
        bool Add(User model);

        User Get(string lg_user);
    }
}
