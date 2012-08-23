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

        [Test]
        public void testGetVideoCaptureDeviceNames()
        {
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            Assert.IsTrue(awaitDictResult().Count > 0);
        }

        [Test]
        public void testSetVideoCaptureDevice()
        {
            setUp();
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult();
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult();
            tearDown();
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

    }
}
