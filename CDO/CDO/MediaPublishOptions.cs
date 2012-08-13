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


        internal static CloudeoSdkWrapper.CDOMediaPublishOptions toNative(MediaPublishOptions options)
        {
            CloudeoSdkWrapper.CDOMediaPublishOptions result = new CloudeoSdkWrapper.CDOMediaPublishOptions();
            result.windowId = StringHelper.toNative(options.windowId);
            result.nativeWidth = options.nativeWidth;
            return result;
        }
    }
}
