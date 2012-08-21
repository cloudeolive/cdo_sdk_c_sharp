using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    public class PlatformInitOptions
    {

        public string sdkPath
        {
            set { this._sdkPath = value; }
            get { return this._sdkPath; }
        }

        private string _sdkPath;
    }
}
