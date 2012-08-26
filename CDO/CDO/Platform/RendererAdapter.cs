using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CDO
{
    class RendererAdapter
    {

        private RenderOptions _options;
        int _rendererId;
        IntPtr _platformHandle;


        public RendererAdapter(IntPtr platformHandle, RenderOptions options)
        {
            _options = options;
            options.invalidateClbck = invalidate;
            options.container.Paint += paint;
            _platformHandle = platformHandle;
        }

        internal int rendererId { set { _rendererId = value; } }


        void invalidate(IntPtr opaque)
        {
            _options.container.Invalidate();
        }

        private void paint(object sender, PaintEventArgs e)
        {
            IntPtr dc = e.Graphics.GetHdc();
            
            CDODrawRequest drawR = new CDODrawRequest();
            drawR.rendererId = _rendererId;
            drawR.windowHandle = dc;
            drawR.left = 0;
            drawR.right = _options.container.Width;
            drawR.top = 0;
            drawR.bottom = _options.container.Height;
            NativeAPI.cdo_draw(_platformHandle, ref drawR);
            e.Graphics.ReleaseHdc(dc);
        }
    }
}
