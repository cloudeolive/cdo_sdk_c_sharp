using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using CDO;

namespace CDOTest
{
    [TestFixture]
    class CloudeoServiceListenerTest: AbstractCloudeoServiceTest
    {

        class MockEventListener:CloudeoServiceListenerAdapter
        {

            public EchoEvent receivedEvent;


            private CountdownLatch _latch;

            public MockEventListener(CountdownLatch latch)
            {
                _latch = latch;
            }

            public override void onEchoEvent(EchoEvent e) 
            {
                receivedEvent = e;
                _latch.Signal();
            }
        }

        [Test]
        public void testEchoNotification()
        {
                
            CountdownLatch latch = new CountdownLatch();
            MockEventListener listener = new MockEventListener(latch);
            _service.addServiceListener(createVoidResponder(), listener);
            awaitVoidResult("addServiceListener");
            _service.sendEchoNotification(createVoidResponder(), "whatever");
            Console.WriteLine("Notification sent");
            awaitVoidResult();
            Assert.IsTrue(latch.Wait(), "Got timeout when waiting for the event");
            Assert.IsNotNull(listener.receivedEvent);
            Assert.AreEqual("whatever", listener.receivedEvent.echoValue);
        }

    }
}
