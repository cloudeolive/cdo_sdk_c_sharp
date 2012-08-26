using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    class RenderSupport
    {

        private class PendingCall
        {
            public PendingCall(object responder)
            {
                this.responder = responder;
                
            }

            public object responder;
            public RendererAdapter renderer;
            public int rendererId;
        }



        private int _callIdGenerator;

        private Dictionary<int, PendingCall> _pendingCalls;

        private IntPtr _platformHandle;

        private Dictionary<int, RendererAdapter> _activeRenderers;


        public RenderSupport(IntPtr platformHandle)
        {
            _pendingCalls = new Dictionary<int, PendingCall>();
            _activeRenderers = new Dictionary<int, RendererAdapter>(); 
            _callIdGenerator = 0;
            _platformHandle = platformHandle;        
        }

        public void renderSink(Responder<int> responder, RenderOptions options)
        {
            RendererAdapter adapter = new RendererAdapter(_platformHandle, options);
            PendingCall call = new PendingCall(responder);
            call.renderer = adapter;
            int callId = _callIdGenerator++;
            _pendingCalls[callId] = call; 
            CDORenderRequest nReq = RenderOptions.toNative(options);
            NativeAPI.cdo_render_sink(renderResponder, _platformHandle, new IntPtr(callId), ref nReq);

        }

        public void stopRender(Responder<object> responder, int rendererId)
        {
            PendingCall call = new PendingCall(responder);
            call.rendererId = rendererId;
            int callId = _callIdGenerator++;
            _pendingCalls[callId] = call;
            NativeAPI.cdo_stop_render(stopRenderResponder, _platformHandle, new IntPtr(callId), rendererId);
        }


        private void renderResponder(IntPtr opaque, ref CDOError error, int i)
        {
            int callId = (int)opaque;
            PendingCall call = _pendingCalls[callId];
            if (error.err_code == 0)
            {
                _activeRenderers[i] = call.renderer;
                ((Responder<int>)call.responder).resultHandler(i);
            }
            else
            {
                ((Responder<int>)call.responder).errHandler(error.err_code, 
                    StringHelper.fromNative(error.err_message));
            }
            _pendingCalls.Remove(callId);
        }

        private void stopRenderResponder(IntPtr opaque, ref CDOError error)
        {
            int callId = (int)opaque;
            PendingCall call = _pendingCalls[callId];
            Responder<object> responder = (Responder<object>) call.responder;
            if (error.err_code == 0)
            {
                responder.resultHandler(null);
            }
            else
            {
                responder.errHandler(error.err_code, 
                    StringHelper.fromNative(error.err_message));
            }
            _activeRenderers.Remove(call.rendererId);
            _pendingCalls.Remove(callId);
        }
    }
}
