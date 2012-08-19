
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

        private CloudeoSdkWrapper.cdo_void_rclbck_t _cdo_void_result_callback_t;
        private CloudeoSdkWrapper.cdo_string_rclbck_t _cdo_string_result_callback_t;
        private CloudeoSdkWrapper.cdo_int_rclbck_t _cdo_int_result_callback_t;
        private CloudeoSdkWrapper.cdo_get_device_names_rclbck_t _cdo_get_device_names_result_callback_t;

        private CloudeoServiceListener _cloudeoServiceListener;

        private CloudeoSdkWrapper.on_video_frame_size_changed_clbck_t _on_video_frame_size_changed_callback_t;
        private CloudeoSdkWrapper.on_connection_lost_clbck_t _on_connection_lost_callback_t;
        private CloudeoSdkWrapper.on_user_event_clbck_t _on_user_event_callback_t;
        private CloudeoSdkWrapper.on_media_stream_clbck_t _on_media_stream_callback_t;
        private CloudeoSdkWrapper.on_mic_activity_clbck_t _on_mic_activity_callback_t;
        private CloudeoSdkWrapper.on_mic_gain_clbck_t _on_mic_gain_callback_t;
        private CloudeoSdkWrapper.on_device_list_changed_clbck_t _on_device_list_changed_callback_t;
        private CloudeoSdkWrapper.on_media_stats_clbck_t _on_media_stats_callback_t;
        private CloudeoSdkWrapper.on_message_clbck_t _on_message_callback_t;
        private CloudeoSdkWrapper.on_media_conn_type_changed_clbck_t _on_media_conn_type_changed_callback_t;
        private CloudeoSdkWrapper.on_echo_clbck_t _on_echo_callback_t;

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
            _enumarator = 0;

            _cdo_void_result_callback_t = new CloudeoSdkWrapper.cdo_void_rclbck_t(cdo_void_result_callback_t);
            _cdo_string_result_callback_t = new CloudeoSdkWrapper.cdo_string_rclbck_t(cdo_string_result_callback_t);
            _cdo_int_result_callback_t = new CloudeoSdkWrapper.cdo_int_rclbck_t(cdo_int_result_callback_t);
            _cdo_get_device_names_result_callback_t = new CloudeoSdkWrapper.cdo_get_device_names_rclbck_t(cdo_get_device_names_result_callback_t);

            _on_video_frame_size_changed_callback_t = new CloudeoSdkWrapper.on_video_frame_size_changed_clbck_t(on_video_frame_size_changed_callback_t);
            _on_connection_lost_callback_t = new CloudeoSdkWrapper.on_connection_lost_clbck_t(on_connection_lost_callback_t);
            _on_user_event_callback_t = new CloudeoSdkWrapper.on_user_event_clbck_t(on_user_event_callback_t);
            _on_media_stream_callback_t = new CloudeoSdkWrapper.on_media_stream_clbck_t(on_media_stream_callback_t);
            _on_mic_activity_callback_t = new CloudeoSdkWrapper.on_mic_activity_clbck_t(on_mic_activity_callback_t);
            _on_mic_gain_callback_t = new CloudeoSdkWrapper.on_mic_gain_clbck_t(on_mic_gain_callback_t);
            _on_device_list_changed_callback_t = new CloudeoSdkWrapper.on_device_list_changed_clbck_t(on_device_list_changed_callback_t);
            _on_media_stats_callback_t = new CloudeoSdkWrapper.on_media_stats_clbck_t(on_media_stats_callback_t);
            _on_message_callback_t = new CloudeoSdkWrapper.on_message_clbck_t(on_message_callback_t);
            _on_media_conn_type_changed_callback_t = new CloudeoSdkWrapper.on_media_conn_type_changed_clbck_t(on_media_conn_type_changed_callback_t);
            _on_echo_callback_t = new CloudeoSdkWrapper.on_echo_clbck_t(on_echo_callback_t);
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
            CloudeoSdkWrapper.cdo_get_version(_cdo_string_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }


        public void addServiceListener(Responder<object> responder, CloudeoServiceListener listener)
        {
            _cloudeoServiceListener = listener;
            CloudeoSdkWrapper.CDOServiceListener lstnr = new CloudeoSdkWrapper.CDOServiceListener();
            lstnr.opaque = IntPtr.Zero;
            lstnr.onConnectionLost = _on_connection_lost_callback_t;
            lstnr.onDeviceListChanged = _on_device_list_changed_callback_t;
            lstnr.onEcho = _on_echo_callback_t;
            lstnr.onMediaConnTypeChanged = _on_media_conn_type_changed_callback_t;
            lstnr.onMediaStats = _on_media_stats_callback_t;
            lstnr.onMediaStreamEvent = _on_media_stream_callback_t;
            lstnr.onMessage = _on_message_callback_t;
            lstnr.onMicActivity = _on_mic_activity_callback_t;
            lstnr.onMicGain = _on_mic_gain_callback_t;
            lstnr.onUserEvent = _on_user_event_callback_t;
            lstnr.onVideoFrameSizeChanged = _on_video_frame_size_changed_callback_t;
            CloudeoSdkWrapper.cdo_add_service_listener(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref lstnr);
        }


        public void sendEchoNotification(Responder<object> responder, string content)
        {
            CloudeoSdkWrapper.CDOString cont = StringHelper.toNative(content);
            CloudeoSdkWrapper.cdo_send_echo_notification(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref cont);
        }


        public void getAudioCaptureDevice(Responder<string> responder)
        {
            CloudeoSdkWrapper.cdo_get_audio_capture_device(_cdo_string_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }

        public void getAudioCaptureDeviceNames(Responder<System.Collections.Generic.Dictionary<string, string>> responder)
        {
            CloudeoSdkWrapper.cdo_get_audio_capture_device_names(_cdo_get_device_names_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }

        public void setAudioCaptureDevice(Responder<object> responder, string deviceId)
        {
            CloudeoSdkWrapper.CDOString devId = StringHelper.toNative(deviceId);
            CloudeoSdkWrapper.cdo_set_audio_capture_device(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref devId);
        }


        public void getAudioOutputDevice(Responder<string> respodner)
        {
            CloudeoSdkWrapper.cdo_get_audio_output_device(_cdo_string_result_callback_t, _platformHandle, new IntPtr(saveResponder(respodner)));
        }

        public void getAudioOutputDeviceNames(Responder<System.Collections.Generic.Dictionary<string, string>> responder)
        {
            CloudeoSdkWrapper.cdo_get_audio_output_device_names(_cdo_get_device_names_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }

        public void setAudioOutputDevice(Responder<object> responder, string deviceId)
        {
            CloudeoSdkWrapper.CDOString devId = StringHelper.toNative(deviceId);
            CloudeoSdkWrapper.cdo_set_audio_output_device(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref devId);
        }


        public void getVideoCaptureDevice(Responder<string> respodner)
        {
            CloudeoSdkWrapper.cdo_get_video_capture_device(_cdo_string_result_callback_t, _platformHandle, new IntPtr(saveResponder(respodner)));
        }

        public void getVideoCaptureDeviceNames(Responder<System.Collections.Generic.Dictionary<string, string>> responder)
        {
            CloudeoSdkWrapper.cdo_get_video_capture_device_names(_cdo_get_device_names_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }

        public void setVideoCaptureDevice(Responder<object> responder, string deviceId)
        {
            CloudeoSdkWrapper.CDOString devId = StringHelper.toNative(deviceId);
            CloudeoSdkWrapper.cdo_set_video_capture_device(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref devId);
        }


        public void getScreenCaptureSources(Responder<System.Collections.Generic.List<ScreenCaptureSource>> responder, int thumbWidth)
        {
            // TODO: implement in future
        }


        public void startLocalVideo(Responder<string> responder)
        {
            CloudeoSdkWrapper.cdo_start_local_video(_cdo_string_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }

        public void stopLocalVideo(Responder<object> responder)
        {
            CloudeoSdkWrapper.cdo_stop_local_video(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }


        public void connect(Responder<object> responder, ConnectionDescription connDescription)
        {
            CloudeoSdkWrapper.CDOConnectionDescriptor connDescr = ConnectionDescription.toNative(connDescription);
            CloudeoSdkWrapper.cdo_connect(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref connDescr);
        }

        public void disconnect(Responder<object> responder, string scopeId)
        {
            CloudeoSdkWrapper.CDOString scpId = StringHelper.toNative(scopeId);
            CloudeoSdkWrapper.cdo_disconnect(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref scpId);
        }

        public void publish(Responder<object> responder, string scopeId, MediaType mediaType, MediaPublishOptions options)
        {
            CloudeoSdkWrapper.CDOString scpId = StringHelper.toNative(scopeId);
            CloudeoSdkWrapper.CDOString mediaTp = StringHelper.toNative(mediaType.StringValue);
            CloudeoSdkWrapper.CDOMediaPublishOptions mediaPublishOpts = MediaPublishOptions.toNative(options);
            CloudeoSdkWrapper.cdo_publish(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref scpId, ref mediaTp, ref mediaPublishOpts);
        }

        public void unpublish(Responder<object> responder, string scopeId, MediaType mediaType)
        {
            CloudeoSdkWrapper.CDOString scpId = StringHelper.toNative(scopeId);
            CloudeoSdkWrapper.CDOString mediaTp = StringHelper.toNative(mediaType.StringValue);
            CloudeoSdkWrapper.cdo_unpublish(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref scpId, ref mediaTp);
        }


        public void sendMessage(Responder<object> responder, string scopeId, string message, long targetUserId)
        {
            CloudeoSdkWrapper.CDOString scpId = StringHelper.toNative(scopeId);
            UIntPtr msgSz = new UIntPtr((message != null) ? (uint)message.Length : 0u);
            CloudeoSdkWrapper.cdo_send_message(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), ref scpId, message, msgSz, ref targetUserId);
        }


        public void getMicrophoneVolume(Responder<int> responder)
        {
            // TODO: implement in future
        }

        public void monitorMicActivity(Responder<object> responder, bool enabled)
        {
            // TODO: implement in future
        }

        public void setMicrophoneVolume(Responder<object> responder, int volume)
        {
            // TODO: implement in future
        }

        public void getSpeakersVolume(Responder<int> responder)
        {
            CloudeoSdkWrapper.cdo_get_volume(_cdo_int_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }

        public void setSpeakersVolume(Responder<object> responder, int volume)
        {
            CloudeoSdkWrapper.cdo_set_volume(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)), volume);
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
            CloudeoSdkWrapper.cdo_start_playing_test_sound(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }

        public void stopPlayingTestSound(Responder<object> responder)
        {
            CloudeoSdkWrapper.cdo_stop_playing_test_sound(_cdo_void_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }

        #endregion


        #region CloudeoSdkWrapper callback handlers

        private void cdo_void_result_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOError error)
        {
            Responder<object> responder = (Responder<object>) getResponder((uint)opaque);

            if (error.err_code == 0)
                responder.resultHandler(null);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        private void cdo_string_result_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOError error, ref CloudeoSdkWrapper.CDOString str)
        {
            Responder<string> responder = (Responder<string>)getResponder((uint)opaque);

            if (error.err_code == 0)
                responder.resultHandler(str.body);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        private void cdo_int_result_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOError error, int i)
        {
            Responder<int> responder = (Responder<int>)getResponder((uint)opaque);

            if (error.err_code == 0)
                responder.resultHandler(i);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        private void cdo_get_device_names_result_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOError error, IntPtr device, UIntPtr size_t)
        {
            Responder<System.Collections.Generic.Dictionary<string, string>> responder = (Responder<System.Collections.Generic.Dictionary<string, string>>)getResponder((uint)opaque);

            System.Collections.Generic.Dictionary<string, string> devList = new Dictionary<string,string>();

            // 'device' is an array of 'CDODevice' structures, add devices to devList
            var arrayValue = device;
            var tableEntrySize = Marshal.SizeOf(typeof(CloudeoSdkWrapper.CDODevice));
            uint tableSize = (uint)size_t;
            for (var i = 0; i < tableSize; i++)
            {
                var cur = (CloudeoSdkWrapper.CDODevice)Marshal.PtrToStructure(arrayValue, typeof(CloudeoSdkWrapper.CDODevice));
                devList.Add(cur.id.body, cur.label.body);
                arrayValue = new IntPtr(arrayValue.ToInt32() + tableEntrySize);
            }


            if (error.err_code == 0)
                responder.resultHandler(devList);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        // CDOServiceListener callback handlers

        private void on_video_frame_size_changed_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOVideoFrameSizeChangedEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onVideoFrameSizeChanged(VideoFrameSizeChangedEvent.FromNative(e));
        }

        private void on_connection_lost_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOConnectionLostEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onConnectionLost(ConnectionLostEvent.FromNative(e));
        }

        private void on_user_event_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOUserStateChangedEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onUserEvent(UserStateChangedEvent.FromNative(e));
        }

        private void on_media_stream_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOUserStateChangedEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onMediaStreamEvent(UserStateChangedEvent.FromNative(e));
        }

        private void on_mic_activity_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMicActivityEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onMicActivity(MicActivityEvent.FromNative(e));
        }

        private void on_mic_gain_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMicGainEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onMicGain(MicGainEvent.FromNative(e));
        }

        private void on_device_list_changed_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDODeviceListChangedEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onDeviceListChanged(DeviceListChangedEvent.FromNative(e));
        }

        private void on_media_stats_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMediaStatsEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onMediaStats(MediaStatsEvent.FromNative(e));
        }

        private void on_message_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMessageEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onMessage(MessageEvent.FromNative(e));
        }

        private void on_media_conn_type_changed_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMediaConnTypeChangedEvent e)
        {
            if (_cloudeoServiceListener != null)
                _cloudeoServiceListener.onMediaConnTypeChanged(MediaConnTypeChangedEvent.FromNative(e));
        }

        private void on_echo_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOEchoEvent e)
        {
            // TODO: implement maybe later
        }

        #endregion
    }
}