using Dapper;
using JSpank.DevSuite.Domain.Entitie;
using JSpank.DevSuite.Domain.Repository;
using JSpank.DevSuite.Domain.Service;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace JSpank.DevSuite.Infra.Repository
{
    public class AppRepository : BaseRepository, IAppRepository
    {
        public void CreateOrUpdate(bool forceNewDb = false)
        {
            var alreadyExistsDb = false;
            var db_path = InitializeDb(forceNewDb, out alreadyExistsDb);

            if (!alreadyExistsDb)
                this.Create();
            else
                this.Update();
        }

        void Create(bool forceNewDb = false)
        {
            var query =
                  @"
                CREATE TABLE IF NOT EXISTS version
                      (
                         cd_version INTEGER NOT NULL PRIMARY KEY,
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
                 CREATE UNIQUE INDEX IF NOT EXISTS ix_un_user ON user (lg_user);

                 CREATE TABLE IF NOT EXISTS timesheet
                      (
                         cd_time INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                         cd_user INTEGER NOT NULL, 
                         dh_input DATETIME NOT NULL,
                         dh_create DATETIME NOT NULL,

                         FOREIGN KEY(cd_user) REFERENCES user(cd_user)
                      );
                CREATE UNIQUE INDEX IF NOT EXISTS ix_timesheet ON timesheet (dh_input);

                CREATE TABLE IF NOT EXISTS timesheet_activity
                      (
                         cd_activity INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                         cd_user INTEGER NOT NULL,
                         dh_input DATETIME NOT NULL,
                         dc_activity VARCHAR(200) NOT NULL,

                         FOREIGN KEY(cd_user) REFERENCES user(cd_user)
                      );
                CREATE UNIQUE INDEX IF NOT EXISTS ix_timesheet_activity ON timesheet_activity (dh_input);
            ";

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", base.db_path)))
                dbCon.Execute(query);

            this.AddVersion(1, "1.0", "Install");
        }

        void Update()
        {
            var collection = new Collection<Version>();

            collection.Add(new Version { cd_version = 1 });
            collection.Add(new Version { cd_version = 2, nm_version = "1.1", dc_version = "Refresh", dc_query = "" });

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", base.db_path)))
            {
                var cd_version = dbCon.ExecuteScalar<int>("SELECT max(cd_version) FROM version");

                foreach (var version in collection.Where(a => a.cd_version > cd_version).OrderBy(a => a.cd_version))
                    this.AddVersion(version.cd_version, version.nm_version, version.dc_version);

            }
        }

        bool AddVersion(int cd_version, string nm_version, string dc_version)
        {
            var query =
            @"
                INSERT INTO version(cd_version,nm_version,dc_version,dh_create) VALUES(@cd_version,@nm_version,@dc_version,@dh_create)
            ";

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", base.db_path)))
                return dbCon.Execute(query, new { cd_version = cd_version, nm_version = nm_version, dc_version = dc_version, dh_create = UtilService.Date }) > 0;
        }

        string InitializeDb(bool forceNewDb, out bool alreadyExistsDb)
        {
            alreadyExistsDb = File.Exists(base.db_path);
            if (forceNewDb && alreadyExistsDb)
            {
                File.Delete(base.db_path);
                alreadyExistsDb = false;
            }

            return base.db_path;
        }
    }
}
