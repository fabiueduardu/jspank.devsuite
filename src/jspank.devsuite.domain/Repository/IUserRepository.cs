using jspank.devsuite.domain.entitie;

namespace jspank.devsuite.domain.repository
{
    public interface IUserRepository
    {
        bool Add(User model);

        User Get(string lg_user);
    }
}
