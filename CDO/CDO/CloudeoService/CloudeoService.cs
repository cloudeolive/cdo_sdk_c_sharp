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
    /// Interface describing the methods provided by the CloudeoService - 
    /// which covers most of the functionality provided by the Cloudeo Platform
    /// </summary>
    public interface CloudeoService
    {

        #region Basic logic
        /**
         * Version and listener management
         * =====================================================================
         */
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void getVersion(Responder<string> responder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="listener"></param>
        void addServiceListener(Responder<Object> responder,
                CloudeoServiceListener listener);
        
        #endregion

        #region Audio capture devices mgmnt
        /**
         * Audio capture devices related methods.
         * =====================================================================
         */ 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="respodner"></param>
        void getAudioCaptureDevice(Responder<string> respodner);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void getAudioCaptureDeviceNames(
                Responder<Dictionary<string,string>> responder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="deviceId"></param>
        void setAudioCaptureDevice(Responder<Object> responder,
                string deviceId);

        #endregion

        #region Audio output devices mgmnt

        /**
         * Audio output devices related methods.
         * =====================================================================
         */ 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="respodner"></param>
        void getAudioOutputDevice(Responder<string> respodner);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void getAudioOutputDeviceNames(
                Responder<Dictionary<string,string>> responder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="deviceId"></param>
        void setAudioOutputDevice(Responder<Object> responder, string deviceId);

        #endregion

        #region Video capture devices mgmnt

        /**
         * Video capture devices related methods.
         * =====================================================================
         */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void getVideoCaptureDevice(Responder<string> respodner);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void getVideoCaptureDeviceNames(
                Responder<Dictionary<string, string>> responder);

        /// <summary>
        /// 
        /// </summary>
        void setVideoCaptureDevice(Responder<Object> responder,
                string deviceId);

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="thumbWidth"></param>
        void getScreenCaptureSources(
                Responder<List<ScreenCaptureSource>> responder, int thumbWidth);

        #region Local preview mgmnt
        /**
         * Local preview management.
         * =====================================================================
         */ 

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void startLocalVideo(Responder<string> responder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void stopLocalVideo(Responder<Object> responder);

        #endregion
        
        #region Connectivity

        /**
         * Connectivity
         * =====================================================================
         */ 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="connDescription"></param>
        void connect(Responder<Object> responder,
                ConnectionDescription connDescription);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="scopeId"></param>
        void disconnect(Responder<Object> responder, string scopeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="scopeId"></param>
        /// <param name="mediaType"></param>
        /// <param name="options"></param>
        void publish(Responder<Object> responder, string scopeId,
                MediaType mediaType, MediaPublishOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="scopeId"></param>
        /// <param name="mediaType"></param>
        void unpublish(Responder<Object> responder,
                string scopeId, MediaType mediaType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="scopeId"></param>
        /// <param name="message"></param>
        /// <param name="targetUserId"></param>
        void sendMessage(Responder<Object> responder, string scopeId,
                string message, long targetUserId);

        #endregion

        #region Fine tune media devices contorl

        /**
         * Fine tune media devices contorl
         * =====================================================================
         */ 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void getMicrophoneVolume(Responder<int> responder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void getSpeakersVolume(Responder<int> responder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="enabled"></param>
        void monitorMicActivity(Responder<Object> responder, bool enabled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="volume"></param>
        void setMicrophoneVolume(Responder<Object> responder, int volume);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="volume"></param>
        void setSpeakersVolume(Responder<Object> responder, int volume);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void startPlayingTestSound(Responder<Object> responder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void stopPlayingTestSound(Responder<Object> responder);

        #endregion
        
        #region Stats management

        /**
         * Stats management
         * =====================================================================
         */ 

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void startMeasuringStatistics(Responder<Object> responder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        void stopMeasuringStatistics(Responder<Object> responder);

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="content"></param>
        void sendEchoNotification(Responder<Object> responder, string content);

    }
}
