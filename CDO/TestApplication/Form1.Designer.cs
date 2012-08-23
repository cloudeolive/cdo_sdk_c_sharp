namespace TestApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LogTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.InitPlatformBTN = new System.Windows.Forms.Button();
            this.PlatformGB = new System.Windows.Forms.GroupBox();
            this.GetVersionBTN = new System.Windows.Forms.Button();
            this.CloudeoServiceGB = new System.Windows.Forms.GroupBox();
            this.VideoDevicesGB = new System.Windows.Forms.GroupBox();
            this.VideoDevicesCB = new System.Windows.Forms.ComboBox();
            this.GetVideoCaptureDeviceBTN = new System.Windows.Forms.Button();
            this.SetVideoCaptureDeviceBTN = new System.Windows.Forms.Button();
            this.GetVideoCaptureDevicesNamesBTN = new System.Windows.Forms.Button();
            this.AudioCaptureDevicesGB = new System.Windows.Forms.GroupBox();
            this.AudioCaptureDevicesCB = new System.Windows.Forms.ComboBox();
            this.GetAudioCaptureDeviceBTN = new System.Windows.Forms.Button();
            this.SetAudioCaptureDeviceBTN = new System.Windows.Forms.Button();
            this.GetAudioCaptureDevicesNamesBTN = new System.Windows.Forms.Button();
            this.AudioOutputDevicesGB = new System.Windows.Forms.GroupBox();
            this.AudioOutputDevicesCB = new System.Windows.Forms.ComboBox();
            this.GetAudioOutputDeviceBTN = new System.Windows.Forms.Button();
            this.SetAudioOutputDeviceBTN = new System.Windows.Forms.Button();
            this.GetAudioOutputDevicesNamesBTN = new System.Windows.Forms.Button();
            this.connUrlTxtBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PlatformGB.SuspendLayout();
            this.CloudeoServiceGB.SuspendLayout();
            this.VideoDevicesGB.SuspendLayout();
            this.AudioCaptureDevicesGB.SuspendLayout();
            this.AudioOutputDevicesGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogTB
            // 
            this.LogTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.LogTB.Location = new System.Drawing.Point(12, 25);
            this.LogTB.Multiline = true;
            this.LogTB.Name = "LogTB";
            this.LogTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTB.Size = new System.Drawing.Size(334, 533);
            this.LogTB.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Log:";
            // 
            // InitPlatformBTN
            // 
            this.InitPlatformBTN.Location = new System.Drawing.Point(6, 19);
            this.InitPlatformBTN.Name = "InitPlatformBTN";
            this.InitPlatformBTN.Size = new System.Drawing.Size(92, 23);
            this.InitPlatformBTN.TabIndex = 3;
            this.InitPlatformBTN.Text = "InitPlatform";
            this.InitPlatformBTN.UseVisualStyleBackColor = true;
            this.InitPlatformBTN.Click += new System.EventHandler(this.InitPlatformBTN_Click);
            // 
            // PlatformGB
            // 
            this.PlatformGB.Controls.Add(this.InitPlatformBTN);
            this.PlatformGB.Location = new System.Drawing.Point(352, 25);
            this.PlatformGB.Name = "PlatformGB";
            this.PlatformGB.Size = new System.Drawing.Size(104, 56);
            this.PlatformGB.TabIndex = 4;
            this.PlatformGB.TabStop = false;
            this.PlatformGB.Text = "Platform";
            // 
            // GetVersionBTN
            // 
            this.GetVersionBTN.Location = new System.Drawing.Point(6, 30);
            this.GetVersionBTN.Name = "GetVersionBTN";
            this.GetVersionBTN.Size = new System.Drawing.Size(92, 23);
            this.GetVersionBTN.TabIndex = 2;
            this.GetVersionBTN.Text = "Get Version";
            this.GetVersionBTN.UseVisualStyleBackColor = true;
            this.GetVersionBTN.Click += new System.EventHandler(this.GetVersionBTN_Click);
            // 
            // CloudeoServiceGB
            // 
            this.CloudeoServiceGB.Controls.Add(this.GetVersionBTN);
            this.CloudeoServiceGB.Location = new System.Drawing.Point(352, 87);
            this.CloudeoServiceGB.Name = "CloudeoServiceGB";
            this.CloudeoServiceGB.Size = new System.Drawing.Size(104, 76);
            this.CloudeoServiceGB.TabIndex = 5;
            this.CloudeoServiceGB.TabStop = false;
            this.CloudeoServiceGB.Text = "CloudeoService";
            // 
            // VideoDevicesGB
            // 
            this.VideoDevicesGB.Controls.Add(this.VideoDevicesCB);
            this.VideoDevicesGB.Controls.Add(this.GetVideoCaptureDeviceBTN);
            this.VideoDevicesGB.Controls.Add(this.SetVideoCaptureDeviceBTN);
            this.VideoDevicesGB.Controls.Add(this.GetVideoCaptureDevicesNamesBTN);
            this.VideoDevicesGB.Location = new System.Drawing.Point(479, 25);
            this.VideoDevicesGB.Name = "VideoDevicesGB";
            this.VideoDevicesGB.Size = new System.Drawing.Size(200, 138);
            this.VideoDevicesGB.TabIndex = 6;
            this.VideoDevicesGB.TabStop = false;
            this.VideoDevicesGB.Text = "Video Devices";
            // 
            // VideoDevicesCB
            // 
            this.VideoDevicesCB.FormattingEnabled = true;
            this.VideoDevicesCB.Location = new System.Drawing.Point(6, 108);
            this.VideoDevicesCB.Name = "VideoDevicesCB";
            this.VideoDevicesCB.Size = new System.Drawing.Size(188, 21);
            this.VideoDevicesCB.TabIndex = 3;
            // 
            // GetVideoCaptureDeviceBTN
            // 
            this.GetVideoCaptureDeviceBTN.Location = new System.Drawing.Point(6, 79);
            this.GetVideoCaptureDeviceBTN.Name = "GetVideoCaptureDeviceBTN";
            this.GetVideoCaptureDeviceBTN.Size = new System.Drawing.Size(188, 23);
            this.GetVideoCaptureDeviceBTN.TabIndex = 2;
            this.GetVideoCaptureDeviceBTN.Text = "Get video capture device";
            this.GetVideoCaptureDeviceBTN.UseVisualStyleBackColor = true;
            this.GetVideoCaptureDeviceBTN.Click += new System.EventHandler(this.GetVideoCaptureDeviceBTN_Click);
            // 
            // SetVideoCaptureDeviceBTN
            // 
            this.SetVideoCaptureDeviceBTN.Location = new System.Drawing.Point(6, 48);
            this.SetVideoCaptureDeviceBTN.Name = "SetVideoCaptureDeviceBTN";
            this.SetVideoCaptureDeviceBTN.Size = new System.Drawing.Size(188, 23);
            this.SetVideoCaptureDeviceBTN.TabIndex = 1;
            this.SetVideoCaptureDeviceBTN.Text = "Set video capture device";
            this.SetVideoCaptureDeviceBTN.UseVisualStyleBackColor = true;
            this.SetVideoCaptureDeviceBTN.Click += new System.EventHandler(this.SetVideoCaptureDeviceBTN_Click);
            // 
            // GetVideoCaptureDevicesNamesBTN
            // 
            this.GetVideoCaptureDevicesNamesBTN.Location = new System.Drawing.Point(6, 19);
            this.GetVideoCaptureDevicesNamesBTN.Name = "GetVideoCaptureDevicesNamesBTN";
            this.GetVideoCaptureDevicesNamesBTN.Size = new System.Drawing.Size(188, 23);
            this.GetVideoCaptureDevicesNamesBTN.TabIndex = 0;
            this.GetVideoCaptureDevicesNamesBTN.Text = "Get video capture devices names";
            this.GetVideoCaptureDevicesNamesBTN.UseVisualStyleBackColor = true;
            this.GetVideoCaptureDevicesNamesBTN.Click += new System.EventHandler(this.GetVideoCaptureDevicesNamesBTN_Click);
            // 
            // AudioCaptureDevicesGB
            // 
            this.AudioCaptureDevicesGB.Controls.Add(this.AudioCaptureDevicesCB);
            this.AudioCaptureDevicesGB.Controls.Add(this.GetAudioCaptureDeviceBTN);
            this.AudioCaptureDevicesGB.Controls.Add(this.SetAudioCaptureDeviceBTN);
            this.AudioCaptureDevicesGB.Controls.Add(this.GetAudioCaptureDevicesNamesBTN);
            this.AudioCaptureDevicesGB.Location = new System.Drawing.Point(479, 169);
            this.AudioCaptureDevicesGB.Name = "AudioCaptureDevicesGB";
            this.AudioCaptureDevicesGB.Size = new System.Drawing.Size(200, 135);
            this.AudioCaptureDevicesGB.TabIndex = 7;
            this.AudioCaptureDevicesGB.TabStop = false;
            this.AudioCaptureDevicesGB.Text = "Audio Capture Devices";
            // 
            // AudioCaptureDevicesCB
            // 
            this.AudioCaptureDevicesCB.FormattingEnabled = true;
            this.AudioCaptureDevicesCB.Location = new System.Drawing.Point(6, 106);
            this.AudioCaptureDevicesCB.Name = "AudioCaptureDevicesCB";
            this.AudioCaptureDevicesCB.Size = new System.Drawing.Size(188, 21);
            this.AudioCaptureDevicesCB.TabIndex = 3;
            // 
            // GetAudioCaptureDeviceBTN
            // 
            this.GetAudioCaptureDeviceBTN.Location = new System.Drawing.Point(6, 77);
            this.GetAudioCaptureDeviceBTN.Name = "GetAudioCaptureDeviceBTN";
            this.GetAudioCaptureDeviceBTN.Size = new System.Drawing.Size(188, 23);
            this.GetAudioCaptureDeviceBTN.TabIndex = 2;
            this.GetAudioCaptureDeviceBTN.Text = "Get audio capture device";
            this.GetAudioCaptureDeviceBTN.UseVisualStyleBackColor = true;
            this.GetAudioCaptureDeviceBTN.Click += new System.EventHandler(this.GetAudioCaptureDeviceBTN_Click);
            // 
            // SetAudioCaptureDeviceBTN
            // 
            this.SetAudioCaptureDeviceBTN.Location = new System.Drawing.Point(6, 48);
            this.SetAudioCaptureDeviceBTN.Name = "SetAudioCaptureDeviceBTN";
            this.SetAudioCaptureDeviceBTN.Size = new System.Drawing.Size(188, 23);
            this.SetAudioCaptureDeviceBTN.TabIndex = 1;
            this.SetAudioCaptureDeviceBTN.Text = "Set audio capture device";
            this.SetAudioCaptureDeviceBTN.UseVisualStyleBackColor = true;
            this.SetAudioCaptureDeviceBTN.Click += new System.EventHandler(this.SetAudioCaptureDeviceBTN_Click);
            // 
            // GetAudioCaptureDevicesNamesBTN
            // 
            this.GetAudioCaptureDevicesNamesBTN.Location = new System.Drawing.Point(6, 19);
            this.GetAudioCaptureDevicesNamesBTN.Name = "GetAudioCaptureDevicesNamesBTN";
            this.GetAudioCaptureDevicesNamesBTN.Size = new System.Drawing.Size(188, 23);
            this.GetAudioCaptureDevicesNamesBTN.TabIndex = 0;
            this.GetAudioCaptureDevicesNamesBTN.Text = "Get audio capture devices names";
            this.GetAudioCaptureDevicesNamesBTN.UseVisualStyleBackColor = true;
            this.GetAudioCaptureDevicesNamesBTN.Click += new System.EventHandler(this.GetAudioCaptureDevicesNamesBTN_Click);
            // 
            // AudioOutputDevicesGB
            // 
            this.AudioOutputDevicesGB.Controls.Add(this.AudioOutputDevicesCB);
            this.AudioOutputDevicesGB.Controls.Add(this.GetAudioOutputDeviceBTN);
            this.AudioOutputDevicesGB.Controls.Add(this.SetAudioOutputDeviceBTN);
            this.AudioOutputDevicesGB.Controls.Add(this.GetAudioOutputDevicesNamesBTN);
            this.AudioOutputDevicesGB.Location = new System.Drawing.Point(479, 310);
            this.AudioOutputDevicesGB.Name = "AudioOutputDevicesGB";
            this.AudioOutputDevicesGB.Size = new System.Drawing.Size(200, 137);
            this.AudioOutputDevicesGB.TabIndex = 8;
            this.AudioOutputDevicesGB.TabStop = false;
            this.AudioOutputDevicesGB.Text = "Audio Output Devices";
            // 
            // AudioOutputDevicesCB
            // 
            this.AudioOutputDevicesCB.FormattingEnabled = true;
            this.AudioOutputDevicesCB.Location = new System.Drawing.Point(6, 106);
            this.AudioOutputDevicesCB.Name = "AudioOutputDevicesCB";
            this.AudioOutputDevicesCB.Size = new System.Drawing.Size(188, 21);
            this.AudioOutputDevicesCB.TabIndex = 3;
            // 
            // GetAudioOutputDeviceBTN
            // 
            this.GetAudioOutputDeviceBTN.Location = new System.Drawing.Point(6, 77);
            this.GetAudioOutputDeviceBTN.Name = "GetAudioOutputDeviceBTN";
            this.GetAudioOutputDeviceBTN.Size = new System.Drawing.Size(188, 23);
            this.GetAudioOutputDeviceBTN.TabIndex = 2;
            this.GetAudioOutputDeviceBTN.Text = "Get audio output device";
            this.GetAudioOutputDeviceBTN.UseVisualStyleBackColor = true;
            this.GetAudioOutputDeviceBTN.Click += new System.EventHandler(this.GetAudioOutputDeviceBTN_Click);
            // 
            // SetAudioOutputDeviceBTN
            // 
            this.SetAudioOutputDeviceBTN.Location = new System.Drawing.Point(6, 48);
            this.SetAudioOutputDeviceBTN.Name = "SetAudioOutputDeviceBTN";
            this.SetAudioOutputDeviceBTN.Size = new System.Drawing.Size(188, 23);
            this.SetAudioOutputDeviceBTN.TabIndex = 1;
            this.SetAudioOutputDeviceBTN.Text = "Set audio output device";
            this.SetAudioOutputDeviceBTN.UseVisualStyleBackColor = true;
            this.SetAudioOutputDeviceBTN.Click += new System.EventHandler(this.SetAudioOutputDeviceBTN_Click);
            // 
            // GetAudioOutputDevicesNamesBTN
            // 
            this.GetAudioOutputDevicesNamesBTN.Location = new System.Drawing.Point(6, 19);
            this.GetAudioOutputDevicesNamesBTN.Name = "GetAudioOutputDevicesNamesBTN";
            this.GetAudioOutputDevicesNamesBTN.Size = new System.Drawing.Size(188, 23);
            this.GetAudioOutputDevicesNamesBTN.TabIndex = 0;
            this.GetAudioOutputDevicesNamesBTN.Text = "Get audio output devices names";
            this.GetAudioOutputDevicesNamesBTN.UseVisualStyleBackColor = true;
            this.GetAudioOutputDevicesNamesBTN.Click += new System.EventHandler(this.GetAudioOutputDevicesNamesBTN_Click);
            // 
            // connUrlTxtBox
            // 
            this.connUrlTxtBox.Location = new System.Drawing.Point(349, 498);
            this.connUrlTxtBox.Name = "connUrlTxtBox";
            this.connUrlTxtBox.Size = new System.Drawing.Size(169, 20);
            this.connUrlTxtBox.TabIndex = 9;
            this.connUrlTxtBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(537, 498);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 577);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.connUrlTxtBox);
            this.Controls.Add(this.AudioOutputDevicesGB);
            this.Controls.Add(this.AudioCaptureDevicesGB);
            this.Controls.Add(this.VideoDevicesGB);
            this.Controls.Add(this.CloudeoServiceGB);
            this.Controls.Add(this.PlatformGB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LogTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloudeo";
            this.PlatformGB.ResumeLayout(false);
            this.CloudeoServiceGB.ResumeLayout(false);
            this.VideoDevicesGB.ResumeLayout(false);
            this.AudioCaptureDevicesGB.ResumeLayout(false);
            this.AudioOutputDevicesGB.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button InitPlatformBTN;
        private System.Windows.Forms.GroupBox PlatformGB;
        private System.Windows.Forms.Button GetVersionBTN;
        private System.Windows.Forms.GroupBox CloudeoServiceGB;
        private System.Windows.Forms.GroupBox VideoDevicesGB;
        private System.Windows.Forms.GroupBox AudioCaptureDevicesGB;
        private System.Windows.Forms.GroupBox AudioOutputDevicesGB;
        private System.Windows.Forms.Button GetVideoCaptureDeviceBTN;
        private System.Windows.Forms.Button SetVideoCaptureDeviceBTN;
        private System.Windows.Forms.Button GetVideoCaptureDevicesNamesBTN;
        private System.Windows.Forms.ComboBox VideoDevicesCB;
        private System.Windows.Forms.ComboBox AudioCaptureDevicesCB;
        private System.Windows.Forms.Button GetAudioCaptureDeviceBTN;
        private System.Windows.Forms.Button SetAudioCaptureDeviceBTN;
        private System.Windows.Forms.Button GetAudioCaptureDevicesNamesBTN;
        private System.Windows.Forms.ComboBox AudioOutputDevicesCB;
        private System.Windows.Forms.Button GetAudioOutputDeviceBTN;
        private System.Windows.Forms.Button SetAudioOutputDeviceBTN;
        private System.Windows.Forms.Button GetAudioOutputDevicesNamesBTN;
        private System.Windows.Forms.TextBox connUrlTxtBox;
        private System.Windows.Forms.Button button1;
    }
}

