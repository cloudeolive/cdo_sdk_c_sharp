/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CDO
{
    internal class CloudeoServiceImpl : CloudeoService
    {
        #region Members

        private IntPtr _platformHandle;
        private Dictionary<uint, object> _respondersDictionary;
        private uint _enumarator;

        private cdo_void_rclbck_t _cdo_void_result_callback_t;
        private cdo_string_rclbck_t
                _cdo_string_result_callback_t;
        private cdo_int_rclbck_t
                _cdo_int_result_callback_t;
        private cdo_get_device_names_rclbck_t
                _cdo_get_device_names_result_callback_t;

        private CloudeoServiceListener _cloudeoServiceListener;

        private List<NativeServiceListenerAdapter> _listeners;

        #endregion


        #region Constructors

        public CloudeoServiceImpl()
        {
            _platformHandle = IntPtr.Zero;
            InitCloudeoServiceImpl();
        }

        public CloudeoServiceImpl(IntPtr platformHnandle)
        {
            _platformHandle = platformHnandle;
            InitCloudeoServiceImpl();
        }

        private void InitCloudeoServiceImpl()
        {
            _respondersDictionary = new Dictionary<uint, object>();
            _listeners = new List<NativeServiceListenerAdapter>();
            _enumarator = 0;

            _cdo_void_result_callback_t =
                new cdo_void_rclbck_t(cdo_void_result_callback_t);
            _cdo_string_result_callback_t =
                new cdo_string_rclbck_t(cdo_string_result_callback_t);
            _cdo_int_result_callback_t =
                new cdo_int_rclbck_t(cdo_int_result_callback_t);
            _cdo_get_device_names_result_callback_t =
                new cdo_get_device_names_rclbck_t(
                      cdo_get_device_names_result_callback_t);

            
        }

        #endregion


        #region Methods

        private uint saveResponder(object responder)
        {
            _respondersDictionary.Add(_enumarator, responder);
            _enumarator++;
            return _enumarator - 1;
        }

        private object getResponder(uint id)
        {
            object result;
            try
            {
                if (_respondersDictionary.TryGetValue(id, out result))
                {
                    _respondersDictionary.Remove(id);
                    return result;
                }
                else
                    return null;
            }
            catch (ArgumentNullException) 
            {
                return null;
            }
        }

        #endregion


        #region CloudeoService interface implementation

        public void getVersion(Responder<string> responder)
        {
            NativeAPI.cdo_get_version(_cdo_string_result_callback_t,
            _platformHandle, new IntPtr(saveResponder(responder)));
        }


        public void addServiceListener(Responder<object> responder,
                                       CloudeoServiceListener listener)
        {
            NativeServiceListenerAdapter listenerAdapter = 
                new NativeServiceListenerAdapter(listener);
            CDOServiceListener listenerNative = listenerAdapter.toNative();
            NativeAPI.cdo_add_service_listener(
                _cdo_void_result_callback_t,
                _platformHandle,
                new IntPtr(saveResponder(responder)),
                ref listenerNative);
            _listeners.Add(listenerAdapter);
        }


        public void sendEchoNotification(Responder<object> responder,
                                         string content)
        {
            CDOString cont = StringHelper.toNative(content);
            NativeAPI.cdo_send_echo_notification(
                _cdo_void_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(responder)), ref cont);
        }


        public void getAudioCaptureDevice(Responder<string> responder)
        {
            NativeAPI.cdo_get_audio_capture_device(
                _cdo_string_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        public void getAudioCaptureDeviceNames(
            Responder<Dictionary<string, string>> responder)
        {
            NativeAPI.cdo_get_audio_capture_device_names(
                _cdo_get_device_names_result_callback_t,
                _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        public void setAudioCaptureDevice(Responder<object> responder,
            string deviceId)
        {
            CDOString devId = StringHelper.toNative(deviceId);
            NativeAPI.cdo_set_audio_capture_device(
                _cdo_void_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(responder)), ref devId);
        }


        public void getAudioOutputDevice(Responder<string> respodner)
        {
            NativeAPI.cdo_get_audio_output_device(
                _cdo_string_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(respodner)));
        }

        public void getAudioOutputDeviceNames(
            Responder<Dictionary<string, string>> responder)
        {
            NativeAPI.cdo_get_audio_output_device_names(
                _cdo_get_device_names_result_callback_t,
                _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        public void setAudioOutputDevice(Responder<object> responder,
            string deviceId)
        {
            CDOString devId = StringHelper.toNative(deviceId);
            NativeAPI.cdo_set_audio_output_device(
                _cdo_void_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(responder)), ref devId);
        }


        public void getVideoCaptureDevice(Responder<string> respodner)
        {
            NativeAPI.cdo_get_video_capture_device(
                _cdo_string_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(respodner)));
        }

        public void getVideoCaptureDeviceNames(
            Responder<Dictionary<string, string>> responder)
        {
            NativeAPI.cdo_get_video_capture_device_names(
                _cdo_get_device_names_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        public void setVideoCaptureDevice(Responder<object> responder,
            string deviceId)
        {
            CDOString devId = StringHelper.toNative(deviceId);
            NativeAPI.cdo_set_video_capture_device(
                _cdo_void_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(responder)), ref devId);
        }


        public void getScreenCaptureSources(
            Responder<List<ScreenCaptureSource>> responder,
            int thumbWidth)
        {
            // TODO: implement in future
        }


        public void startLocalVideo(Responder<string> responder)
        {
            NativeAPI.cdo_start_local_video(
                _cdo_string_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        public void stopLocalVideo(Responder<object> responder)
        {
            NativeAPI.cdo_stop_local_video(
                _cdo_void_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }


        public void connect(Responder<object> responder,
            ConnectionDescription connDescription)
        {
            CDOString connDescriptorString =
                StringHelper.toNative(connDescription.toJSON());
            NativeAPI.cdo_connect_string(
                _cdo_void_result_callback_t,
                _platformHandle,
                new IntPtr(saveResponder(responder)),
                ref connDescriptorString);
        }

        public void disconnect(Responder<object> responder, string scopeId)
        {
            CDOString scpId = StringHelper.toNative(scopeId);
            NativeAPI.cdo_disconnect(_cdo_void_result_callback_t,
                _platformHandle, new IntPtr(saveResponder(responder)),
                ref scpId);
        }

        public void publish(Responder<object> responder, string scopeId,
            MediaType mediaType, MediaPublishOptions options)
        {
            CDOString scpId = StringHelper.toNative(scopeId);
            CDOString mediaTp =
                StringHelper.toNative(mediaType.StringValue);
            CDOMediaPublishOptions mediaPublishOpts =
                MediaPublishOptions.toNative(options);
            NativeAPI.cdo_publish(_cdo_void_result_callback_t,
                _platformHandle, new IntPtr(saveResponder(responder)),
                ref scpId, ref mediaTp, ref mediaPublishOpts);
        }

        public void unpublish(Responder<object> responder, string scopeId,
            MediaType mediaType)
        {
            CDOString scpId = StringHelper.toNative(scopeId);
            CDOString mediaTp =
                StringHelper.toNative(mediaType.StringValue);
            NativeAPI.cdo_unpublish(_cdo_void_result_callback_t,
                _platformHandle, new IntPtr(saveResponder(responder)),
                ref scpId, ref mediaTp);
        }


        public void sendMessage(Responder<object> responder, string scopeId,
            string message, long targetUserId)
        {
            CDOString scpId = StringHelper.toNative(scopeId);
            UIntPtr msgSz =
                new UIntPtr((message != null) ? (uint)message.Length : 0u);
            NativeAPI.cdo_send_message(_cdo_void_result_callback_t,
                _platformHandle, new IntPtr(saveResponder(responder)),
                ref scpId, message, msgSz, ref targetUserId);
        }


        public void getMicrophoneVolume(Responder<int> responder)
        {
            // TODO: implement in future
        }

        public void monitorMicActivity(Responder<object> responder,
            bool enabled)
        {
            // TODO: implement in future
        }

        public void setMicrophoneVolume(Responder<object> responder, int volume)
        {
            // TODO: implement in future
        }

        public void getSpeakersVolume(Responder<int> responder)
        {
            NativeAPI.cdo_get_volume(_cdo_int_result_callback_t,
                _platformHandle, new IntPtr(saveResponder(responder)));
        }

        public void setSpeakersVolume(Responder<object> responder, int volume)
        {
            NativeAPI.cdo_set_volume(_cdo_void_result_callback_t,
                _platformHandle, new IntPtr(saveResponder(responder)), volume);
        }


        public void startMeasuringStatistics(Responder<object> responder)
        {
            // TODO: implement in future
        }

        public void stopMeasuringStatistics(Responder<object> responder)
        {
            // TODO: implement in future
        }


        public void startPlayingTestSound(Responder<object> responder)
        {
            NativeAPI.cdo_start_playing_test_sound(
                _cdo_void_result_callback_t,
                _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        public void stopPlayingTestSound(Responder<object> responder)
        {
            NativeAPI.cdo_stop_playing_test_sound(
                _cdo_void_result_callback_t, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        #endregion


        #region NativeAPI callback handlers

        private void cdo_void_result_callback_t(IntPtr opaque,
            ref CDOError error)
        {
            Responder<object> responder =
                (Responder<object>) getResponder((uint)opaque);

            if (error.err_code == 0)
                responder.resultHandler(null);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        private void cdo_string_result_callback_t(IntPtr opaque,
            ref CDOError error,
            ref CDOString str)
        {
            Responder<string> responder =
                (Responder<string>)getResponder((uint)opaque);

            if (error.err_code == 0)
                responder.resultHandler(str.body);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        private void cdo_int_result_callback_t(IntPtr opaque,
            ref CDOError error, int i)
        {
            Responder<int> responder =
                (Responder<int>)getResponder((uint)opaque);

            if (error.err_code == 0)
                responder.resultHandler(i);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        private void cdo_get_device_names_result_callback_t(IntPtr opaque,
            ref CDOError error, IntPtr device, UIntPtr size_t)
        {
            Responder<Dictionary<string, string>> responder =
                (Responder<Dictionary<string, string>>)
                getResponder((uint)opaque);

            Dictionary<string, string> devList =
                new Dictionary<string,string>();

            // 'device' is an array of 'CDODevice' structures, add devices to
            // devList
            var arrayValue = device;
            var tableEntrySize = Marshal.SizeOf(typeof(CDODevice));
            uint tableSize = (uint)size_t;
            for (var i = 0; i < tableSize; i++)
            {
                var cur = (CDODevice) Marshal.PtrToStructure(
                    arrayValue, typeof(CDODevice));
                devList.Add(cur.id.body, cur.label.body);
                arrayValue = new IntPtr(arrayValue.ToInt32() + tableEntrySize);
            }


            if (error.err_code == 0)
                responder.resultHandler(devList);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        #endregion
    }
}