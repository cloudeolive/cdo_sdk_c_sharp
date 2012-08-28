/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

namespace sample_app
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
            this.localVideoRenderer = new System.Windows.Forms.Panel();
            this.remoteVideoRenderer = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.disconnectBtn = new System.Windows.Forms.Button();
            this.connectBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.scopeIdInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.spkSelect = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.micSelect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.camSelect = new System.Windows.Forms.ComboBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.logsSink = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // localVideoRenderer
            // 
            this.localVideoRenderer.BackColor = System.Drawing.Color.White;
            this.localVideoRenderer.Location = new System.Drawing.Point(12, 38);
            this.localVideoRenderer.Name = "localVideoRenderer";
            this.localVideoRenderer.Size = new System.Drawing.Size(320, 240);
            this.localVideoRenderer.TabIndex = 0;
            // 
            // remoteVideoRenderer
            // 
            this.remoteVideoRenderer.BackColor = System.Drawing.Color.White;
            this.remoteVideoRenderer.Location = new System.Drawing.Point(338, 38);
            this.remoteVideoRenderer.Name = "remoteVideoRenderer";
            this.remoteVideoRenderer.Size = new System.Drawing.Size(320, 240);
            this.remoteVideoRenderer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cloudeo SDK version:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.disconnectBtn);
            this.groupBox1.Controls.Add(this.connectBtn);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.scopeIdInput);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.spkSelect);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.micSelect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.camSelect);
            this.groupBox1.Location = new System.Drawing.Point(664, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 518);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // disconnectBtn
            // 
            this.disconnectBtn.Location = new System.Drawing.Point(135, 232);
            this.disconnectBtn.Name = "disconnectBtn";
            this.disconnectBtn.Size = new System.Drawing.Size(75, 23);
            this.disconnectBtn.TabIndex = 14;
            this.disconnectBtn.Text = "Disconnect";
            this.disconnectBtn.UseVisualStyleBackColor = true;
            this.disconnectBtn.Click += new System.EventHandler(this.disconnectBtn_Click);
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(54, 232);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 13;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Room Id:";
            // 
            // scopeIdInput
            // 
            this.scopeIdInput.Location = new System.Drawing.Point(9, 206);
            this.scopeIdInput.Name = "scopeIdInput";
            this.scopeIdInput.Size = new System.Drawing.Size(204, 20);
            this.scopeIdInput.TabIndex = 11;
            this.scopeIdInput.Text = "testRoom";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Speakers:";
            // 
            // spkSelect
            // 
            this.spkSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spkSelect.FormattingEnabled = true;
            this.spkSelect.Location = new System.Drawing.Point(9, 144);
            this.spkSelect.Name = "spkSelect";
            this.spkSelect.Size = new System.Drawing.Size(204, 21);
            this.spkSelect.TabIndex = 9;
            this.spkSelect.SelectedIndexChanged += new System.EventHandler(this.spkSelect_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Microphone:";
            // 
            // micSelect
            // 
            this.micSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.micSelect.FormattingEnabled = true;
            this.micSelect.Location = new System.Drawing.Point(9, 94);
            this.micSelect.Name = "micSelect";
            this.micSelect.Size = new System.Drawing.Size(204, 21);
            this.micSelect.TabIndex = 7;
            this.micSelect.SelectedIndexChanged += new System.EventHandler(this.micSelect_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Camera:";
            // 
            // camSelect
            // 
            this.camSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.camSelect.FormattingEnabled = true;
            this.camSelect.Items.AddRange(new object[] {
            "Loading..."});
            this.camSelect.Location = new System.Drawing.Point(9, 43);
            this.camSelect.Name = "camSelect";
            this.camSelect.Size = new System.Drawing.Size(204, 21);
            this.camSelect.TabIndex = 5;
            this.camSelect.SelectedIndexChanged += new System.EventHandler(this.camSelect_SelectedIndexChanged);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(130, 9);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(54, 13);
            this.versionLabel.TabIndex = 5;
            this.versionLabel.Text = "Loading...";
            // 
            // logsSink
            // 
            this.logsSink.Location = new System.Drawing.Point(12, 314);
            this.logsSink.Name = "logsSink";
            this.logsSink.Size = new System.Drawing.Size(643, 242);
            this.logsSink.TabIndex = 6;
            this.logsSink.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Logs:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 563);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.logsSink);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.remoteVideoRenderer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.localVideoRenderer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel localVideoRenderer;
        private System.Windows.Forms.Panel remoteVideoRenderer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox spkSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox micSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox camSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox scopeIdInput;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button disconnectBtn;
        private System.Windows.Forms.RichTextBox logsSink;
        private System.Windows.Forms.Label label5;
    }
}

