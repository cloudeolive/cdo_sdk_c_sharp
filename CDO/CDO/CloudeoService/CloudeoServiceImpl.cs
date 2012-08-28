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
    /// <summary>
    /// Implementation of the CloudeoService interface
    /// </summary>
    internal class CloudeoServiceImpl : CloudeoService
    {
        
        #region Members
        /**
         * Class members
         * =====================================================================
         */

        /// <summary>
        /// Platform handle - used for all the API calls
        /// </summary>
        private IntPtr _platformHandle;

        /// <summary>
        /// Dictionary that stores all the API pending calls. Required to 
        /// propertly handle asynchronous results.
        /// </summary>
        private Dictionary<uint, object> _respondersDictionary;

        /// <summary>
        /// Generator for call ids. Generates keys for the 
        /// _resopndersDictionary
        /// </summary>
        private uint _callIdGenerator;

        /// <summary>
        /// Void result callback delegate. Stored as a class member to prevent 
        /// deallocation.
        /// </summary>
        private cdo_void_rclbck_t _voidRCallback;
        
        /// <summary>
        /// String result callback delegate. Stored as a class member to prevent 
        /// deallocation.
        /// </summary>
        private cdo_string_rclbck_t _stringRCallback;
        
        /// <summary>
        /// Int result callback delegate. Stored as a class member to prevent 
        /// deallocation.
        /// </summary>
        private cdo_int_rclbck_t _intRCallback;
        
        /// <summary>
        /// Devices result callback delegate. Stored as a class member to prevent 
        /// deallocation.
        /// </summary>
        private cdo_get_device_names_rclbck_t _devsRCallback;
        
        /// <summary>
        /// List of all registered CloudeoServiceListener adapters. To prevent 
        /// deallocation when passing delegate to the native code.
        /// </summary>
        private List<NativeServiceListenerAdapter> _listeners;

        #endregion

        #region Constructors
        /**
         * Constructors
         * =====================================================================
         */

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="platformHnandle">
        /// handle for the platform that should be used when performing API 
        /// calls.
        /// </param>
        public CloudeoServiceImpl(IntPtr platformHnandle)
        {
            // 1. Initialize all fields.
            _platformHandle = platformHnandle;
            _respondersDictionary = new Dictionary<uint, object>();
            _listeners = new List<NativeServiceListenerAdapter>();
            _callIdGenerator = 0;

            // 2. Create all the result delegates.
            _voidRCallback = new cdo_void_rclbck_t(voidRCallback);
            _stringRCallback = new cdo_string_rclbck_t(stringRCallback);
            _intRCallback = new cdo_int_rclbck_t(intRCallback);
            _devsRCallback = new cdo_get_device_names_rclbck_t(devsRCallback);
        }

        #endregion        

        #region CloudeoService - basic logic
        /**
         * CloudeoService - basic logic
         * =====================================================================
         */

        /// <inheritdoc />
        public void getVersion(Responder<string> responder)
        {
            NativeAPI.cdo_get_version(_stringRCallback,
            _platformHandle, new IntPtr(saveResponder(responder)));
        }

        // =====================================================================

        /// <inheritdoc />
        public void addServiceListener(Responder<object> responder,
                                       CloudeoServiceListener listener)
        {
            NativeServiceListenerAdapter listenerAdapter = 
                new NativeServiceListenerAdapter(listener);
            CDOServiceListener listenerNative = listenerAdapter.toNative();
            NativeAPI.cdo_add_service_listener(
                _voidRCallback,
                _platformHandle,
                new IntPtr(saveResponder(responder)),
                ref listenerNative);
            _listeners.Add(listenerAdapter);
        }

        #endregion

        #region CloudeoService - basic logic
        /**
         * CloudeoService - Audio capture device
         * =====================================================================
         */

        /// <inheritdoc />
        public void getAudioCaptureDevice(Responder<string> responder)
        {
            NativeAPI.cdo_get_audio_capture_device(
                _stringRCallback, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        // =====================================================================

        /// <inheritdoc />
        public void getAudioCaptureDeviceNames(
            Responder<Dictionary<string, string>> responder)
        {
            NativeAPI.cdo_get_audio_capture_device_names(
                _devsRCallback,
                _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        // =====================================================================

        /// <inheritdoc />
        public void setAudioCaptureDevice(Responder<object> responder,
            string deviceId)
        {
            CDOString devId = StringHelper.toNative(deviceId);
            NativeAPI.cdo_set_audio_capture_device(
                _voidRCallback, 
                _platformHandle,
                new IntPtr(saveResponder(responder)), 
                ref devId);
        }

        #endregion

        #region CloudeoService - Audio output device
        /**
         * CloudeoService - Audio output device
         * =====================================================================
         */

        /// <inheritdoc />
        public void getAudioOutputDevice(Responder<string> respodner)
        {
            NativeAPI.cdo_get_audio_output_device(
                _stringRCallback, 
                _platformHandle,
                new IntPtr(saveResponder(respodner)));
        }

        // =====================================================================
        
        /// <inheritdoc />
        public void getAudioOutputDeviceNames(
            Responder<Dictionary<string, string>> responder)
        {
            NativeAPI.cdo_get_audio_output_device_names(
                _devsRCallback,
                _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        // =====================================================================

        /// <inheritdoc />
        public void setAudioOutputDevice(Responder<object> responder,
            string deviceId)
        {
            CDOString devId = StringHelper.toNative(deviceId);
            NativeAPI.cdo_set_audio_output_device(
                _voidRCallback, _platformHandle,
                new IntPtr(saveResponder(responder)), ref devId);
        }

        #endregion

        #region CloudeoService - Video capture device
        /**
         * CloudeoService - Video output device
         * =====================================================================
         */
      
        /// <inheritdoc />
        public void getVideoCaptureDevice(Responder<string> respodner)
        {
            NativeAPI.cdo_get_video_capture_device(
                _stringRCallback, _platformHandle,
                new IntPtr(saveResponder(respodner)));
        }

        // =====================================================================

        /// <inheritdoc />
        public void getVideoCaptureDeviceNames(
            Responder<Dictionary<string, string>> responder)
        {
            NativeAPI.cdo_get_video_capture_device_names(
                _devsRCallback, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        // =====================================================================

        /// <inheritdoc />
        public void setVideoCaptureDevice(Responder<object> responder,
            string deviceId)
        {
            CDOString devId = StringHelper.toNative(deviceId);
            NativeAPI.cdo_set_video_capture_device(
                _voidRCallback, _platformHandle,
                new IntPtr(saveResponder(responder)), ref devId);
        }

        #endregion


        /// <inheritdoc />
        public void getScreenCaptureSources(
            Responder<List<ScreenCaptureSource>> responder,
            int thumbWidth)
        {
            // TODO: implement in future
        }

        #region CloudeoService - Local video management
        /**
         * CloudeoService - Local video management
         * =====================================================================
         */

        /// <inheritdoc />
        public void startLocalVideo(Responder<string> responder)
        {
            NativeAPI.cdo_start_local_video(
                _stringRCallback, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        // =====================================================================

        /// <inheritdoc />
        public void stopLocalVideo(Responder<object> responder)
        {
            NativeAPI.cdo_stop_local_video(
                _voidRCallback, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        #endregion

        #region CloudeoService - Connectivity
        /**
         * CloudeoService - Connectivity
         * =====================================================================
         */

        /// <inheritdoc />
        public void connect(Responder<object> responder,
            ConnectionDescription connDescription)
        {
            CDOString connDescriptorString =
                StringHelper.toNative(connDescription.toJSON());
            NativeAPI.cdo_connect_string(
                _voidRCallback,
                _platformHandle,
                new IntPtr(saveResponder(responder)),
                ref connDescriptorString);
        }

        // =====================================================================

        /// <inheritdoc />
        public void disconnect(Responder<object> responder, string scopeId)
        {
            CDOString scpId = StringHelper.toNative(scopeId);
            NativeAPI.cdo_disconnect(_voidRCallback,
                _platformHandle, new IntPtr(saveResponder(responder)),
                ref scpId);
        }

        // =====================================================================

        /// <inheritdoc />
        public void publish(Responder<object> responder, string scopeId,
            MediaType mediaType, MediaPublishOptions options)
        {
            CDOString scpId = StringHelper.toNative(scopeId);
            CDOString mediaTp =
                StringHelper.toNative(mediaType.StringValue);
            CDOMediaPublishOptions mediaPublishOpts =
                MediaPublishOptions.toNative(options);
            NativeAPI.cdo_publish(_voidRCallback,
                _platformHandle, new IntPtr(saveResponder(responder)),
                ref scpId, ref mediaTp, ref mediaPublishOpts);
        }

        // =====================================================================

        /// <inheritdoc />
        public void unpublish(Responder<object> responder, string scopeId,
            MediaType mediaType)
        {
            CDOString scpId = StringHelper.toNative(scopeId);
            CDOString mediaTp =
                StringHelper.toNative(mediaType.StringValue);
            NativeAPI.cdo_unpublish(_voidRCallback,
                _platformHandle, new IntPtr(saveResponder(responder)),
                ref scpId, ref mediaTp);
        }

        // =====================================================================

        /// <inheritdoc />
        public void sendMessage(Responder<object> responder, string scopeId,
            string message, long targetUserId)
        {
            CDOString scpId = StringHelper.toNative(scopeId);
            UIntPtr msgSz =
                new UIntPtr((message != null) ? (uint)message.Length : 0u);
            NativeAPI.cdo_send_message(_voidRCallback,
                _platformHandle, new IntPtr(saveResponder(responder)),
                ref scpId, message, msgSz, ref targetUserId);
        }

        #endregion

        #region CloudeoService - Devices fine tune
        /**
         * CloudeoService - Devices fine tune
         * =====================================================================
         */


        /// <inheritdoc />
        public void getMicrophoneVolume(Responder<int> responder)
        {
            // TODO: implement in future
        }

        // =====================================================================

        /// <inheritdoc />
        public void monitorMicActivity(Responder<object> responder,
            bool enabled)
        {
            // TODO: implement in future
        }

        // =====================================================================

        /// <inheritdoc />
        public void setMicrophoneVolume(Responder<object> responder, int volume)
        {
            // TODO: implement in future
        }

        // =====================================================================

        /// <inheritdoc />
        public void getSpeakersVolume(Responder<int> responder)
        {
            NativeAPI.cdo_get_volume(_intRCallback,
                _platformHandle, new IntPtr(saveResponder(responder)));
        }

        // =====================================================================

        /// <inheritdoc />
        public void setSpeakersVolume(Responder<object> responder, int volume)
        {
            NativeAPI.cdo_set_volume(_voidRCallback,
                _platformHandle, new IntPtr(saveResponder(responder)), volume);
        }

        #endregion

        #region CloudeoService - Stats management
        /**
         * CloudeoService - Stats management
         * =====================================================================
         */

        /// <inheritdoc />
        public void startMeasuringStatistics(Responder<object> responder)
        {
            // TODO: implement in future
        }

        // =====================================================================

        /// <inheritdoc />
        public void stopMeasuringStatistics(Responder<object> responder)
        {
            // TODO: implement in future
        }


        /// <inheritdoc />
        public void startPlayingTestSound(Responder<object> responder)
        {
            NativeAPI.cdo_start_playing_test_sound(
                _voidRCallback,
                _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        /// <inheritdoc />
        public void stopPlayingTestSound(Responder<object> responder)
        {
            NativeAPI.cdo_stop_playing_test_sound(
                _voidRCallback, _platformHandle,
                new IntPtr(saveResponder(responder)));
        }

        #endregion

        /// <inheritdoc />
        public void sendEchoNotification(Responder<object> responder,
                                         string content)
        {
            CDOString cont = StringHelper.toNative(content);
            NativeAPI.cdo_send_echo_notification(
                _voidRCallback, _platformHandle,
                new IntPtr(saveResponder(responder)), ref cont);
        }

        #region Private helpers
        /**
         * Private helpers
         * =====================================================================
         */

        /// <summary>
        /// Generates an ID for the call and store given responder under this id
        /// </summary>
        /// <param name="responder">Responder to be stored.</param>
        /// <returns>Id of call</returns>
        private uint saveResponder(object responder)
        {
            uint callId = _callIdGenerator++;
            _respondersDictionary.Add(callId, responder);            
            return callId;
        }

        /// <summary>
        /// Returns a responder associated with call with given id.
        /// </summary>
        /// <param name="id">id of call to get related responder</param>
        /// <returns>responder associated with call with given id</returns>
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

        #region NativeAPI callback handlers
        /**
         * NativeAPI callback handlers
         * =====================================================================
         */
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opaque"></param>
        /// <param name="error"></param>
        private void voidRCallback(IntPtr opaque, ref CDOError error)
        {
            Responder<object> responder = (Responder<object>) getResponder(
                (uint)opaque);

            if (error.err_code == 0)
                responder.resultHandler(null);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        // =====================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opaque"></param>
        /// <param name="error"></param>
        /// <param name="str"></param>
        private void stringRCallback(IntPtr opaque,
            ref CDOError error,
            ref CDOString str)
        {
            Responder<string> responder = (Responder<string>) getResponder(
                (uint)opaque);

            if (error.err_code == 0)
                responder.resultHandler(str.body);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        // =====================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opaque"></param>
        /// <param name="error"></param>
        /// <param name="i"></param>
        private void intRCallback(IntPtr opaque,
            ref CDOError error, int i)
        {
            Responder<int> responder =
                (Responder<int>)getResponder((uint)opaque);

            if (error.err_code == 0)
                responder.resultHandler(i);
            else
                responder.errHandler(error.err_code, error.err_message.body);
        }

        // =====================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opaque"></param>
        /// <param name="error"></param>
        /// <param name="device"></param>
        /// <param name="size_t"></param>
        private void devsRCallback(IntPtr opaque,
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