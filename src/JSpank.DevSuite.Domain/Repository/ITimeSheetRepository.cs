using JSpank.DevSuite.Domain.Entitie;
using System;
using System.Collections.Generic;

namespace JSpank.DevSuite.Domain.Repository
{
    public interface ITimeSheetRepository
    {
        bool Add(TimeSheet model);

        bool AddActivity(TimeSheetActivity model);

        IEnumerable<TimeSheet> Search(DateTime dh_input_start, DateTime dh_input_end);

        IEnumerable<TimeSheetActivity> SearchActivity(DateTime dh_input_start, DateTime dh_input_end);
    }
}
