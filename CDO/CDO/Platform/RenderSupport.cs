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

        private int _callIdGenerator;

        private Dictionary<int, PendingCall> _pendingCalls;

        private IntPtr _platformHandle;

        private Dictionary<int, WeakReference> _activeRenderers;       

        public RenderSupport(IntPtr platformHandle)
        {
            _pendingCalls = new Dictionary<int, PendingCall>();            
            _callIdGenerator = 0;
            _platformHandle = platformHandle;
            _activeRenderers = new Dictionary<int, WeakReference>();
        }

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

        public void renderSink(Responder<RenderingWidget> responder,
            RenderOptions options)
        {
            int callId = _callIdGenerator++;
            RenderingWidget widget =
                new RenderingWidget(_platformHandle, onRendererPreDispose);
            _pendingCalls[callId] = new PendingCall(responder, widget);
            options.invalidateClbck = widget.getInvalidateClbck();
            CDORenderRequest nReq = RenderOptions.toNative(options);
            NativeAPI.cdo_render_sink(renderResponder, _platformHandle,
                new IntPtr(callId), ref nReq);
        }
        
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
        

        private void onRendererPreDispose(int rendererId)
        {
            _activeRenderers.Remove(rendererId);
        }

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
    }

    

}
