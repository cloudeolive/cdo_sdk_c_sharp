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
    class CloudeoServiceConnectivityTest:AbstractCloudeoServiceTest
    {
        [Test]
        public void testConnectDisconnect()
        {
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getVideoCaptureDeviceNames", 5000);
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setVideoCaptureDevice", 5000);
            devs = null; ;
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioCaptureDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioCaptureDevice");
            devs = null; ;
            _service.getAudioOutputDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioOutputDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioOutputDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioOutputDevice");
            string scopeId = "c_sharp_test_room";
            
            ConnectionDescription connDescr = new ConnectionDescription();
            connDescr.autopublishAudio = true;
            connDescr.autopublishVideo = true;
            connDescr.url = "dev04.saymama.com:7005/c_sharp_test_room";
            connDescr.token = new Random().Next(1000) + "";
            connDescr.lowVideoStream.maxBitRate = 32;
            connDescr.lowVideoStream.maxWidth = 160;
            connDescr.lowVideoStream.maxHeight = 120;
            connDescr.lowVideoStream.maxFps = 5;
            connDescr.lowVideoStream.publish = true;
            connDescr.lowVideoStream.receive = true;

            connDescr.highVideoStream.maxBitRate = 500;
            connDescr.highVideoStream.maxWidth = 320;
            connDescr.highVideoStream.maxHeight = 240;
            connDescr.highVideoStream.maxFps = 15;
            connDescr.highVideoStream.publish = true;
            connDescr.highVideoStream.receive = true;

            _service.connect(createVoidResponder(), connDescr);
            awaitVoidResult("connect", 10000);
            Thread.Sleep(5000);
            _service.disconnect(createVoidResponder(), scopeId);
            awaitVoidResult("disconnect");

        }

        [Test]
        public void testPublish()
        {
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getVideoCaptureDeviceNames", 5000);
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setVideoCaptureDevice", 5000);
            devs = null; ;
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioCaptureDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioCaptureDevice");
            devs = null; ;
            _service.getAudioOutputDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioOutputDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioOutputDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioOutputDevice");

            string scopeId = "c_sharp_test_room";
            ConnectionDescription connDescr = new ConnectionDescription();
            connDescr.autopublishAudio = false;
            connDescr.autopublishVideo = false;
            connDescr.url = "dev04.saymama.com:7005/" + scopeId;
            connDescr.token = new Random().Next(1000) + "";
            connDescr.lowVideoStream.maxBitRate = 32;
            connDescr.lowVideoStream.maxWidth = 160;
            connDescr.lowVideoStream.maxHeight = 120;
            connDescr.lowVideoStream.maxFps = 5;
            connDescr.lowVideoStream.publish = true;
            connDescr.lowVideoStream.receive = true;

            connDescr.highVideoStream.maxBitRate = 500;
            connDescr.highVideoStream.maxWidth = 320;
            connDescr.highVideoStream.maxHeight = 240;
            connDescr.highVideoStream.maxFps = 15;
            connDescr.highVideoStream.publish = true;
            connDescr.highVideoStream.receive = true;

            _service.connect(createVoidResponder(), connDescr);
            awaitVoidResult("connect", 10000);

            _service.publish(createVoidResponder(), scopeId, MediaType.AUDIO, null);
            awaitVoidResult("publish audio");

            Thread.Sleep(1000);

            _service.publish(createVoidResponder(), scopeId, MediaType.VIDEO, null);
            awaitVoidResult("publish video", 10000);

            Thread.Sleep(5000);
            _service.disconnect(createVoidResponder(), scopeId);
            awaitVoidResult("disconnect");

        }

        [Test]
        public void testUnpublish()
        {
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getVideoCaptureDeviceNames", 5000);
            Assert.IsTrue(devs.Count > 0);
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setVideoCaptureDevice", 5000);
            devs = null; ;
            _service.getAudioCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioCaptureDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioCaptureDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioCaptureDevice");
            devs = null; ;
            _service.getAudioOutputDeviceNames(createDevsResponder());
            devs = awaitDictResult("getAudioOutputDeviceNames");
            Assert.IsTrue(devs.Count > 0);
            _service.setAudioOutputDevice(createVoidResponder(), devs.Keys.First());
            awaitVoidResult("setAudioOutputDevice");
            string scopeId = "c_sharp_test_room";

            ConnectionDescription connDescr = new ConnectionDescription();
            connDescr.autopublishAudio = true;
            connDescr.autopublishVideo = true;
            connDescr.url = "dev04.saymama.com:7005/c_sharp_test_room";
            connDescr.token = new Random().Next(1000) + "";
            connDescr.lowVideoStream.maxBitRate = 32;
            connDescr.lowVideoStream.maxWidth = 160;
            connDescr.lowVideoStream.maxHeight = 120;
            connDescr.lowVideoStream.maxFps = 5;
            connDescr.lowVideoStream.publish = true;
            connDescr.lowVideoStream.receive = true;

            connDescr.highVideoStream.maxBitRate = 500;
            connDescr.highVideoStream.maxWidth = 320;
            connDescr.highVideoStream.maxHeight = 240;
            connDescr.highVideoStream.maxFps = 15;
            connDescr.highVideoStream.publish = true;
            connDescr.highVideoStream.receive = true;

            _service.connect(createVoidResponder(), connDescr);
            awaitVoidResult("connect", 10000);
            Thread.Sleep(2000);

            _service.unpublish(createVoidResponder(), scopeId, MediaType.AUDIO);
            awaitVoidResult("unpublish audio");

            Thread.Sleep(1000);

            _service.unpublish(createVoidResponder(), scopeId, MediaType.VIDEO);
            awaitVoidResult("unpublish video");

            Thread.Sleep(1000);
            
            _service.disconnect(createVoidResponder(), scopeId);
            awaitVoidResult("disconnect");

        }

    }
}
