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
    class CloudeoServiceConnectivityTest:AbstractCloudeoServiceTest
    {
        [Test]
        public void testConnectDisconnect()
        {
            string scopeId = "c_sharp_test_room";
            setupDevs();
            ConnectionDescription connDescr = genDefConnDescr(scopeId);
            _service.connect(createVoidResponder(), connDescr);
            awaitVoidResult("connect", 10000);
            Thread.Sleep(5000);
            _service.disconnect(createVoidResponder(), scopeId);
            awaitVoidResult("disconnect");

        }

        [Test]
        public void testPublish()
        {
            setupDevs();
            string scopeId = "c_sharp_test_room";
            ConnectionDescription connDescr = genDefConnDescr(scopeId);
            connDescr.autopublishAudio = false;
            connDescr.autopublishVideo = false;

            _service.connect(createVoidResponder(), connDescr);
            awaitVoidResult("connect", 10000);

            Thread.Sleep(1000);

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
        public void testPublishScreen()
        {
            setupDevs();
            string scopeId = "c_sharp_test_room";
            ConnectionDescription connDescr = genDefConnDescr(scopeId);
            connDescr.autopublishAudio = false;
            connDescr.autopublishVideo = false;

            _service.connect(createVoidResponder(), connDescr);
            awaitVoidResult("connect", 10000);

            _service.getScreenCaptureSources(createScrSourcesResponder(), 160);
            List<ScreenCaptureSource> sources = awaitScrSourcesResult();

            MediaPublishOptions options = new MediaPublishOptions();
            options.windowId = sources[0].id;
            options.nativeWidth = 640;
            _service.publish(createVoidResponder(), scopeId, MediaType.SCREEN, options);
            awaitVoidResult("publish screen", 10000);

            Thread.Sleep(15000);
            _service.disconnect(createVoidResponder(), scopeId);
            awaitVoidResult("disconnect");

        }

        [Test]
        public void testUnpublish()
        {
            setupDevs();
            string scopeId = "c_sharp_test_room";
            ConnectionDescription connDescr = genDefConnDescr(scopeId);

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

        [Test]
        public void testSendMessage()
        {
            setupDevs(); 
            string scopeId = "c_sharp_test_room";
            ConnectionDescription connDescr = genDefConnDescr(scopeId);
            _service.connect(createVoidResponder(), connDescr);
            awaitVoidResult("connect", 10000);

            Thread.Sleep(1000);

            _service.sendMessage(createVoidResponder(), scopeId, "Some random message", -1);
            awaitVoidResult("publish audio");

            Thread.Sleep(1000);

            Thread.Sleep(5000);
            _service.disconnect(createVoidResponder(), scopeId);
            awaitVoidResult("disconnect");

        }

        [Test]
        public void testStats()
        {
            setupDevs();
            string scopeId = "c_sharp_test_room";
            ConnectionDescription connDescr = genDefConnDescr(scopeId);
            connDescr.autopublishAudio = true;
            connDescr.autopublishVideo = true;
            
            _service.connect(createVoidResponder(), connDescr);
            awaitVoidResult("connect", 10000);

            Thread.Sleep(1000);
            MediaStatsEvent lastE = null;
            dispatcher.MediaStats += delegate(object nothing, MediaStatsEvent e)
            {
                lastE = e;
                Console.Error.WriteLine("Got media event " + e.MediaType.StringValue);
                Console.Error.WriteLine("Bitrate: " + e.Stats.bitRate);
                Console.Error.WriteLine("RTT: " + e.Stats.rtt);
            };
            
            _service.startMeasuringStatistics(createVoidResponder(), scopeId, 1);
            awaitVoidResult("startMeasuringStatistics");
            Thread.Sleep(5000);
            Assert.IsNotNull(lastE);
            _service.stopMeasuringStatistics(createVoidResponder(), scopeId);
            awaitVoidResult("stopMeasuringStatistics");
            Thread.Sleep(1000);
            lastE = null;
            Thread.Sleep(5000);
            Assert.IsNull(lastE);
            _service.disconnect(createVoidResponder(), scopeId);
            awaitVoidResult("disconnect");

        }

        private ConnectionDescription genDefConnDescr(string scopeId)
        {
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

            return connDescr;
        }

        private void setupDevs()
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
            
        }
    }
}
