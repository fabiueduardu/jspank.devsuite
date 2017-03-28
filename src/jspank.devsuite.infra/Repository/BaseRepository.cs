using System;

namespace jspank.devsuite.infra.repository
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
            //   throw new NotImplementedException();
        }
    }
}
