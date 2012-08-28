/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

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
