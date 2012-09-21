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
using System.Drawing;
using System.IO;

namespace CDO
{
    public class ScreenCaptureSource
    {
        private string _id;
        private string _title;
        private Image _snapshot;

        public string id { get { return this._id; } }
        public string title { get { return this._title; } }
        public Image snapshot { get { return this._snapshot; } }

        internal ScreenCaptureSource(string id, string title, byte[] imageData)
        {
            this._id = id;
            this._title = title;
            this._snapshot = Image.FromStream(new MemoryStream(imageData));
        }
    }
}
