/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

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

        protected int _intResult;

        protected Dictionary<string, string> _devsResult;
        protected List<ScreenCaptureSource> _scrSourcesResult;

        protected CloudeoServiceEventDispatcher dispatcher;
        private int _lastError;
        private string _lastErrMessage;
        private ManualRenderer _mRendererResult;
        private RenderingWidget _renderingWidgetResult;

        private void errHandler(int errCode, string errMessage)
        {
            _lastError = errCode;
            _lastErrMessage = errMessage;
            _latch.Signal();
        }

        protected Responder<string> createStringResponder()
        {
            setupCall();
            return Platform.createResponder<string>(
                delegate(string result)
                {
                    _stringResult = result;
                    _latch.Signal();
                }, errHandler
                );
        }

        protected Responder<int> createIntResponder()
        {
            setupCall();
            return Platform.createResponder<int>(
                delegate(int result)
                {
                    _intResult = result;
                    _latch.Signal();
                },
                errHandler
                );
        }

        protected Responder<ManualRenderer> createManualRendererResponder()
        {
            setupCall();
            return Platform.createResponder<ManualRenderer>(
                delegate(ManualRenderer result)
                {
                    _mRendererResult = result;
                    _latch.Signal();
                },
                errHandler
                );
        }

        protected Responder<RenderingWidget> createRendererResponder()
        {
            setupCall();
            return Platform.createResponder<RenderingWidget>(
                delegate(RenderingWidget result)
                {
                    _renderingWidgetResult = result;
                    _latch.Signal();
                },
                errHandler
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
                errHandler
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
                errHandler
                );
        }

        protected Responder<List<ScreenCaptureSource>> createScrSourcesResponder()
        {
            setupCall();
            return Platform.createResponder<List<ScreenCaptureSource>>(
                delegate(List<ScreenCaptureSource> result)
                {
                    _scrSourcesResult = result;
                    _latch.Signal();
                },
                errHandler
                );
        }


        protected string awaitStringResult(string method = "", int timeout = 2000)
        {
            waitAndCheckError(method, timeout);
            return _stringResult;
        }

        protected int awaitIntResult(string method = "", int timeout = 2000)
        {
            waitAndCheckError(method, timeout);
            return _intResult;
        }

        protected Dictionary<string, string> awaitDictResult(string method = "",
            int timeout = 2000)
        {
            waitAndCheckError(method, timeout);
            return _devsResult;
        }

        protected List<ScreenCaptureSource> awaitScrSourcesResult(int timeout = 2000)
        {
            waitAndCheckError("getScreenCaptureSources", timeout);
            return _scrSourcesResult;
        }

        protected void awaitVoidResult(string method = "", int timeout = 2000)
        {
            waitAndCheckError(method, timeout);
        }

        protected ManualRenderer awaitManualRendererResult(string method = "", int timeout = 2000)
        {
            waitAndCheckError(method, timeout);
            return _mRendererResult;
        }


        protected RenderingWidget awaitRendererResult(string method = "", int timeout = 2000)
        {
            waitAndCheckError(method, timeout);
            return _renderingWidgetResult;
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
            Assert.AreEqual(0, _lastError, "Got error result in method: " + method + 
                " err message: " + _lastErrMessage);
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
            dispatcher = new CloudeoServiceEventDispatcher();
            _service.addServiceListener(createVoidResponder(), dispatcher);
            awaitVoidResult("addServiceListener");
        }

        [TearDown]
        protected void tearDown()
        {
            Platform.release();
        }

    }
}
