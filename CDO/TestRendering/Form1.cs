using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDO;
namespace TestRendering
{
    public partial class Form1 : Form
    {

        class CDOPlatformReadyListener : PlatformInitListener
        {

            public CDOPlatformReadyListener(Form1 form)
            {
                _form = form;
            }

            private Form1 _form;
            public void onInitProgressChanged(InitProgressChangedEvent e)
            {
                
            }

            public void onInitStateChanged(InitStateChangedEvent e)
            {
                if (e.state == InitStateChangedEvent.InitState.INITIALIZED)
                {
                    _form.onPlatformReady();
                }
            }

        }


        private void onPlatformReady()
        {
            Platform.getService().getVideoCaptureDeviceNames(
                Platform.createResponder<Dictionary<string,string>>(onVideoDevices));
        }

        private void onVideoDevices(Dictionary<string, string> devs)
        {
            Platform.getService().setVideoCaptureDevice(
                Platform.createResponder<object>(onDeviceSet), 
                devs.Keys.First());
        }

        private void onDeviceSet(object nothing)
        {
            Platform.getService().startLocalVideo(
                Platform.createResponder<string>(onVideoStarted));
        }

        private void onVideoStarted(string sinkId)
        {
            RenderOptions rOptions = new RenderOptions();
            rOptions.container = this.renderingPanel;
            rOptions.mirror = true;
            rOptions.filter = VideoScalingFilter.BICUBIC;
            rOptions.sinkId = sinkId;
            Platform.renderSink(rOptions);
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Platform.init(new CDOPlatformReadyListener(this));
        }

        private void renderingPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
