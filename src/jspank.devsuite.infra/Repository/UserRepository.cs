using System.Data.SQLite;
using Dapper;
using jspank.devsuite.domain.Entitie;
using jspank.devsuite.domain.Service;
using System.Linq;
using jspank.devsuite.domain.Repository;

namespace jspank.devsuite.infra.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public bool Add(User model)
        {
            var query =
            @"
                INSERT INTO user(lg_user,dh_create) VALUES(@lg_user,@dh_create)
            ";

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", base.db_path)))
                return dbCon.Execute(query, new { lg_user = model.lg_user, dh_create = UtilService.Date }) > 0;
        }

        public User Get(string lg_user)
        {
            var query =
            @"
                SELECT * FROM user WHERE lg_user = @lg_user
            ";

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", base.db_path)))
            {
                var result = dbCon.Query<User>(query, new { lg_user = lg_user });
                return result.FirstOrDefault();
            }
        }
    }
}
