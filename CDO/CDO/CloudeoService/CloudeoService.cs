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

    public interface CloudeoService
    {
        void getVersion(Responder<string> responder);

        void addServiceListener(Responder<Object> responder,
                CloudeoServiceListener listener);

        void sendEchoNotification(Responder<Object> responder, string content);


        void getAudioCaptureDevice(Responder<string> respodner);

        void getAudioCaptureDeviceNames(
                Responder<Dictionary<string,string>> responder);

        void setAudioCaptureDevice(Responder<Object> responder,
                string deviceId);
        

        void getAudioOutputDevice(Responder<string> respodner);

        void getAudioOutputDeviceNames(
                Responder<Dictionary<string,string>> responder);

        void setAudioOutputDevice(Responder<Object> responder, string deviceId);


        void getVideoCaptureDevice(Responder<string> respodner);

        void getVideoCaptureDeviceNames(
                Responder<Dictionary<string, string>> responder);

        void setVideoCaptureDevice(Responder<Object> responder,
                string deviceId);


        void getScreenCaptureSources(
                Responder<List<ScreenCaptureSource>> responder, int thumbWidth);


        void startLocalVideo(Responder<string> responder);

        void stopLocalVideo(Responder<Object> responder);


        void connect(Responder<Object> responder,
                ConnectionDescription connDescription);

        void disconnect(Responder<Object> responder, string scopeId);

        void publish(Responder<Object> responder, string scopeId,
                MediaType mediaType, MediaPublishOptions options);

        void unpublish(Responder<Object> responder,
                string scopeId, MediaType mediaType);

        void sendMessage(Responder<Object> responder, string scopeId,
                string message, long targetUserId);


        void getMicrophoneVolume(Responder<int> responder);

        void getSpeakersVolume(Responder<int> responder);

        void monitorMicActivity(Responder<Object> responder, bool enabled);

        void setMicrophoneVolume(Responder<Object> responder, int volume);

        void setSpeakersVolume(Responder<Object> responder, int volume);
        

        void startMeasuringStatistics(Responder<Object> responder);

        void stopMeasuringStatistics(Responder<Object> responder);
        
        void startPlayingTestSound(Responder<Object> responder);

        void stopPlayingTestSound(Responder<Object> responder);
    }
}
