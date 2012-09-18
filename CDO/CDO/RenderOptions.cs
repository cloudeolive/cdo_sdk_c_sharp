using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    public class VideoScalingFilter
    {
        private string stringValue;

        internal string StringValue { get { return stringValue; } }

        private VideoScalingFilter() {}
        private VideoScalingFilter(string s) { stringValue = s;  }

        public static VideoScalingFilter FAST_BILINEAR = new VideoScalingFilter("FAST_BILINEAR");
        public static VideoScalingFilter BICUBIC = new VideoScalingFilter("BICUBIC");
    }

    public delegate void InvalidateClbck();


    public class RenderOptions
    {
        private string _sinkId;
        private bool _mirror;
        private VideoScalingFilter _filter;
        private invalidate_clbck_t _invalidateClbck;
        private InvalidateClbck _customInvalidateClbck;

        public string sinkId { get { return _sinkId; } set { _sinkId = value; } }
        public bool mirror { get { return _mirror; } set { _mirror = value; } }
        public VideoScalingFilter filter { get { return _filter; } set { _filter = value; } }
        public InvalidateClbck invalidateClbck { set { _customInvalidateClbck = value; } }

        internal invalidate_clbck_t nativeInvalidateClbck { set { _invalidateClbck = value; } }



        internal static CDORenderRequest toNative(RenderOptions options)
        {
            CDORenderRequest result = new CDORenderRequest();
            result.sinkId = StringHelper.toNative(options.sinkId);
            result.mirror = options.mirror;
            result.filter = StringHelper.toNative(options.filter.StringValue);
            result.opaque = IntPtr.Zero;
            result.invalidateCallback = options._invalidateClbck;
            return result;
        }
    }

    public class DrawRequest
    {
        /**
         * Area of the target window to render on.
         */
        public int top;
        public int left;
        public int bottom;
        public int right;
        
        /**
         * Platform-specific window handle.
         */
        public IntPtr hdc;
        
        /**
         * Id of renderer which should be affected by the Draw request.
         */
        public int rendererId;

        internal CDODrawRequest toNative()
        {
            CDODrawRequest nReq = new CDODrawRequest();
            nReq.bottom = bottom;
            nReq.top = top;
            nReq.left = left;
            nReq.right = right;
            nReq.windowHandle = hdc;
            nReq.rendererId = rendererId;
            return nReq;
        }

    }


    
}
