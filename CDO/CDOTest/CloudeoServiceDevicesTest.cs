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
    class CloudeoServiceDevicesTest : AbstractCloudeoServiceTest
    {

        /*
         * Video devices
         * ==========================================================================
         */

        [Test]
        public void testGetVideoCaptureDeviceNames()
        {
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult("getVideoCaptureDeviceNames", 5000).Count > 0);
        }

        [Test]
        public void testSetVideoCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getVideoCaptureDeviceNames", 5000);
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setVideoCaptureDevice", 5000);
        }


        [Test]
        public void testGetVideoCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getVideoCaptureDeviceNames", 5000);
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setVideoCaptureDevice", 5000);
            _service.getVideoCaptureDevice(createStringResponder());
            Assert.AreEqual(devs.Keys.First(),
                awaitStringResult("getVideoCaptureDevice", 5000));
        }


        /*
         * Audio capture devices
         * ==========================================================================
         */

        [Test]
        public void testGetAudioCaptureDeviceNames()
        {
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult("getAudioCaptureDeviceNames").Count > 0);
        }

        [Test]
        public void testSetAudioCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioCaptureDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioCaptureDevice");
        }


        [Test]
        public void testGetAudioCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioCaptureDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioCaptureDevice");
            _service.getAudioCaptureDevice(createStringResponder());
            Assert.AreEqual(devs.Keys.First(), awaitStringResult("getAudioCaptureDevice"));
        }

        [Test]
        public void testStartStopMonitoringMicActivity()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioCaptureDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioCaptureDevice");
            _service.monitorMicActivity(createVoidResponder(), true);
            awaitVoidResult("monitorMicActivity");
            int notifications = 0;
            dispatcher.MicActivity += delegate(object sender, MicActivityEvent e) {
                notifications++;
            };
            Thread.Sleep(5000);
            _service.monitorMicActivity(createVoidResponder(), false);
            awaitVoidResult("monitorMicActivity");
            Assert.Greater(notifications, 0);            
        }

        /*
         * Audio output devices
         * ==========================================================================
         */

        [Test]
        public void testGetAudioOutputDeviceNames()
        {
            _service.getAudioOutputDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult("getAudioOutputDeviceNames").Count > 0);
        }

        [Test]
        public void testSetAudioOutputDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioOutputDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioOutputDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioOutputDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioOutputDevice");
        }


        [Test]
        public void testGetAudioOutputDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioOutputDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioOutputDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioOutputDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioOutputDevice");
            _service.getAudioOutputDevice(createStringResponder());
            Assert.AreEqual(devs.Keys.First(), awaitStringResult("getAudioOutputDevice"));
        }

        [Test]
        public void testSetGetVolume()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioOutputDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioOutputDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioOutputDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioOutputDevice");
            _service.setSpeakersVolume(createVoidResponder(), 123);
            awaitVoidResult("setSpeakersVolume");
            _service.getSpeakersVolume(createIntResponder());
            Assert.AreEqual(123, awaitIntResult("getSpeakersVolume"));
        }

        /*
         * Screen sharing
         * ==========================================================================
         */

        [Test]
        public void testGetScreenSharingSources()
        {
            //_service.getScreenCaptureSources(createScrSourcesResponder(), 160);
            //Assert.IsTrue(awaitScrSourcesResult().Count > 0);
        }

    }
}
