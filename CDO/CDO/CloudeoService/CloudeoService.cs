/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */
using System;
using System.Collections.Generic;

namespace CDO
{

    /// <summary>
    /// Provides the core functionality of the Cloudeo SDK, including devices
    /// management, connectivity and SDK management.
    /// </summary>
    public interface CloudeoService
    {

        #region Basic logic
        /*
         * Version and listener management
         * =====================================================================
         */
        
        /// <summary>
        /// Retrieves version of the SDK. Example result: "1.15.0.2".
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void getVersion(Responder<string> responder);

        /// <summary>
        /// Sets an id of web application using the SDK. Required when making
        /// authorized connection request.Sets an id of web application using the SDK. Required when making
        /// authorized connection request.
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="applicationId"></param>
        void setApplicationId(Responder<object> responder, long applicationId);
        
        /// <summary>
        /// Registers a Cloudeo Service listener. Listener provided here should implement the
        /// CDO.CloudeoServiceListener interface.
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// @see CDO.CloudeoServiceListener
        /// @see CDO.CloudeoServiceListenerAdapter
        /// @see CDO.CloudeoServiceEventDispatcher
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="listener">listener implementation to be set</param>
        void addServiceListener(Responder<Object> responder,
                CloudeoServiceListener listener);
        
        #endregion

        #region Audio capture devices mgmnt
        /*
         * Audio capture devices related methods.
         * =====================================================================
         */ 

        /// <summary>
        /// Returns the index of currently configured audio capture device (microphone).
        /// The value returned by this method is a Number, corresponding to index from
        /// an array obtained from call to the
        /// CDO.CloudeoService.getAudioCaptureDeviceNames method.
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// @see setAudioCaptureDevice
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void getAudioCaptureDevice(Responder<string> respodner);
        
        /// <summary>
        /// Returns a list of audio capture devices (microphones) plugged in to the
        /// user's computer at the moment of call. Keys of the
        /// resulting dictionary are to be used when configuring audio capture device.
        /// 
        /// Please note, that it is not guaranteed that indexes of devices 
        /// are permanent across multiple
        /// 
        /// #### Possible errors:
        ///
        /// - __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// - __MEDIA_INVALID_AUDIO_DEV (4005)__
        ///   In case of an error with getting the amount of the devices.
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void getAudioCaptureDeviceNames(
                Responder<Dictionary<string,string>> responder);

        /// <summary>
        /// Sets the audio capture device (microphone) to be used by the Cloudeo Service.
        /// The device is configured using index of the array obtained from call to the
        /// CDO.CloudeoService.getAudioCaptureDeviceNames method.
        /// 
        /// #### Possible errors:
        ///
        /// - __DEFAULT_ERROR (-1)__ 
        ///   If an unexpected, internal error occurs
        /// - __MEDIA_INVALID_AUDIO_IN_DEV (4003)__
        ///   In case of invalid device id specified specified or if there were problems 
        ///   with enabling the device (e.g. device in use on Windows XP)
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="deviceId"></param>
        void setAudioCaptureDevice(Responder<Object> responder,
                string deviceId);

        #endregion

        #region Audio output devices mgmnt

        /*
         * Audio output devices related methods.
         * =====================================================================
         */ 

        /// <summary>
        /// Returns the index of currently configured audio output device.
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="respodner"></param>
        void getAudioOutputDevice(Responder<string> respodner);
        
        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder"></param>
        void getAudioOutputDeviceNames(
                Responder<Dictionary<string,string>> responder);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder"></param>
        /// <param name="deviceId"></param>
        void setAudioOutputDevice(Responder<Object> responder, string deviceId);

        #endregion

        #region Video capture devices mgmnt

        /*
         * Video capture devices related methods.
         * =====================================================================
         */

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder"></param>
        void getVideoCaptureDevice(Responder<string> respodner);
        
        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder"></param>
        void getVideoCaptureDeviceNames(
                Responder<Dictionary<string, string>> responder);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder"></param>
        /// <param name="deviceId"></param>
        void setVideoCaptureDevice(Responder<Object> responder,
                string deviceId);

        #endregion

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="thumbWidth"></param>
        void getScreenCaptureSources(
                Responder<List<ScreenCaptureSource>> responder, int thumbWidth);

        #region Local preview mgmnt
        /*
         * Local preview management.
         * =====================================================================
         */ 

        
        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void startLocalVideo(Responder<string> responder);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void stopLocalVideo(Responder<Object> responder);

        #endregion
        
        #region Connectivity

        /**
         * Connectivity
         * =====================================================================
         */ 


        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="connDescription"></param>
        void connect(Responder<Object> responder,
                ConnectionDescription connDescription);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="scopeId"></param>
        void disconnect(Responder<Object> responder, string scopeId);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// @since 1.17.0.0
        /// <param name="scopeId"></param>
        /// <param name="mediaType"></param>
        /// <param name="options"></param>
        void publish(Responder<Object> responder, string scopeId,
                MediaType mediaType, MediaPublishOptions options);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="scopeId"></param>
        /// <param name="mediaType"></param>
        void unpublish(Responder<Object> responder,
                string scopeId, MediaType mediaType);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="scopeId"></param>
        /// <param name="message"></param>
        /// <param name="targetUserId"></param>
        void sendMessage(Responder<Object> responder, string scopeId,
                string message, long targetUserId);

        #endregion

        #region Fine tune media devices contorl

        /*
         * Fine tune media devices contorl
         * =====================================================================
         */ 
        
        

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void getSpeakersVolume(Responder<int> responder);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="enabled"></param>
        void monitorMicActivity(Responder<Object> responder, bool enabled);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="volume"></param>
        void setSpeakersVolume(Responder<Object> responder, int volume);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void startPlayingTestSound(Responder<Object> responder);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void stopPlayingTestSound(Responder<Object> responder);

        #endregion
        
        #region Stats management

        /**
         * Stats management
         * =====================================================================
         */ 

        
        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void startMeasuringStatistics(Responder<Object> responder, string scopeId, int interval);

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        void stopMeasuringStatistics(Responder<Object> responder, string scopeId);

        #endregion

        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="options"></param>
        void renderSink(Responder<RenderingWidget> responder, RenderOptions options);
        
        /// <summary>
        /// 
        /// 
        /// #### Possible errors:
        ///
        /// __DEFAULT_ERROR (-1)__ If an unexpected, internal error occurs
        /// </summary>
        /// @since 1.17.0.0
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="options"></param>
        void manualRenderSink(Responder<ManualRenderer> responder, RenderOptions options);
        
        /// @private
        /// <param name="responder">
        /// Object that will receive the result of call.
        /// </param>
        /// <param name="content"></param>
        void sendEchoNotification(Responder<Object> responder, string content);

    }
}
