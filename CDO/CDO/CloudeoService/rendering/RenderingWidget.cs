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

        private ManualRenderer manualRenderer;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platformHandle"></param>
        /// <param name="preDisposeDelegate"></param>
        internal RenderingWidget(ManualRenderer manualRenderer)
        {
            this.manualRenderer = manualRenderer;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            Paint += draw;
            manualRenderer.Invalidated += newFrame;
        }

        /// <summary>
        /// 
        /// </summary>
        ~RenderingWidget()
        {
            
        }

        #endregion 

        #region PublicAPI
        
        /// <summary>
        /// 
        /// </summary>
        public void stop()
        {
            manualRenderer.stop();
        }
        
        #endregion

        #region Private helpers

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void draw(object sender, PaintEventArgs e)
        {
            IntPtr dc = e.Graphics.GetHdc();
            DrawRequest drawR = new DrawRequest();
            drawR.hdc = dc;
            drawR.left = 0;
            drawR.right = Width;
            drawR.top = 0;
            drawR.bottom = Height;
            manualRenderer.draw(drawR);
            e.Graphics.ReleaseHdc(dc);
        }

        private void newFrame(object sender, EventArgs ea)
        {
            Invalidate();
        }

        #endregion
    }
}
