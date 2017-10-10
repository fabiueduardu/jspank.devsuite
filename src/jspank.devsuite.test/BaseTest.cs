using JSpank.DevSuite.Ioc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JSpank.DevSuite.Test
{
    [TestClass]
    public class BaseTest
    {
        IocResolver iocResolver;

        protected T Resolve<T>()
        {
            return iocResolver.Resolve<T>();
        }

        protected string Key5
        {
            get
            {
                return this.Key(05);
            }
        }

        protected string KeyAll
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        protected string Key(int length = 5)
        {
            return KeyAll.Substring(0, length);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            iocResolver = new IocResolver();
        }
    }
}
