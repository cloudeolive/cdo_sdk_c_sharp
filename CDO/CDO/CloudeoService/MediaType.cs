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
    public sealed class MediaType
    {
        private string stringValue;

        public string StringValue
        {
            get { return stringValue; }
        }

        /**
         * Non-constructable
         */ 
        private MediaType(string s)
        {
            stringValue = s;
        }

        public static MediaType AUDIO  = new MediaType("audio");
        public static MediaType VIDEO  = new MediaType("video");
        public static MediaType SCREEN = new MediaType("screen");

        internal static MediaType FromString(string s)
        {
            if(String.Equals(s, AUDIO.stringValue, StringComparison.InvariantCultureIgnoreCase))
                return AUDIO;
            else if(String.Equals(s, VIDEO.stringValue, StringComparison.InvariantCultureIgnoreCase))
                return VIDEO;
            else if(String.Equals(s, SCREEN.stringValue, StringComparison.InvariantCultureIgnoreCase))
                return SCREEN;
            else
                return null;
        }
    }
}
