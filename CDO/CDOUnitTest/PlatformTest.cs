using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using CDO;
using NUnit.Framework;
using System.Threading;

namespace CDOUnitTest
{
    

    [TestFixture]
    public class PlatformTest
    {

        //class InitListener : CDO.PlatformInitListener
        //{

        //    public InitListener(CountdownEvent latch)
        //    {
        //        _latch = latch;
        //    }

        //    public void onInitProgressChanged(InitProgressChangedEvent e)
        //    {

        //    }


        //    public void onInitStateChanged(InitStateChangedEvent e)
        //    {
        //        _initState = e.state;
        //        _latch.Signal();
        //    }

        //    public InitStateChangedEvent.InitState initState
        //    {
        //        get { return this._initState; }
        //    }

        //    private InitStateChangedEvent.InitState _initState;
        //    private CountdownEvent _latch;

        //}

        [Test]
        public void testInit()
        {
            //CountdownEvent countDown = new CountdownEvent(1);
            //InitListener listener = new InitListener(countDown);   
            //CDO.Platform.init(listener);
            //countDown.Wait();
            Assert.IsTrue(false);
        }
    
    
    }
    

}
