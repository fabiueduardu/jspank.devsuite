using System;

namespace JSpank.DevSuite.Domain.Entitie
{
    public class TimeSheet
    {
        public int cd_time { get; set; }
        public int cd_user { get; set; }
        public DateTime dh_input { get; set; }
        public DateTime dh_create { get; set; }
    }
}
