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

        public static MediaType AUDIO  = new MediaType("AUDIO");
        public static MediaType VIDEO  = new MediaType("VIDEO");
        public static MediaType SCREEN = new MediaType("SCREEN");

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
