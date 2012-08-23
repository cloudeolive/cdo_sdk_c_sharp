using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CDO;

namespace CDOTest
{
    [TestFixture]
    class CloudeoServiceTest
    {

        private CloudeoService _service;

        [TestFixtureSetUp]
        public void setUp()
        {
            CountdownLatch latch = new CountdownLatch(1);
            InitListener listener = new InitListener(latch);
            Platform.init(listener);
            latch.Wait();
            Assert.AreEqual(listener.initState, InitStateChangedEvent.InitState.INITIALIZED);
            _service = Platform.getService();
        }

        [TestFixtureTearDown]
        public void tearDown()
        {
            Platform.release();
        }

        [Test]
        public void testGetVersion()
        {
            CountdownLatch latch = new CountdownLatch();
            string version = "";
            _service.getVersion(Platform.createResponder<string>(
                delegate(string result)
                {
                    version = result;
                    latch.Signal();
                }
                ));
            latch.Wait();
            Assert.IsTrue(version.Length > 4);
        }

    }
}
