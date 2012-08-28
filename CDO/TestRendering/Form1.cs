/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

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
            rOptions.mirror = false;
            rOptions.filter = VideoScalingFilter.FAST_BILINEAR;
            rOptions.sinkId = _localPreviewSinkId;
            Platform.renderSink(Platform.R<RenderingWidget>(onRenderStarted), rOptions);        
        }

        private void stopRenderBtn_Click(object sender, EventArgs e)
        {
            RenderingWidget rWidget = (RenderingWidget) renderingPanel.Controls[0];
            rWidget.stop();
            renderingPanel.Controls.Clear();
            Invoke(new EnableDisableDelegate(disableButtonCallback), new object[] { stopRenderBtn });
            Invoke(new EnableDisableDelegate(enableButtonCallback), new object[] { startRenderBtn });

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

        private delegate void AddWidgetDelegate(Panel panel, Control component);

        private void onRenderStarted(RenderingWidget renderer)
        {
            renderer.Width = renderingPanel.Width;
            renderer.Height = renderingPanel.Height;
            
            Invoke(new AddWidgetDelegate(addWidget), new object[] { renderingPanel, renderer });
            Invoke(new EnableDisableDelegate(enableButtonCallback), new object[] { stopRenderBtn });
            Invoke(new EnableDisableDelegate(disableButtonCallback), new object[] { startRenderBtn });
        }        

        private void addWidget(Panel panel, Control component)
        {
            panel.Controls.Add(component);
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
