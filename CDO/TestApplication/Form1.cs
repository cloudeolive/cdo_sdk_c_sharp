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
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String path = "cloudeo_sdk_1.16.2.1";
            CloudeoSdkWrapper.CDOString str = new CloudeoSdkWrapper.CDOString();
            str.body = path;
            str.length = (UInt32)path.Length;
            CloudeoSdkWrapper.CDOInitOptions initOptions = new CloudeoSdkWrapper.CDOInitOptions();
            initOptions.logicLibPath = str;
            Debug.Write("Initializing the platform\n");

            IntPtr idPtr = new IntPtr(123);
            CloudeoSdkWrapper.cdo_platform_init_done_clbck callback = new CloudeoSdkWrapper.cdo_platform_init_done_clbck(cdo_platform_init_done_clbck);
            CloudeoSdkWrapper.cdo_init_platform(callback, ref initOptions, idPtr);
            Debug.Write("Frame init complete\n");
        }

        
        private void cdo_platform_init_done_clbck(IntPtr ptr, ref CloudeoSdkWrapper.CDOError err, IntPtr h)
        {
            Debug.Write("Platform initialized\n");
        }
    }
}
