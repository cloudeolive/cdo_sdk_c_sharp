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

        [Test]
        public void testGetVideoCaptureDeviceNames()
        {
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult().Count > 0);
        }

        [Test]
        public void testSetVideoCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult();
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult();
        }


        [Test]
        public void testGetVideoCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult();
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult();
            _service.getVideoCaptureDevice(createStringResponder());
            Assert.AreEqual(devs.Keys.First(), awaitStringResult());
        }


        /*
         * Audio capture devices
         * ==========================================================================
         */

        [Test]
        public void testGetAudioCaptureDeviceNames()
        {
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult().Count > 0);
        }

        [Test]
        public void testSetAudioCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult();
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult();
        }


        [Test]
        public void testGetAudioCaptureDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult();
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult();
            _service.getAudioCaptureDevice(createStringResponder());
            Assert.AreEqual(devs.Keys.First(), awaitStringResult());
        }

        /*
         * Audio output devices
         * ==========================================================================
         */

        [Test]
        public void testGetAudioOutputDeviceNames()
        {
            _service.getAudioOutputDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult().Count > 0);
        }

        [Test]
        public void testSetAudioOutputDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioOutputDeviceNames(createDevsResponder());
            devs = awaitDictResult();
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioOutputDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult();
        }


        [Test]
        public void testGetAudioOutputDevice()
        {
            Dictionary<string, string> devs = null; ;
            _service.getAudioOutputDeviceNames(createDevsResponder());
            devs = awaitDictResult();
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioOutputDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult();
            _service.getAudioOutputDevice(createStringResponder());
            Assert.AreEqual(devs.Keys.First(), awaitStringResult());
        }

    }
}
