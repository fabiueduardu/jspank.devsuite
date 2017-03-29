using System;

namespace JSpank.DevSuite.Domain.Entitie
{
    public class TimeSheetActivity
    {
        public int cd_activity { get; set; }
        public int cd_user { get; set; }
        public DateTime dh_input { get; set; }
        public string dc_activity { get; set; }
    }
}
