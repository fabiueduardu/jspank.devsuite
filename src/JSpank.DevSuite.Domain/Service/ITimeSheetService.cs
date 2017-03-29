using JSpank.DevSuite.Domain.Entitie;
using System;
using System.Collections.Generic;

namespace JSpank.DevSuite.Domain.Service
{
    public interface ITimeSheetService
    {
        bool Add(DateTime dh_input);

        bool AddActivity(DateTime dh_input, string dc_activity);

        IEnumerable<TimeSheet> Search(DateTime dh_input_start, DateTime dh_input_end);

        IEnumerable<TimeSheetActivity> SearchActivity(DateTime dh_input_start, DateTime dh_input_end);

    }
}
