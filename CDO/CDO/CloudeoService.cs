using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{

    public interface CloudeoService
    {
        public void getVersion(Responder<string> responder);

        public void addServiceListener(Responder<Object> responder, CloudeoServiceListener listener);


        public void getAudioCaptureDevice(Responder<string> respodner);
        public void getAudioCaptureDeviceNames(Responder<Dictionary<string,string>> responder);
        public void setAudioCaptureDevice(Responder<Object> responder, string deviceId);
        
        public void getAudioOutputDevice(Responder<string> respodner);
        public void getAudioOutputDeviceNames(Responder<Dictionary<string,string>> responder);
        public void setAudioOutputDevice(Responder<Object> responder, string deviceId);

        public void getVideoCaptureDevice(Responder<string> respodner);
        public void getVideoCaptureDeviceNames(Responder<Dictionary<string, string>> responder);
        public void setVideoCaptureDevice(Responder<Object> responder, string deviceId);

        public void getScreenCaptureSources(Responder<List<ScreenCaptureSource>> responder, int thumbWidth);

        // public void getHostCpuDetails();

        public void startLocalVideo(Responder<string> responder);
        public void stopLocalVideo(Responder<Object> responder);


        public void connect(Responder<Object> responder, ConnectionDescription connDescription);
        public void disconnect(Responder<Object> responder, string scopeId);
        public void publish(Responder<Object> responder, string scopeId, MediaType mediaType, MediaPublishOptions options);
        public void unpublish(Responder<Object> responder, string scopeId, MediaType mediaType);

        public void sendMessage(Responder<Object> responder, string scopeId, string message, long targetUserId = null);

        public void getMicrophoneVolume(Responder<int> responder);
        public void getSpeakersVolume(Responder<int> responder);
        public void monitorMicActivity(Responder<Object> responder, bool enabled);
        public void setMicrophoneVolume(Responder<Object> responder, int volume);
        public void setSpeakersVolume(Responder<Object> responder, int volume);
        
        public void startMeasuringStatistics(Responder<Object> responder);
        public void stopMeasuringStatistics(Responder<Object> responder);
        
        public void startPlayingTestSound(Responder<Object> responder);
        public void stopPlayingTestSound(Responder<Object> responder);

    }
}
