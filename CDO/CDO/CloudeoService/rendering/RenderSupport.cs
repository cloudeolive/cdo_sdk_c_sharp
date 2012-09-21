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
                    ((ManualRenderer)entry.Value.Target).stop(false);
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
            PendingCall call = new PendingCall(responder);
            renderSinkInternal(call, options);
        }

        public void manualRenderSink(Responder<ManualRenderer> responder, RenderOptions options)
        {
            PendingCall call = new PendingCall(responder);
            renderSinkInternal(call, options);
        }

        #endregion

        #region Private helpers
        /**
         * Private helpers
         * =====================================================================
         */

        private void renderSinkInternal(PendingCall call, RenderOptions options)
        {
            int callId = _callIdGenerator++;
            ManualRenderer renderer = new ManualRenderer(_platformHandle, onRendererPreDispose);
            call.manualRenderer = renderer;
            _pendingCalls[callId] = call;
            CDORenderRequest nReq = RenderOptions.toNative(options);
            nReq.invalidateCallback = renderer.getInvalidateClbck();
            Console.Error.WriteLine("Requesting SDK to start rendering sink");
            NativeAPI.cdo_render_sink(_renderResponder, _platformHandle,
                new IntPtr(callId), ref nReq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opaque"></param>
        /// <param name="error"></param>
        /// <param name="i"></param>
        private void renderResponder(IntPtr opaque, ref CDOError error, int i)
        {
            try
            {
                Console.Error.WriteLine("Got rendering result");
                int callId = (int)opaque;
                PendingCall call = _pendingCalls[callId];
                _pendingCalls.Remove(callId);
                if (error.err_code == 0)
                {
                    call.manualRenderer.rendererId = i;
                    _activeRenderers[i] = new WeakReference(call.manualRenderer);
                    if (call.manual)
                    {
                        call.manualRendererResponder.resultHandler(call.manualRenderer);
                    }
                    else
                    {
                        call.rendererResponder.resultHandler(
                            new RenderingWidget(call.manualRenderer));
                    }
                }
                else
                {
                    if (call.manual)
                        call.manualRendererResponder.errHandler(error.err_code,
                        StringHelper.fromNative(error.err_message));
                    else
                        call.rendererResponder.errHandler(error.err_code,
                        StringHelper.fromNative(error.err_message));
                }
            }
            catch (Exception) { }
            
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
            public Responder<RenderingWidget> rendererResponder;
            public ManualRenderer manualRenderer;
            public Responder<ManualRenderer> manualRendererResponder;
            public bool manual;

            public PendingCall(Responder<RenderingWidget> rendererResponder 
                )
            {
                this.manual = false;
                this.rendererResponder = rendererResponder;
            }

            public PendingCall(Responder<ManualRenderer> rendererResponder)
            {
                this.manual = true;
                this.manualRendererResponder = rendererResponder;
            }
        }

        #endregion
    }    
}
