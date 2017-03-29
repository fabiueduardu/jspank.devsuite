using Dapper;
using JSpank.DevSuite.Domain.Entitie;
using JSpank.DevSuite.Domain.Repository;
using JSpank.DevSuite.Domain.Service;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace JSpank.DevSuite.Infra.Repository
{
    public class TimeSheetRepository : BaseRepository, ITimeSheetRepository
    {
        public bool Add(TimeSheet model)
        {
            var query =
            @"
                INSERT INTO timesheet(cd_user,dh_input,dh_create) VALUES(@cd_user,@dh_input,@dh_create)
            ";

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", base.db_path)))
                return dbCon.Execute(query, new { cd_user = model.cd_user, dh_input = model.dh_input, dh_create = UtilService.Date }) > 0;
        }
        
        public bool AddActivity(TimeSheetActivity model)
        {
            var query =
           @"
                INSERT INTO timesheet_activity(cd_user,dh_input,dc_activity) VALUES(@cd_user,@dh_input,@dc_activity)
            ";

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", base.db_path)))
                return dbCon.Execute(query, new { cd_user = model.cd_user, dh_input = model.dh_input, dc_activity = model.dc_activity}) > 0;

        }

        public IEnumerable<TimeSheet> Search(DateTime dh_input_start, DateTime dh_input_end)
        {
            var query =
            @"
                SELECT * FROM timesheet WHERE dh_input >= @dh_input_start AND dh_input <= @dh_input_end
            ";

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", base.db_path)))
            {
                var result = dbCon.Query<TimeSheet>(query, new { dh_input_start = dh_input_start, dh_input_end = dh_input_end });
                return result;
            }
        }

        public IEnumerable<TimeSheetActivity> SearchActivity(DateTime dh_input_start, DateTime dh_input_end)
        {
            var query =
           @"
                SELECT * FROM timesheet_activity WHERE dh_input >= @dh_input_start AND dh_input <= @dh_input_end
            ";

            using (var dbCon = new SQLiteConnection(string.Concat("Data Source=", base.db_path)))
            {
                var result = dbCon.Query<TimeSheetActivity>(query, new { dh_input_start = dh_input_start, dh_input_end = dh_input_end });
                return result;
            }
        }
    }
}
