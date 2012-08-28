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
using System.Threading;

namespace CDOTest
{
    [TestFixture]
    class CloudeoServiceTest : AbstractCloudeoServiceTest
    {

        [Test]
        public void testGetVersion()
        {
            Responder<string> responder = createStringResponder(); 
            _service.getVersion(responder);
            Assert.IsTrue(awaitStringResult().Length > 4);
        }


        [Test]
        public void testStartStopLocalVideo()
        {
            // 1. Set proper video capture device
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getVideoCaptureDeviceNames", 15000);
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setVideoCaptureDevice", 15000);

            // 2. Do the real test

            _service.startLocalVideo(createStringResponder());
            string sinkId = awaitStringResult("startLocalVideo", 15000);
            Assert.That(sinkId.Length > 0);

            Thread.Sleep(5000);
            _service.stopLocalVideo(createVoidResponder());
            awaitVoidResult();
        }
    }
}