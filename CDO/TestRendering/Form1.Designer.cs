/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

namespace TestRendering
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
            this.renderingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.startRenderBtn = new System.Windows.Forms.Button();
            this.stopRenderBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // renderingPanel
            // 
            this.renderingPanel.BackColor = System.Drawing.Color.White;
            this.renderingPanel.Location = new System.Drawing.Point(16, 38);
            this.renderingPanel.Name = "renderingPanel";
            this.renderingPanel.Size = new System.Drawing.Size(320, 240);
            this.renderingPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SDK Version:";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(89, 13);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(54, 13);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.Text = "Loading...";
            // 
            // startRenderBtn
            // 
            this.startRenderBtn.Enabled = false;
            this.startRenderBtn.Location = new System.Drawing.Point(16, 284);
            this.startRenderBtn.Name = "startRenderBtn";
            this.startRenderBtn.Size = new System.Drawing.Size(75, 23);
            this.startRenderBtn.TabIndex = 3;
            this.startRenderBtn.Text = "Start Render";
            this.startRenderBtn.UseVisualStyleBackColor = true;
            this.startRenderBtn.Click += new System.EventHandler(this.startRenderBtn_Click);
            // 
            // stopRenderBtn
            // 
            this.stopRenderBtn.Enabled = false;
            this.stopRenderBtn.Location = new System.Drawing.Point(97, 284);
            this.stopRenderBtn.Name = "stopRenderBtn";
            this.stopRenderBtn.Size = new System.Drawing.Size(75, 23);
            this.stopRenderBtn.TabIndex = 4;
            this.stopRenderBtn.Text = "Stop Render";
            this.stopRenderBtn.UseVisualStyleBackColor = true;
            this.stopRenderBtn.Click += new System.EventHandler(this.stopRenderBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 314);
            this.Controls.Add(this.stopRenderBtn);
            this.Controls.Add(this.startRenderBtn);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.renderingPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel renderingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button startRenderBtn;
        private System.Windows.Forms.Button stopRenderBtn;
    }
}

