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
    public class ConnectionLostEvent :EventArgs
    {
        private string _scopeId;
        private int _errCode;
        private string _errMessage;

        public string ScopeId { get { return _scopeId; } }
        public int ErrCode { get { return _errCode; } }
        public string ErrMessage { get { return _errMessage; } }

        internal static ConnectionLostEvent FromNative(
            CDOConnectionLostEvent connLostEvnt)
        {
            ConnectionLostEvent result = new ConnectionLostEvent();
            result._scopeId = connLostEvnt.scopeId.body;
            result._errCode = connLostEvnt.errCode;
            result._errMessage = connLostEvnt.errMessage.body;
            return result;
        }
    }

    public class DeviceListChangedEvent : EventArgs

    {
        private bool _audioIn;
        private bool _videoIn;
        private bool _audioOut;

        public bool AudioIn { get { return _audioIn; } }
        public bool VideoIn { get { return _videoIn; } }
        public bool AudioOut { get { return _audioOut; } }

        internal static DeviceListChangedEvent FromNative(
            CDODeviceListChangedEvent devListChangedEvnt)
        {
            DeviceListChangedEvent result = new DeviceListChangedEvent();
            result._audioIn = devListChangedEvnt.audioIn;
            result._videoIn = devListChangedEvnt.videoIn;
            result._audioOut = devListChangedEvnt.audioOut;
            return result;
        }
    }

    public class MediaConnTypeChangedEvent : EventArgs

    {
        private string _scopeId;
        private MediaType _mediaType;
        private ConnectionType _connType;

        public string ScopeId { get { return _scopeId; } }
        public MediaType MediaType { get { return _mediaType; } }
        public ConnectionType ConnType { get { return _connType; } }

        internal static MediaConnTypeChangedEvent FromNative(
            CDOMediaConnTypeChangedEvent mediaConnTypeChangedEvnt)
        {
            MediaConnTypeChangedEvent result = new MediaConnTypeChangedEvent();
            result._scopeId = mediaConnTypeChangedEvnt.scopeId.body;
            result._mediaType =
                MediaType.FromString(mediaConnTypeChangedEvnt.mediaType.body);
            result._connType = ConnectionType.FromString(
                mediaConnTypeChangedEvnt.connectionType.body);
            return result;
        }
    }

    public class MediaStats
    {
        private CDOMediaStats _stats;

        public float avgJitter { get {return _stats.avgJitter; }}
            public float avOffset{ get {return _stats.avOffset; }}
            public float bitRate{ get {return _stats.bitRate; }}
            public float cpu{ get {return _stats.cpu; }}
            public float fps{ get {return _stats.fps; }}
            public float jbLength{ get {return _stats.jbLength; }}
            public float layer{ get {return _stats.layer; }}
            public float loss{ get {return _stats.loss; }}
            public float maxJitter{ get {return _stats.maxJitter; }}
            public float psnr{ get {return _stats.psnr; }}
            public float quality{ get {return _stats.quality; }}
            public float queueDelay{ get {return _stats.queueDelay; }}
            public float rtt{ get {return _stats.rtt; }}
            public float totalCpu{ get {return _stats.totalCpu; }}
            public float totalLoss{ get {return _stats.totalLoss; }}
            

        internal MediaStats(CDOMediaStats stats)
        {
            _stats = stats;
        }
    }

    public class MediaStatsEvent : EventArgs

    {
        private string _scopeId;
        private MediaType _mediaType;
        private long _remoteUserId;
        private MediaStats _stats;

        public string ScopeId { get { return _scopeId; } }
        public MediaType MediaType { get { return _mediaType; } }
        public long RemoteUserId { get { return _remoteUserId; } }
        public MediaStats Stats { get { return _stats; } }

        internal static MediaStatsEvent FromNative(
            CDOMediaStatsEvent mediaStatsEvnt)
        {
            MediaStatsEvent result = new MediaStatsEvent();
            result._scopeId = mediaStatsEvnt.scopeId.body;
            result._mediaType = MediaType.FromString(mediaStatsEvnt.mediaType.body);
            result._remoteUserId = mediaStatsEvnt.remoteUserId;
            result._stats = new MediaStats(mediaStatsEvnt.stats);
            return result;
        }

    }

    public class MessageEvent : EventArgs

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

    public class MicActivityEvent : EventArgs

    {
        private int _activity;

        public int Activity { get { return _activity; } }

        internal static MicActivityEvent FromNative(
            CDOMicActivityEvent micActivityEvnt)
        {
            MicActivityEvent result = new MicActivityEvent();
            result._activity = micActivityEvnt.activity;
            return result;
        }
    }

    public class MicGainEvent : EventArgs

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

    public class VideoFrameSizeChangedEvent : EventArgs

    {
        private string _sinkId;
        private int _width;
        private int _height;

        public string SinkId { get { return _sinkId; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        internal static VideoFrameSizeChangedEvent FromNative(
            CDOVideoFrameSizeChangedEvent vidFrameSizeChangedEvent)
        {
            VideoFrameSizeChangedEvent result =
                new VideoFrameSizeChangedEvent();
            result._sinkId = vidFrameSizeChangedEvent.sinkId.body;
            result._width = vidFrameSizeChangedEvent.width;
            result._height = vidFrameSizeChangedEvent.height; 
            return result;
        }
    }

    public class UserStateChangedEvent : EventArgs

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

        internal static UserStateChangedEvent FromNative(
            CDOUserStateChangedEvent userStateChangedEvnt)
        {
            UserStateChangedEvent result = new UserStateChangedEvent();
            result._scopeId = userStateChangedEvnt.scopeId.body;
            result._mediaType =
                MediaType.FromString(userStateChangedEvnt.mediaType.body);
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

    public class EchoEvent : EventArgs

    {
        private string _echoValue;

        internal static EchoEvent FromNative(CDOEchoEvent nEvent)
        {
            EchoEvent e = new EchoEvent();
            e._echoValue = StringHelper.fromNative(nEvent.echoValue);
            return e;
        }

        public string echoValue { get { return _echoValue;  } }


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
        void onEchoEvent(EchoEvent e);
    }


    public class CloudeoServiceListenerAdapter:CloudeoServiceListener
    {
        public virtual void onConnectionLost(ConnectionLostEvent e) { }
        public virtual void onDeviceListChanged(DeviceListChangedEvent e) { }
        public virtual void onMediaConnTypeChanged(MediaConnTypeChangedEvent e)
        { }
        public virtual void onMediaStats(MediaStatsEvent e) { }
        public virtual void onMediaStreamEvent(UserStateChangedEvent e) { }
        public virtual void onMessage(MessageEvent e) { }
        public virtual void onMicActivity(MicActivityEvent e) { }
        public virtual void onMicGain(MicGainEvent e) { }
        public virtual void onUserEvent(UserStateChangedEvent e) { }
        public virtual void onVideoFrameSizeChanged(
            VideoFrameSizeChangedEvent e)
        { }
        public virtual void onEchoEvent(EchoEvent e) { }
    }

    public class CloudeoServiceEventDispatcher : CloudeoServiceListener
    {

        #region Connection Lost handling
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ConnectionLostHandler(object sender, ConnectionLostEvent e);

        /// <summary>
        /// 
        /// </summary>
        public event ConnectionLostHandler ConnectionLost;

        public virtual void onConnectionLost(ConnectionLostEvent e) 
        {
            if (ConnectionLost != null)
                ConnectionLost(this, e);
        }

        #endregion
        #region DeviceListChanged handling
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DeviceListChangedHandler(object sender, DeviceListChangedEvent e);

        /// <summary>
        /// 
        /// </summary>
        public event DeviceListChangedHandler DeviceListChanged;

        public virtual void onDeviceListChanged(DeviceListChangedEvent e)
        {
            if (DeviceListChanged != null)
                DeviceListChanged(this, e);
        }


        #endregion
        #region MediaConnTypeChanged handling
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MediaConnTypeChangedHandler(object sender, MediaConnTypeChangedEvent e);

        /// <summary>
        /// 
        /// </summary>
        public event MediaConnTypeChangedHandler MediaConnTypeChanged;

        
        public virtual void onMediaConnTypeChanged(MediaConnTypeChangedEvent e)
        {
            if (MediaConnTypeChanged != null)
                MediaConnTypeChanged(this, e);
        }
        
        #endregion

        #region MediaStats handling
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MediaStatsHandler(object sender, MediaStatsEvent e);

        /// <summary>
        /// 
        /// </summary>
        public event MediaStatsHandler MediaStats;

        public virtual void onMediaStats(MediaStatsEvent e)
        {
            if (MediaStats != null)
                MediaStats(this, e);
        }
        #endregion
        #region MediaStream handling
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void UserStateChangedEventHandler(object sender, UserStateChangedEvent e);

        /// <summary>
        /// 
        /// </summary>
        public event UserStateChangedEventHandler MediaStream;


        public virtual void onMediaStreamEvent(UserStateChangedEvent e) 
        {
            if (MediaStream != null)
                MediaStream(this, e);
        }
        #endregion

        #region UserEvent handling

        /// <summary>
        /// 
        /// </summary>
        public event UserStateChangedEventHandler UserEvent;

        public virtual void onUserEvent(UserStateChangedEvent e)
        {
            if (UserEvent != null)
                UserEvent(this, e);
        }
        #endregion

        public delegate void MessageEventHandler(object sender, MessageEvent e);

        public event MessageEventHandler Message;
        public virtual void onMessage(MessageEvent e) 
        {
            if (Message != null)
                Message(this, e);
        }

        public delegate void MicActivityEventHandler(object sender, MicActivityEvent e);

        public event MicActivityEventHandler MicActivity;
        public virtual void onMicActivity(MicActivityEvent e) 
        {
            if (MicActivity != null)
                MicActivity(this, e);
        }

        public delegate void MicGainEventHandler(object sender, MicGainEvent e);
        public event MicGainEventHandler MicGain;
        public virtual void onMicGain(MicGainEvent e) 
        {
            if (MicGain != null)
                MicGain(this, e);
        }


        public delegate void VideoFrameSizeChangedEventHandler(object sender, VideoFrameSizeChangedEvent e);
        public event VideoFrameSizeChangedEventHandler VideoFrameSizeChanged;
        public virtual void onVideoFrameSizeChanged(
            VideoFrameSizeChangedEvent e)
        { 
            if(VideoFrameSizeChanged != null)
                VideoFrameSizeChanged(this, e);
        }
        public virtual void onEchoEvent(EchoEvent e) { }
    }

}
