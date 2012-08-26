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

        private string _localPreviewSinkId;

        private int _rendererId;
        private int _rendererId2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Platform.init(new CDOPlatformReadyListener(this));
        }

        private void startRenderBtn_Click(object sender, EventArgs e)
        {
            RenderOptions rOptions = new RenderOptions();
            rOptions.container = this.renderingPanel;
            rOptions.mirror = false;
            rOptions.filter = VideoScalingFilter.FAST_BILINEAR;
            rOptions.sinkId = _localPreviewSinkId;
            Platform.renderSink(Platform.R<int>(onRenderStarted), rOptions);

            RenderOptions rOptions2 = new RenderOptions();
            rOptions2.container = this.renderingPanel2;
            rOptions2.mirror = true;
            rOptions2.filter = VideoScalingFilter.FAST_BILINEAR;
            rOptions2.sinkId = _localPreviewSinkId;
            Platform.renderSink(Platform.R<int>(onRenderStarted2), rOptions2);            

        }

        private void stopRenderBtn_Click(object sender, EventArgs e)
        {
            Platform.stopRender(Platform.R<object>(onRenderStopped), _rendererId);
            Platform.stopRender(Platform.R<object>(onRenderStopped), _rendererId2);
        }

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
            Platform.getService().getVersion(Platform.R<string>(onVersion));
            Platform.getService().getVideoCaptureDeviceNames(
                Platform.R<Dictionary<string,string>>(onVideoDevices));
        }

        delegate void SetVersionCallback(string text);
        
        private void onVersion(string version)
        {
            if (versionLabel.InvokeRequired)
            {                
                Invoke(new SetVersionCallback(onVersion), new object[] { version });
            }
            else
            {
                versionLabel.Text = version;
            }
        }

        private void onVideoDevices(Dictionary<string, string> devs)
        {
            Platform.getService().setVideoCaptureDevice(
                Platform.R<object>(onDeviceSet), 
                devs.Keys.First());
        }

        private void onDeviceSet(object nothing)
        {
            Platform.getService().startLocalVideo(
                Platform.R<string>(onVideoStarted));
        }
              
        private void onVideoStarted(string sinkId)
        {
            _localPreviewSinkId = sinkId;
            startRenderBtn_Click(null, null);
        }

        private void onRenderStarted(int rendererId)
        {
            _rendererId = rendererId;
            Invoke(new EnableDisableDelegate(enableButtonCallback), new object[] { stopRenderBtn });
            Invoke(new EnableDisableDelegate(disableButtonCallback), new object[] { startRenderBtn });
        }

        private void onRenderStarted2(int rendererId)
        {
            _rendererId2 = rendererId;
        }

        private void onRenderStopped(object unused)
        {
            Invoke(new EnableDisableDelegate(disableButtonCallback), new object[] { stopRenderBtn });
            Invoke(new EnableDisableDelegate(enableButtonCallback), new object[] { startRenderBtn });
        }

        delegate void EnableDisableDelegate(Button button);
        private void enableButtonCallback(Button btn)
        {
            btn.Enabled = true;
        }

        private void disableButtonCallback(Button btn)
        {
            btn.Enabled = false;
        }

        
    }
}
