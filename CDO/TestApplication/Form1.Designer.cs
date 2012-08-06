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
            this.GetVersionBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LogTB
            // 
            this.LogTB.Location = new System.Drawing.Point(12, 25);
            this.LogTB.Multiline = true;
            this.LogTB.Name = "LogTB";
            this.LogTB.Size = new System.Drawing.Size(260, 225);
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
            // GetVersionBTN
            // 
            this.GetVersionBTN.Location = new System.Drawing.Point(278, 25);
            this.GetVersionBTN.Name = "GetVersionBTN";
            this.GetVersionBTN.Size = new System.Drawing.Size(75, 23);
            this.GetVersionBTN.TabIndex = 2;
            this.GetVersionBTN.Text = "Get Version";
            this.GetVersionBTN.UseVisualStyleBackColor = true;
            this.GetVersionBTN.Click += new System.EventHandler(this.GetVersionBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 262);
            this.Controls.Add(this.GetVersionBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LogTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button GetVersionBTN;
    }
}

