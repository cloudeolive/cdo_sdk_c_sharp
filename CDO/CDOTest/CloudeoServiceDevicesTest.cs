using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CDO;

namespace CDOTest
{
    [TestFixture]
    class CloudeoServiceDevicesTest : AbstractCloudeoServiceTest
    {

        /*
         * Video devices
         * ==========================================================================
         */

        // [Test]
        public void testGetVideoCaptureDeviceNames()
        {
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult("getVideoCaptureDeviceNames", 5000).Count > 0);
        }

        // [Test]
        public void testSetVideoCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getVideoCaptureDeviceNames", 5000);
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setVideoCaptureDevice", 5000);
        }


        // [Test]
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

        // [Test]
        public void testGetAudioCaptureDeviceNames()
        {
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult("getAudioCaptureDeviceNames").Count > 0);
        }

        // [Test]
        public void testSetAudioCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioCaptureDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioCaptureDevice");
        }


        // [Test]
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

        /*
         * Audio output devices
         * ==========================================================================
         */

        // [Test]
        public void testGetAudioOutputDeviceNames()
        {
            _service.getAudioOutputDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult("getAudioOutputDeviceNames").Count > 0);
        }

        // [Test]
        public void testSetAudioOutputDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioOutputDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioOutputDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioOutputDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioOutputDevice");
        }


        // [Test]
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

    }
}
