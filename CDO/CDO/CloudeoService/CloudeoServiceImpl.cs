
using System;
using System.Collections.Generic;
namespace CDO
{
    internal class CloudeoServiceImpl : CloudeoService
    {
        #region Members

        private IntPtr _platformHandle;
        private Dictionary<uint, object> _respondersDictionary;
        private uint _enumarator;


        private CloudeoSdkWrapper.void_rclbck_t _void_result_callback_t;
        private CloudeoSdkWrapper.cdo_void_rclbck_t _cdo_void_result_callback_t;
        private CloudeoSdkWrapper.cdo_void_rclbck_t2 _cdo_void_result_callback_t2;
        private CloudeoSdkWrapper.cdo_string_rclbck_t _cdo_string_result_callback_t;
        private CloudeoSdkWrapper.cdo_int_rclbck_t _cdo_int_result_callback_t;
        private CloudeoSdkWrapper.cdo_get_device_names_rclbck_t _cdo_get_device_names_result_callback_t;

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

            _void_result_callback_t = new CloudeoSdkWrapper.void_rclbck_t(void_result_callback_t);
            _cdo_void_result_callback_t = new CloudeoSdkWrapper.cdo_void_rclbck_t(cdo_void_result_callback_t);
            _cdo_void_result_callback_t2 = new CloudeoSdkWrapper.cdo_void_rclbck_t2(cdo_void_result_callback_t2);
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


        #region Method

        private uint saveResponder(object responder)
        {
            _respondersDictionary.Add(_enumarator++, responder);
            return _enumarator;
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
            //TODO: implement method
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
            CloudeoSdkWrapper.cdo_get_video_capture_device(_cdo_string_result_callback_t, _platformHandle, new IntPtr(saveResponder(responder)));
        }


        public void getScreenCaptureSources(Responder<System.Collections.Generic.List<ScreenCaptureSource>> responder, int thumbWidth)
        {
            // TODO: I can't find corresponding function in CloudeoSdkWrapper
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
            // TODO: implement mentod
        }


        public void getMicrophoneVolume(Responder<int> responder)
        {
            // TODO: implement mentod
        }

        public void getSpeakersVolume(Responder<int> responder)
        {
            // TODO: implement mentod
        }

        public void monitorMicActivity(Responder<object> responder, bool enabled)
        {
            // TODO: implement mentod
        }

        public void setMicrophoneVolume(Responder<object> responder, int volume)
        {
            // TODO: implement mentod
        }

        public void setSpeakersVolume(Responder<object> responder, int volume)
        {
            // TODO: implement mentod
        }


        public void startMeasuringStatistics(Responder<object> responder)
        {
            // TODO: implement mentod
        }

        public void stopMeasuringStatistics(Responder<object> responder)
        {
            // TODO: implement mentod
        }


        public void startPlayingTestSound(Responder<object> responder)
        {
            // TODO: implement mentod
        }

        public void stopPlayingTestSound(Responder<object> responder)
        {
            // TODO: implement mentod
        }

        #endregion


        #region CloudeoSdkWrapper callback handlers

        private void void_result_callback_t(ref CloudeoSdkWrapper.CDOString str)
        {

        }

        private void cdo_void_result_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOError error)
        {

        }

        private void cdo_void_result_callback_t2(IntPtr opaque, IntPtr error)
        {

        }

        private void cdo_string_result_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOError error, ref CloudeoSdkWrapper.CDOString str)
        {

        }

        private void cdo_int_result_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOError error, int i)
        {

        }

        private void cdo_get_device_names_result_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOError error, ref CloudeoSdkWrapper.CDODevice device, UIntPtr size_t)
        {

        }


        private void on_video_frame_size_changed_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOVideoFrameSizeChangedEvent e)
        {

        }

        private void on_connection_lost_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOConnectionLostEvent e)
        {

        }

        private void on_user_event_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOUserStateChangedEvent e)
        {

        }

        private void on_media_stream_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOUserStateChangedEvent e)
        {

        }

        private void on_mic_activity_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMicActivityEvent e)
        {

        }

        private void on_mic_gain_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMicGainEvent e)
        {

        }

        private void on_device_list_changed_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDODeviceListChangedEvent e)
        {

        }

        private void on_media_stats_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMediaStatsEvent e)
        {

        }

        private void on_message_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMessageEvent e)
        {

        }

        private void on_media_conn_type_changed_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOMediaConnTypeChangedEvent e)
        {

        }

        private void on_echo_callback_t(IntPtr opaque, ref CloudeoSdkWrapper.CDOEchoEvent e)
        {

        }

        #endregion
    }
}