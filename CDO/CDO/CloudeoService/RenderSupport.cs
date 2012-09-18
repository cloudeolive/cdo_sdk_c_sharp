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

namespace CDO
{
    class RenderSupport
    {

        #region Members
        /**
         * Members
         * =====================================================================
         */
        
        /// <summary>
        /// 
        /// </summary>
        private int _callIdGenerator;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<int, PendingCall> _pendingCalls;

        /// <summary>
        /// 
        /// </summary>
        private IntPtr _platformHandle;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<int, WeakReference> _activeRenderers;

        /// <summary>
        /// 
        /// </summary>
        private cdo_int_rclbck_t _renderResponder;

        #endregion

        #region Constructors
        /**
         * Constructors
         * =====================================================================
         */
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="platformHandle"></param>
        public RenderSupport(IntPtr platformHandle)
        {
            _pendingCalls = new Dictionary<int, PendingCall>();
            _renderResponder = new cdo_int_rclbck_t(renderResponder);
            _callIdGenerator = 0;
            _platformHandle = platformHandle;
            _activeRenderers = new Dictionary<int, WeakReference>();
        }

        #endregion

        #region Public API
        /**
         * Public API
         * =====================================================================
         */        

        /// <summary>
        /// 
        /// </summary>
        public void shutdown()
        {
            foreach (KeyValuePair<int, WeakReference> entry in _activeRenderers)
            {
                if (entry.Value.IsAlive)
                {
                    ((RenderingWidget)entry.Value.Target).stop(false);
                }
            }
            _activeRenderers.Clear();

        }

        // =====================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="options"></param>
        public void renderSink(Responder<RenderingWidget> responder,
            RenderOptions options)
        {
            int callId = _callIdGenerator++;
            RenderingWidget widget =
                new RenderingWidget(_platformHandle, onRendererPreDispose);
            _pendingCalls[callId] = new PendingCall(responder, widget);
            options.nativeInvalidateClbck = widget.getInvalidateClbck();
            CDORenderRequest nReq = RenderOptions.toNative(options);
            NativeAPI.cdo_render_sink(_renderResponder, _platformHandle,
                new IntPtr(callId), ref nReq);
        }

        #endregion

        #region Private helpers
        /**
         * Private helpers
         * =====================================================================
         */
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opaque"></param>
        /// <param name="error"></param>
        /// <param name="i"></param>
        private void renderResponder(IntPtr opaque, ref CDOError error, int i)
        {
            int callId = (int)opaque;
            PendingCall call = _pendingCalls[callId];
            if (error.err_code == 0)
            {
                call.renderer.rendererId = i;
                _activeRenderers[i] = new WeakReference(call.renderer);
                call.responder.resultHandler(call.renderer);
            }
            else
            {
                call.responder.errHandler(error.err_code, 
                    StringHelper.fromNative(error.err_message));
            }
            _pendingCalls.Remove(callId);
        }

        // =====================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rendererId"></param>
        private void onRendererPreDispose(int rendererId)
        {
            _activeRenderers.Remove(rendererId);
        }

        #endregion

        #region class PendingCall
        /**
         * PendingCall helper class
         * =====================================================================
         */
        
        /// <summary>
        /// 
        /// </summary>
        private class PendingCall
        {
            public Responder<RenderingWidget> responder;
            public RenderingWidget renderer;

            public PendingCall(Responder<RenderingWidget> responder,
                RenderingWidget renderer)
            {
                this.responder = responder;
                this.renderer = renderer;
            }

        }

        #endregion
    }    
}
