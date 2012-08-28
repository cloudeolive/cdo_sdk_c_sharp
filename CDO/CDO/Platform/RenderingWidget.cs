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

        #region Members
        
        /// <summary>
        /// 
        /// </summary>
        private IntPtr _platformHandle;
        
        /// <summary>
        /// 
        /// </summary>
        private int _rendererId;
        
        /// <summary>
        /// 
        /// </summary>
        private EventWaitHandle stoppedEvent;

        /// <summary>
        /// 
        /// </summary>
        private invalidate_clbck_t _invalidateCallback;
        private PreDisposeHandlerDelegate _preDisposeDelegate;

        /// <summary>
        /// 
        /// </summary>
        private cdo_void_rclbck_t _stopRHandler;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platformHandle"></param>
        /// <param name="preDisposeDelegate"></param>
        internal RenderingWidget(IntPtr platformHandle,
            PreDisposeHandlerDelegate preDisposeDelegate)
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

        /// <summary>
        /// 
        /// </summary>
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

        #region Private helpers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runPreDisposeDelegate"></param>
        internal void stop(bool runPreDisposeDelegate)
        {
            if (_rendererId < 0)
            {
                return;
            }
            stoppedEvent = new ManualResetEvent(false);
            _stopRHandler = new cdo_void_rclbck_t(stopRHandler);
            NativeAPI.cdo_stop_render(_stopRHandler, _platformHandle,
                IntPtr.Zero, _rendererId);
            stoppedEvent.WaitOne(2000);
            if(runPreDisposeDelegate)
                _preDisposeDelegate(_rendererId);
            _rendererId = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal invalidate_clbck_t getInvalidateClbck()
        {
            return _invalidateCallback;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opaque"></param>
        private void invalidateClbck(IntPtr opaque)
        {
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        internal int rendererId { set { _rendererId = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opaque"></param>
        /// <param name="error"></param>
        private void stopRHandler(IntPtr opaque, ref CDOError error) 
        {
            stoppedEvent.Set();
        }

        #endregion
    }
}
