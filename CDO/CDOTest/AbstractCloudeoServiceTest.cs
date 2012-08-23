using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CDO;
namespace CDOTest
{
    public class AbstractCloudeoServiceTest
    {

        protected CloudeoService _service;

        private CountdownLatch _latch;

        protected string _stringResult;

        protected Dictionary<string, string> _devsResult;

        private int _lastError;

        protected Responder<string> createStringResponder()
        {
            setupCall();
            return Platform.createResponder<string>(
                delegate(string result)
                {
                    _stringResult = result;
                    _latch.Signal();
                },
                delegate(int errCode, string errMessage)
                {
                    _lastError = errCode;
                    _latch.Signal();
                }
                );
        }

        protected Responder<object> createVoidResponder()
        {
            setupCall(); 
            return Platform.createResponder<object>(
                delegate(object result)
                {
                    _latch.Signal();
                },
                delegate(int errCode, string errMessage)
                {
                    _lastError = errCode;
                    _latch.Signal();
                }
                );
        }

        protected Responder<Dictionary<string,string>> createDevsResponder()
        {
            setupCall(); 
            return Platform.createResponder<Dictionary<string, string>>(
                delegate(Dictionary<string, string> result)
                {
                    _devsResult = result;
                    _latch.Signal();
                },
                delegate(int errCode, string errMessage)
                {
                    _lastError = errCode;
                    _latch.Signal();
                }
                );
        }



        protected string awaitStringResult(string method = "", int timeout = 2000)
        {
            waitAndCheckError(method, timeout);
            return _stringResult;
        }

        protected Dictionary<string, string> awaitDictResult(string method = "", 
            int timeout = 2000)
        {
            waitAndCheckError(method, timeout); 
            return _devsResult;
        }

        protected void awaitVoidResult(string method = "", int timeout = 2000)
        {
            waitAndCheckError(method, timeout);
        }

        private void setupCall()
        {
            _lastError = 0;
            _latch = new CountdownLatch();
        }

        private void waitAndCheckError(string method = "", int timeout = 2000)
        {
            Assert.That(_latch.Wait(timeout), "Timeout reached when waiting for result of" +
             " method call: " + method);
            Assert.AreEqual(0, _lastError, "Got error result in method: " + method);
        }

                
        [SetUp]
        protected void setUp()
        {
            CountdownLatch latch = new CountdownLatch(1);
            InitListener listener = new InitListener(latch);
            Platform.init(listener);
            latch.Wait();
            Assert.AreEqual(listener.initState, InitStateChangedEvent.InitState.INITIALIZED);
            _service = Platform.getService();
        }

        [TearDown]
        protected void tearDown()
        {
            Platform.release();
        }

    }
}
