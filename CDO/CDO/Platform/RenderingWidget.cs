using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CDO
{
   
    internal delegate void PreDisposeHandlerDelegate(int rendererId);

    /// <summary>
    /// 
    /// </summary>
    public class RenderingWidget : Control
    {

        #region ClassMembers
        
        private IntPtr _platformHandle;
        private int _rendererId;
        private EventWaitHandle stoppedEvent;
        private invalidate_clbck_t _invalidateCallback;
        private PreDisposeHandlerDelegate _preDisposeDelegate;
        
        #endregion

        #region ConstructorsDestructors
        internal RenderingWidget(IntPtr platformHandle, PreDisposeHandlerDelegate preDisposeDelegate)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            _rendererId = -1;
            _platformHandle = platformHandle;
            Paint += draw;
            _invalidateCallback = new invalidate_clbck_t(invalidateClbck);
            _preDisposeDelegate = preDisposeDelegate;
        }


        ~RenderingWidget()
        {
            stop();
        }

        #endregion 

        #region PublicAPI
        /// <summary>
        /// 
        /// </summary>
        public void stop()
        {
            stop(true);
        }
        #endregion

        #region PrivateHelpers


        internal void stop(bool runPreDisposeDelegate)
        {
            if (_rendererId < 0)
            {
                return;
            }
            stoppedEvent = new ManualResetEvent(false);
            NativeAPI.cdo_stop_render(stopRHandler, _platformHandle, IntPtr.Zero, _rendererId);
            stoppedEvent.WaitOne(2000);
            if(runPreDisposeDelegate)
                _preDisposeDelegate(_rendererId);
            _rendererId = -1;
        }

        internal invalidate_clbck_t getInvalidateClbck()
        {
            return _invalidateCallback;
        }

        private void invalidateClbck(IntPtr opaque)
        {
            Invalidate();
        }

        internal int rendererId { set { _rendererId = value; } }

        private void draw(object sender, PaintEventArgs e)
        {
            if (_rendererId < 0)
                return;

            if (Width < 8 || Height < 8)
                return;

            IntPtr dc = e.Graphics.GetHdc();

            CDODrawRequest drawR = new CDODrawRequest();
            drawR.rendererId = _rendererId;
            drawR.windowHandle = dc;
            drawR.left = 0;
            drawR.right = Width;
            drawR.top = 0;
            drawR.bottom = Height;
            NativeAPI.cdo_draw(_platformHandle, ref drawR);
            e.Graphics.ReleaseHdc(dc);
        }

        private void stopRHandler(IntPtr opaque, ref CDOError error) 
        {
            stoppedEvent.Set();
        }

        #endregion
    }
}
