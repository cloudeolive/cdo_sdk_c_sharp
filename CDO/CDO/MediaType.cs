using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    public sealed class MediaType
    {

        /**
         * Non-constructable
         */ 
        private MediaType()
        {
        }

        public static string AUDIO = "AUDIO";

        public static string VIDEO = "VIDEO";

        public static string SCREEN = "SCREEN";

    }
}
