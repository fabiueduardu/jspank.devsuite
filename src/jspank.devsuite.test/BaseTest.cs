
using DryIoc;
using JSpank.DevSuite.Test.Core.Ioc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JSpank.DevSuite.Test
{
    [TestClass]
    public class BaseTest
    {
        Container _IocContainer;

        protected T Resolve<T>()
        {
            return this._IocContainer.Resolve<T>();
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
            IocContainer.Register(ref this._IocContainer);
        }
    }
}
