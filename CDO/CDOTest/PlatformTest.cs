using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDO;
namespace CDOTest
{
using NUnit.Framework;
using System.Threading;

    [TestFixture]
    public class PlatformTest
    {

        public void tearDown()
        {
            //Console.WriteLine("PlatformTest.tearDown()\n");
            Platform.release();
        }

        [Test]
        public void testInit()
        {
            CountdownLatch latch = new CountdownLatch(1);
            InitListener listener = new InitListener(latch);
            Platform.init(listener);
            latch.Wait();
            Assert.AreEqual(listener.initState, InitStateChangedEvent.InitState.INITIALIZED);
            tearDown();
        }

        [Test]
        public void testGetService()
        {
            CountdownLatch latch = new CountdownLatch(1);
            InitListener listener = new InitListener(latch);
            Assert.IsNull(Platform.getService());
            Platform.init(listener);
            latch.Wait();
            Assert.IsNotNull(Platform.getService());
            tearDown();
        }
    
    
    }
}
