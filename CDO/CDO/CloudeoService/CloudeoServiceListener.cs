using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    public class ConnectionLostEvent 
    {
        private string _scopeId;
        private int _errCode;
        private string _errMessage;

        public string ScopeId { get { return _scopeId; } }
        public int ErrCode { get { return _errCode; } }
        public string ErrMessage { get { return _errMessage; } }

        internal static ConnectionLostEvent FromNative(CDOConnectionLostEvent connLostEvnt)
        {
            ConnectionLostEvent result = new ConnectionLostEvent();
            result._scopeId = connLostEvnt.scopeId.body;
            result._errCode = connLostEvnt.errCode;
            result._errMessage = connLostEvnt.errMessage.body;
            return result;
        }
    }

    public class DeviceListChangedEvent
    {
        private bool _audioIn;
        private bool _videoIn;
        private bool _audioOut;

        public bool AudioIn { get { return _audioIn; } }
        public bool VideoIn { get { return _videoIn; } }
        public bool AudioOut { get { return _audioOut; } }

        internal static DeviceListChangedEvent FromNative(CDODeviceListChangedEvent devListChangedEvnt)
        {
            DeviceListChangedEvent result = new DeviceListChangedEvent();
            result._audioIn = devListChangedEvnt.audioIn;
            result._videoIn = devListChangedEvnt.videoIn;
            result._audioOut = devListChangedEvnt.audioOut;
            return result;
        }
    }

    public class MediaConnTypeChangedEvent
    {
        private string _scopeId;
        private MediaType _mediaType;
        private ConnectionType _connType;

        public string ScopeId { get { return _scopeId; } }
        public MediaType MediaType { get { return _mediaType; } }
        public ConnectionType ConnTypw { get { return _connType; } }

        internal static MediaConnTypeChangedEvent FromNative(CDOMediaConnTypeChangedEvent mediaConnTypeChangedEvnt)
        {
            MediaConnTypeChangedEvent result = new MediaConnTypeChangedEvent();
            result._scopeId = mediaConnTypeChangedEvnt.scopeId.body;
            result._mediaType = MediaType.FromString(mediaConnTypeChangedEvnt.mediaType.body);
            result._connType = ConnectionType.FromString(mediaConnTypeChangedEvnt.connectionType.body);
            return result;
        }
    }

    public class MediaStatsEvent
    {
        private string _scopeId;
        private MediaType _mediaType;
        private long _remoteUserId;
        private Dictionary<string, float> _stats;

        public string ScopeId { get { return _scopeId; } }
        public MediaType MediaType { get { return _mediaType; } }
        public long RemoteUserId { get { return _remoteUserId; } }
        public Dictionary<string, float> Stats { get { return _stats; } }

        internal static MediaStatsEvent FromNative(CDOMediaStatsEvent mediaStatsEvnt)
        {
            MediaStatsEvent result = new MediaStatsEvent();
            result._scopeId = mediaStatsEvnt.scopeId.body;
            result._mediaType = MediaType.FromString(mediaStatsEvnt.mediaType.body);
            result._remoteUserId = mediaStatsEvnt.remoteUserId;
            result._stats = getStats(mediaStatsEvnt.stats);
            return result;
        }

        internal static Dictionary<string, float> getStats(CDOMediaStats stats)
        {
            Dictionary<string, float> result = new Dictionary<string,float>();
            result.Add("avgJitter", stats.avgJitter);
            result.Add("avOffset", stats.avOffset);
            result.Add("bitRate", stats.bitRate);
            result.Add("cpu", stats.cpu);
            result.Add("fps", stats.fps);
            result.Add("jbLength", stats.jbLength);
            result.Add("layer", stats.layer);
            result.Add("loss", stats.loss);
            result.Add("maxJitter", stats.maxJitter);
            result.Add("psnr", stats.psnr);
            result.Add("quality", stats.quality);
            result.Add("queueDelay", stats.queueDelay);
            result.Add("rtt", stats.rtt);
            result.Add("totalCpu", stats.totalCpu);
            result.Add("totalLoss", stats.totalLoss);
            return result;
        }
    }

    public class MessageEvent
    {
        private long _srcUserId;
        private string _data;

        public long SrcUserId { get { return _srcUserId; } }
        public string Data { get { return _data; } }

        internal static MessageEvent FromNative(CDOMessageEvent messageEvnt)
        {
            MessageEvent result = new MessageEvent();
            result._srcUserId = messageEvnt.srcUserId;
            result._data = messageEvnt.data.body;
            return result;
        }
    }

    public class MicActivityEvent
    {
        private int _activity;

        public int Activity { get { return _activity; } }

        internal static MicActivityEvent FromNative(CDOMicActivityEvent micActivityEvnt)
        {
            MicActivityEvent result = new MicActivityEvent();
            result._activity = micActivityEvnt.activity;
            return result;
        }
    }

    public class MicGainEvent
    {
        private int _gain;

        public int Gain { get { return _gain; } }

        internal static MicGainEvent FromNative(CDOMicGainEvent micGainEvnt)
        {
            MicGainEvent result = new MicGainEvent();
            result._gain = micGainEvnt.gain;
            return result;
        }
    }

    public class VideoFrameSizeChangedEvent
    {
        private string _sinkId;
        private int _width;
        private int _height;

        public string SinkId { get { return _sinkId; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        internal static VideoFrameSizeChangedEvent FromNative(CDOVideoFrameSizeChangedEvent vidFrameSizeChangedEvent)
        {
            VideoFrameSizeChangedEvent result = new VideoFrameSizeChangedEvent();
            result._sinkId = vidFrameSizeChangedEvent.sinkId.body;
            result._width = vidFrameSizeChangedEvent.width;
            result._height = vidFrameSizeChangedEvent.height; 
            return result;
        }
    }

    public class UserStateChangedEvent
    {
        private string _scopeId;
        private MediaType _mediaType;

        private bool _isConnected;
        private long _userId;

        private bool _audioPublished;

        private bool _screenPublished;
        private string _screenSinkId;

        private bool _videoPublished;
        private string _videoSinkId;


        public string ScopeId { get { return _scopeId; } }
        public MediaType MediaType { get { return _mediaType; } }

        public bool IsConnected { get { return _isConnected; } }
        public long UserId { get { return _userId; } }

        public bool AudioPublished { get { return _audioPublished; } }

        public bool ScreenPublished { get { return _screenPublished; } }
        public string ScreenSinkId { get { return _screenSinkId; } }

        public bool VideoPublished { get { return _videoPublished; } }
        public string VideoSinkId { get { return _videoSinkId; } }

        internal static UserStateChangedEvent FromNative(CDOUserStateChangedEvent userStateChangedEvnt)
        {
            UserStateChangedEvent result = new UserStateChangedEvent();
            result._scopeId = userStateChangedEvnt.scopeId.body;
            result._mediaType = MediaType.FromString(userStateChangedEvnt.mediaType.body);
            result._isConnected = userStateChangedEvnt.isConnected;
            result._userId = userStateChangedEvnt.userId;
            result._audioPublished = userStateChangedEvnt.audioPublished;
            result._screenPublished = userStateChangedEvnt.screenPublished;
            result._screenSinkId = userStateChangedEvnt.screenSinkId.body;
            result._videoPublished = userStateChangedEvnt.videoPublished;
            result._videoSinkId = userStateChangedEvnt.videoSinkId.body;
            return result;
        }
    }


    public interface CloudeoServiceListener
    {
        void onConnectionLost(ConnectionLostEvent e);
        void onDeviceListChanged(DeviceListChangedEvent e);
        void onMediaConnTypeChanged(MediaConnTypeChangedEvent e);
        void onMediaStats(MediaStatsEvent e);
        void onMediaStreamEvent(UserStateChangedEvent e);
        void onMessage(MessageEvent e);
        void onMicActivity(MicActivityEvent e);
        void onMicGain(MicGainEvent e);
        void onUserEvent(UserStateChangedEvent e);
        void onVideoFrameSizeChanged(VideoFrameSizeChangedEvent e);
    }
}
