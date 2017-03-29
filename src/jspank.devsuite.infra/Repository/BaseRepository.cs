using System;

namespace JSpank.DevSuite.Infra.Repository
{
    public abstract class BaseRepository : IDisposable
    {
        protected string db_path
        {
            get
            {
                return string.Concat(Environment.CurrentDirectory, @"\db.sqlite");
            }
        }

        public void Dispose()
        {
            
        }
    }
}
