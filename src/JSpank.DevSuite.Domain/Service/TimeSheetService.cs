using JSpank.DevSuite.Domain.Entitie;
using JSpank.DevSuite.Domain.Repository;
using System;
using System.Collections.Generic;

namespace JSpank.DevSuite.Domain.Service
{
    public class TimeSheetService : ITimeSheetService
    {
        readonly ITimeSheetRepository _ITimeSheetRepository;
        readonly IUserService _IUserService;

        public TimeSheetService(ITimeSheetRepository _ITimeSheetRepository, IUserService _IUserService)
        {
            this._ITimeSheetRepository = _ITimeSheetRepository;
            this._IUserService = _IUserService;
        }

        public bool Add(DateTime dh_input)
        {
            var model = new TimeSheet
            {
                dh_input = dh_input,
                cd_user = this._IUserService.Me.cd_user
            };
            return this._ITimeSheetRepository.Add(model);
        }

        public bool AddActivity(DateTime dh_input, string dc_activity)
        {
            var model = new TimeSheetActivity
            {
                dh_input = dh_input,
                cd_user = this._IUserService.Me.cd_user,
                dc_activity = dc_activity
            };

            return this._ITimeSheetRepository.AddActivity(model);
        }

        public IEnumerable<TimeSheet> Search(DateTime dh_input_start, DateTime dh_input_end)
        {
            return this._ITimeSheetRepository.Search(dh_input_start, dh_input_end);
        }

        public IEnumerable<TimeSheetActivity> SearchActivity(DateTime dh_input_start, DateTime dh_input_end)
        {
            return this._ITimeSheetRepository.SearchActivity(dh_input_start, dh_input_end);
        }
    }
}
