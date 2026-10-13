using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImportFileParser.Loaders;

namespace ImportParserTests
{
    [TestClass]
    public class Pop3ClientTests
    {
        CPop3Loader m_Conv = new CPop3Loader("WLT", "pop.gmail.com", 995, "someuser@gmail.com", "pwd");

        [TestMethod]
        public void TestConnection()
        {
            //m_Conv.Load();
        }
    }
}
