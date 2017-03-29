using System;

namespace JSpank.DevSuite.Domain.Entitie
{
    public class Version
    {
        public int cd_version { get; set; }
        public string nm_version { get; set; }
        public string dc_version { get; set; }
        public string dc_query { get; set; }
        public DateTime dh_create { get; set; }
    }
}
