using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TesteStream;

namespace TesteStream.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual('E', Program.firstChar(new TesteStream("aAbBABacfe")));
            Assert.AreEqual('U', Program.firstChar(new TesteStream("aAbBABacfu")));
            Assert.AreEqual('E', Program.firstChar(new TesteStream("aAbBABacfE")));
            Assert.AreEqual('I', Program.firstChar(new TesteStream("aAbBiABacfXAsaW")));
        }
    }
}
