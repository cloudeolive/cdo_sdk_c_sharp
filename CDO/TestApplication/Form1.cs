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
    public partial class Form1 : Form
    {

        IntPtr CDOHandle;

        public Form1()
        {
            InitializeComponent();
        }

        // Patform init

        private void Form1_Load(object sender, EventArgs e)
        {
            //String path = "C:\\Users\\Pawel\\AppData\\LocalLow\\Cloudeo\\1.16.2.1";
            //CloudeoSdkWrapper.CDOString str = new CloudeoSdkWrapper.CDOString();
            //str.body = path;
            //str.length = (UInt32)path.Length;
            
            //CloudeoSdkWrapper.CDOInitOptions initOptions = new CloudeoSdkWrapper.CDOInitOptions();
            //initOptions.logicLibPath = str;
            //Debug.Write("Initializing the platform\n");

            //IntPtr idPtr = new IntPtr(123);
            
            //CloudeoSdkWrapper.cdo_platform_init_done_clbck callback = new CloudeoSdkWrapper.cdo_platform_init_done_clbck(cdo_platform_init_done_clbck);
            
            //CloudeoSdkWrapper.cdo_init_platform(callback, ref initOptions, idPtr);

            //LogTB.AppendText("Frame init complete\n");
            //Debug.Write("Frame init complete\n");
        }

        
        //private void cdo_platform_init_done_clbck(IntPtr ptr, ref CloudeoSdkWrapper.CDOError err, IntPtr h)
        //{
        //    CDOHandle = h;

        //    LogTB.AppendText("Platform initialized\n");
        //    Debug.Write("Platform initialized\n");
        //}

        // Get Version

        private void GetVersionBTN_Click(object sender, EventArgs e)
        {
            //CloudeoSdkWrapper.cdo_string_rclbck_t callback = new CloudeoSdkWrapper.cdo_string_rclbck_t(cdo_get_version_clbck);

            //IntPtr idPtr = new IntPtr(456);

            //CloudeoSdkWrapper.cdo_get_version(callback, CDOHandle, idPtr);

            //LogTB.AppendText("Getting version...\n");
            //Debug.Write("Getting version...\n");
        }


        //private void cdo_get_version_clbck(IntPtr opaque, ref CloudeoSdkWrapper.CDOError error, ref CloudeoSdkWrapper.CDOString str)
        //{
        //    LogTB.AppendText("Get version complete\n");
        //    Debug.Write("Get version complete\n");
        //}
    }
}
