using System.Data.SQLite;
using Dapper;
using System.IO;

namespace jspank.devsuite.infra.Repository
{
    public class AppRepository : BaseRepository
    {
        public bool Create(bool forceRenew = false)
        {
            var query =
            @"
                  CREATE TABLE IF NOT EXISTS version
                      (
                         cd_version INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                         nm_version VARCHAR(10) NOT NULL,
                         dc_version VARCHAR(100) NOT NULL,
                         dh_create DATETIME NOT NULL
                    );

                CREATE TABLE IF NOT EXISTS user
                      (
                         cd_user INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                         lg_user VARCHAR(100) NOT NULL,
                         dh_create DATETIME NOT NULL
                    );

                CREATE UNIQUE INDEX IF NOT EXISTS ix_un_user ON user (lg_user)
    
            ";

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", this.InitializeDb(forceRenew))))
                dbCon.Execute(query);

            return true;
        }

        string InitializeDb(bool forceRenew)
        {
            if (forceRenew && File.Exists(base.db_path))
                File.Delete(base.db_path);

            return base.db_path;
        }
    }
}
