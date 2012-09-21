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
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
            dispatcher.MicActivity += delegate(object sender, MicActivityEvent e)
            {
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
            _service.getScreenCaptureSources(createScrSourcesResponder(), 160);
            List<ScreenCaptureSource> sources = awaitScrSourcesResult(10000);
            Assert.IsTrue(sources.Count > 0);
            Console.Error.WriteLine("Got sources: " + sources.Count);
            CountdownLatch latch = new CountdownLatch(1);
            PictureForm pForm = new PictureForm(sources);
            pForm.ShowDialog();
        }

    }

    class PictureWithLabel : Panel
    {
        private Label label = new Label();
        private PictureBox pictureBox = new PictureBox();

        public PictureWithLabel(ScreenCaptureSource src)
        {
            this.Width = 160;
            this.Height = 140;
            label.Text = src.title;
            pictureBox.Image = src.snapshot;
            pictureBox.SetBounds(0, 0, 160, 120);
            label.SetBounds(0, 120, 160, 20);
            Controls.Add(label);
            Controls.Add(pictureBox);
        }
    }

    class PictureForm : Form
    {
        private FlowLayoutPanel flowLayout = new FlowLayoutPanel();
        
        List<ScreenCaptureSource> sources;
        public PictureForm(List<ScreenCaptureSource> sources)
        {
            this.sources = sources;
            this.Height = 480;
            this.Width = 640;
            flowLayout.SetBounds(0, 0, 640, 480);
            Controls.Add(flowLayout);
            foreach (ScreenCaptureSource src in sources)
            {
                PictureWithLabel pwl = new PictureWithLabel(src);
                flowLayout.Controls.Add(pwl);
            }
        }
    }

}
