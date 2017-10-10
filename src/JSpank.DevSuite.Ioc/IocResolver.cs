using DryIoc;
using System;

namespace JSpank.DevSuite.Ioc
{
    public class IocResolver : IDisposable
    {
        static Container _IocContainer;

        public IocResolver()
        {
            new IocContainer().Register(ref _IocContainer);
        }

        public T Resolve<T>()
        {
            return _IocContainer.Resolve<T>();
        }

        public void Dispose()
        {
            _IocContainer.Dispose();
        }
    }
}
