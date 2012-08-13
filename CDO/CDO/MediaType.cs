using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    public sealed class MediaType
    {
        private string stringValue;

        internal string StringValue
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

        public static MediaType AUDIO = new MediaType("AUDIO");
        public static MediaType VIDEO = new MediaType("VIDEO");
        public static MediaType SCREEN = new MediaType("SCREEN");
    }
}
