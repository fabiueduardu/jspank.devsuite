
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jspank.devsuite.test.core.ioc;
using jspank.devsuite.domain.service;
using DryIoc;

namespace jspank.devsuite.test
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
