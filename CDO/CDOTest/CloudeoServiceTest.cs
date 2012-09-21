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
using System.Drawing;

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
        public void testSetAppId()
        {
            Responder<object> responder = createVoidResponder();
            _service.setApplicationId(responder, 1);
            awaitVoidResult("setApplicationId");
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

        [Test]
        public void testRenderingWidget()
        {
            // 1. Set proper video capture device
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getVideoCaptureDeviceNames", 15000);
            Assert.IsTrue(devs.Count > 0);
            Console.Error.WriteLine("Setting the device");
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            
            awaitVoidResult("setVideoCaptureDevice", 15000);

            // 2. Do the real test
            Console.Error.WriteLine("Starting local video");
            _service.startLocalVideo(createStringResponder());
            string sinkId = awaitStringResult("startLocalVideo", 15000);
            Assert.That(sinkId.Length > 0);
            Console.Error.WriteLine("Creating renderer");
            RenderOptions ro = new RenderOptions();
            ro.filter = VideoScalingFilter.FAST_BILINEAR;
            ro.mirror = true;
            ro.sinkId = sinkId;
            Console.Error.WriteLine("Starting rendering");
            Form renderingForm = new Form();
            renderingForm.Width = 360;
            renderingForm.Height = 240;

            _service.renderSink(createRendererResponder(), ro);
            RenderingWidget renderer = awaitRendererResult();
            renderer.Width = 320;
            renderer.Height = 240;
            renderer.SetBounds(10, 10, 320, 240);
            renderingForm.Controls.Add(renderer);
            
            renderingForm.ShowDialog();
            _service.stopLocalVideo(createVoidResponder());
            awaitVoidResult();
        }
    

        [Test]
        public void testCustomRendering()
        {
            // 1. Set proper video capture device
            Dictionary<string, string> devs = null; ;
            _service.getVideoCaptureDeviceNames(createDevsResponder());
            devs = awaitDictResult("getVideoCaptureDeviceNames", 15000);
            Assert.IsTrue(devs.Count > 0);
            Console.Error.WriteLine("Setting the device");
            _service.setVideoCaptureDevice(createVoidResponder(), devs.Keys.First());
            
            awaitVoidResult("setVideoCaptureDevice", 15000);

            // 2. Do the real test
            Console.Error.WriteLine("Starting local video");
            _service.startLocalVideo(createStringResponder());
            string sinkId = awaitStringResult("startLocalVideo", 15000);
            Assert.That(sinkId.Length > 0);
            Console.Error.WriteLine("Creating renderer");
            RenderOptions ro = new RenderOptions();
            ro.filter = VideoScalingFilter.FAST_BILINEAR;
            ro.mirror = true;
            ro.sinkId = sinkId;
            Console.Error.WriteLine("Starting rendering");
            _service.manualRenderSink(createManualRendererResponder(), ro);
            ManualRenderer mRenderer = awaitManualRendererResult();
            Console.Error.WriteLine("Rendering started");
            TestRenderingForm renderingForm = new TestRenderingForm(mRenderer);

            renderingForm.ShowDialog();
            _service.stopLocalVideo(createVoidResponder());
            awaitVoidResult();
        }
    }

    class Renderer : Control
    {
        public Renderer()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
    }

    class TestRenderingForm : Form
    {
        private Renderer renderingPanel = new Renderer();

        private ManualRenderer renderer;
        public TestRenderingForm(ManualRenderer renderer)
        {
            this.renderer = renderer;
            Width = 340;
            Height = 260;
            renderingPanel.SetBounds(10, 10, 320, 240);
            Controls.Add(renderingPanel);
            renderer.Invalidated += invalidate;
            renderingPanel.Paint += draw;
            renderingPanel.BackColor = Color.Beige;
        }

        public void draw(object sender, PaintEventArgs e)
        {
            IntPtr dc = e.Graphics.GetHdc();
            DrawRequest drawR = new DrawRequest();
            drawR.hdc = dc;
            drawR.left = 0;
            drawR.right = renderingPanel.Width;
            drawR.top = 0;
            drawR.bottom = renderingPanel.Height;
            renderer.draw(drawR);
            e.Graphics.ReleaseHdc(dc);

        }

        public void invalidate(object sender, EventArgs args)
        {
            renderingPanel.Invalidate();
        }
    }

}