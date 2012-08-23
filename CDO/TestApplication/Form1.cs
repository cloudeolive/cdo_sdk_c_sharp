using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TestApplication
{
    public partial class Form1 : Form, PlatformInitListener
    {
        private CloudeoService _cloudeoService;

        public Form1()
        {
            InitializeComponent();
        }

        #region Cross Thread Operation

        public void UpdateLogTB(string text)
        {
            if (LogTB.InvokeRequired)
                LogTB.Invoke(new Action<string>(UpdateLogTB), text);
            else
                LogTB.AppendText(text);
        }

        public void UpdateCB(ComboBox cb, Dictionary<string, string> dic)
        {
            if (cb.InvokeRequired)
            {
                cb.Invoke(new Action<ComboBox, Dictionary<string, string>>(UpdateCB),cb, dic);
            }
            else
            {
                cb.DataSource = new BindingSource(dic, null);
                cb.DisplayMember = "Value";
                cb.ValueMember = "Key";
            }
        }

        #endregion


        #region PlatformInit

        private void InitPlatformBTN_Click(object sender, EventArgs e)
        {
            Platform.init(this);
        }
        public void onInitProgressChanged(InitProgressChangedEvent e)
        {
            LogTB.AppendText(String.Format("Init platform progress: {0}{1}", e.progress, Environment.NewLine));
        }
        public void onInitStateChanged(InitStateChangedEvent e)
        {
            LogTB.AppendText(String.Format("Init platform: errCode={0}; errMessage={1}; state={2};{3}", e.errCode, e.errMessage, e.state, Environment.NewLine));
            _cloudeoService = Platform.getService();
        }

        #endregion


        #region GetVersion

        private void GetVersionBTN_Click(object sender, EventArgs e)
        {
            _cloudeoService.getVersion(Platform.createResponder<string>(getServiceResultHandler, getServiceErrHandler));
        }
        private void getServiceResultHandler(string s)
        {
            UpdateLogTB(String.Format("getVersion: s={0};{1}", s, Environment.NewLine));
        }
        private void getServiceErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("getVersion: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        #endregion


        #region Video Capture Devices

        private void GetVideoCaptureDevicesNamesBTN_Click(object sender, EventArgs e)
        {
            _cloudeoService.getVideoCaptureDeviceNames(Platform.createResponder<Dictionary<string,string>>(GetVideoCaptureDevicesNamesResultHandler, GetVideoCaptureDevicesNamesErrHandler));
        }
        private void GetVideoCaptureDevicesNamesResultHandler(Dictionary<string, string> dic)
        {
            UpdateCB(VideoDevicesCB, dic);
        }
        private void GetVideoCaptureDevicesNamesErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("GetVideoCaptureDevicesNames: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        private void SetVideoCaptureDeviceBTN_Click(object sender, EventArgs e)
        {
            if (VideoDevicesCB.SelectedItem != null)
            {
                _cloudeoService.setVideoCaptureDevice(Platform.createResponder<object>(SetVideoCaptureDeviceResultHandler, SetVideoCaptureDeviceErrHandler), (string)VideoDevicesCB.SelectedValue);
            }
            else
            {
                UpdateLogTB(String.Format("SetVideoCaptureDevice: Select video device.{0}", Environment.NewLine));
            }
        }
        private void SetVideoCaptureDeviceResultHandler(object obj)
        {
            UpdateLogTB(String.Format("SetVideoCaptureDevice: Device was set.{0}", Environment.NewLine));
        }
        private void SetVideoCaptureDeviceErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("SetVideoCaptureDevice: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        private void GetVideoCaptureDeviceBTN_Click(object sender, EventArgs e)
        {
            _cloudeoService.getVideoCaptureDevice(Platform.createResponder<string>(GetVideoCaptureDeviceResultHandler, GetVideoCaptureDeviceErrHandler));
        }
        private void GetVideoCaptureDeviceResultHandler(string s)
        {
            UpdateLogTB(String.Format("GetVideoCaptureDevice: device={0};{1}", s, Environment.NewLine));
        }
        private void GetVideoCaptureDeviceErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("GetVideoCaptureDevice: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        #endregion


        #region Audio Capture Devices

        private void GetAudioCaptureDevicesNamesBTN_Click(object sender, EventArgs e)
        {
            _cloudeoService.getAudioCaptureDeviceNames(Platform.createResponder<Dictionary<string, string>>(GetAudioCaptureDevicesNamesResultHandler, GetAudioCaptureDevicesNamesErrHandler));
        }
        private void GetAudioCaptureDevicesNamesResultHandler(Dictionary<string, string> dic)
        {
            UpdateCB(AudioCaptureDevicesCB, dic);
        }
        private void GetAudioCaptureDevicesNamesErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("GetAudioCaptureDevicesNames: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        private void SetAudioCaptureDeviceBTN_Click(object sender, EventArgs e)
        {
            if (AudioCaptureDevicesCB.SelectedItem != null)
            {
                _cloudeoService.setAudioCaptureDevice(Platform.createResponder<object>(SetAudioCaptureDeviceResultHandler, SetAudioCaptureDeviceErrHandler), (string)AudioCaptureDevicesCB.SelectedValue);
            }
            else
            {
                UpdateLogTB(String.Format("SetAudioCaptureDevice: Select audio capture device.{0}", Environment.NewLine));
            }
        }
        private void SetAudioCaptureDeviceResultHandler(object obj)
        {
            UpdateLogTB(String.Format("SetAudioCaptureDevice: Device was set.{0}", Environment.NewLine));
        }
        private void SetAudioCaptureDeviceErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("SetAudioCaptureDevice: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        private void GetAudioCaptureDeviceBTN_Click(object sender, EventArgs e)
        {
            _cloudeoService.getAudioCaptureDevice(Platform.createResponder<string>(GetAudioCaptureDeviceResultHandler, GetAudioCaptureDeviceErrHandler));
        }
        private void GetAudioCaptureDeviceResultHandler(string s)
        {
            UpdateLogTB(String.Format("GetAudioCaptureDevice: device={0};{1}", s, Environment.NewLine));
        }
        private void GetAudioCaptureDeviceErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("GetAudioCaptureDevice: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        #endregion


        #region Audio Output Devices

        private void GetAudioOutputDevicesNamesBTN_Click(object sender, EventArgs e)
        {
            _cloudeoService.getAudioOutputDeviceNames(Platform.createResponder<Dictionary<string, string>>(GetAudioOutputDevicesNamesResultHandler, GetAudioOutputDevicesNamesErrHandler));
        }
        private void GetAudioOutputDevicesNamesResultHandler(Dictionary<string, string> dic)
        {
            UpdateCB(AudioOutputDevicesCB, dic);
        }
        private void GetAudioOutputDevicesNamesErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("GetAudioOutputDevicesNames: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        private void SetAudioOutputDeviceBTN_Click(object sender, EventArgs e)
        {
            if (AudioOutputDevicesCB.SelectedItem != null)
            {
                _cloudeoService.setAudioOutputDevice(Platform.createResponder<object>(SetAudioOutputDeviceResultHandler, SetAudioOutputDeviceErrHandler), (string)AudioOutputDevicesCB.SelectedValue);
            }
            else
            {
                UpdateLogTB(String.Format("SetAudioOutputDevice: Select audio output device.{0}", Environment.NewLine));
            }
        }
        private void SetAudioOutputDeviceResultHandler(object obj)
        {
            UpdateLogTB(String.Format("SetAudioOutputDevice: Device was set.{0}", Environment.NewLine));
        }
        private void SetAudioOutputDeviceErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("SetAudioOutputDevice: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        private void GetAudioOutputDeviceBTN_Click(object sender, EventArgs e)
        {
            _cloudeoService.getAudioOutputDevice(Platform.createResponder<string>(GetAudioOutputDeviceResultHandler, GetAudioOutputDeviceErrHandler));
        }
        private void GetAudioOutputDeviceResultHandler(string s)
        {
            UpdateLogTB(String.Format("GetAudioOutputDevice: device={0};{1}", s, Environment.NewLine));
        }
        private void GetAudioOutputDeviceErrHandler(int errCode, string errMessage)
        {
            UpdateLogTB(String.Format("GetAudioOutputDevice: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine));
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            string url = connUrlTxtBox.Text;
            ConnectionDescription connDescr = new ConnectionDescription();
            connDescr.autopublishAudio = true;
            connDescr.autopublishVideo = true;
            connDescr.url = url;
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
            UpdateLogTB("Trying to establish the connection to the streamer with url: "+url+"\n");
            _cloudeoService.connect(Platform.createResponder<object>(
                delegate(object result) {
                    UpdateLogTB("Successfully connected\n");
                },
                delegate(int errCode, string errMessage) {
                    String.Format("Failed to connect: errCode={0}; errMessage={1};{2}", errCode, errMessage, Environment.NewLine);
                }
                ), connDescr); 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
