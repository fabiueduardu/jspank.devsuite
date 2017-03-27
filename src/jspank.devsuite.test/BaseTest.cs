using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jspank.devsuite.test
{
    [TestClass]
    public class BaseTest
    {
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
            
        }
    }
}
