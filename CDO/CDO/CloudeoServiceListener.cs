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
        private string errMessage;

        // implement getters here

    }

    public class DeviceListChangedEvent
    {
        private bool _audioIn;
        private bool _videoIn;
        private bool _audioOut;

        // implement getters here
    }

    public class MediaConnTypeChangedEvent
    {
        private string _scopeId;
        private MediaType _mediaType;
        private ConnectionType _connType;
        // implement getters here
    }

    public class MediaStatsEvent
    {
        private string _scopeId;
        private MediaType _mediaType;
        private long _remoteUserId;
        private Dictionary<string, float> _stats;
        // implement getters here
    }

    public class MessageEvent
    {
        private long _srcUserId;
        private string data;
        // implement getters here
    }

    public class MicActivityEvent
    {
        private int _activity;
        // implement getters here
    }


    public class MicGainEvent
    {
        private int _gain;
        // implement getters here
    }

    public class VideoFrameSizeChangedEvent
    {
        private string _sinkId;
        private int _width;
        private int _height;
        // implement getters here
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
        // implement getters here
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
