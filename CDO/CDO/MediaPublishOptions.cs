using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    public class MediaPublishOptions
    {

        public string windowId;

        public int nativeWidth;


        internal static CDOMediaPublishOptions toNative(MediaPublishOptions options)
        {
            CDOMediaPublishOptions result = new CDOMediaPublishOptions();
            result.windowId = StringHelper.toNative(options.windowId);
            result.nativeWidth = options.nativeWidth;
            return result;
        }
    }
}
