﻿using Dapper;
using JSpank.DevSuite.Domain.Entitie;
using JSpank.DevSuite.Domain.Repository;
using JSpank.DevSuite.Domain.Service;
using System.Data.SQLite;
using System.Linq;

namespace JSpank.DevSuite.Infra.Repository
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
