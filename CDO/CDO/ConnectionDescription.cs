using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{

    public class VideoStreamDescription
    {

        /**
         * 
         */ 
        public uint maxWidth;
        
        /**
         * 
         */ 
        public uint maxHeight;
        
        /**
         * 
         */ 
        public uint maxBitRate;
        
        /**
         * 
         */ 
        public uint maxFps;
        
        /**
         * 
         */ 
        public bool publish;
        
        /**
         * 
         */ 
        public bool receive;

        internal static CloudeoSdkWrapper.VideoStreamDescriptor toNative(VideoStreamDescription videoStreamDescription)
        {
            CloudeoSdkWrapper.VideoStreamDescriptor videoDescr = new CloudeoSdkWrapper.VideoStreamDescriptor();
            videoDescr.maxWidth = videoStreamDescription.maxWidth;
            videoDescr.maxHeight = videoStreamDescription.maxHeight;
            videoDescr.maxBitRate = videoStreamDescription.maxBitRate;
            videoDescr.maxFps = videoStreamDescription.maxFps;
            videoDescr.publish = videoStreamDescription.publish;
            videoDescr.receive = videoStreamDescription.receive;
            return videoDescr;
        }
    }

    public class ConnectionDescription
    {

        public ConnectionDescription()
        {
            highVideoStream = new VideoStreamDescription();
            lowVideoStream = new VideoStreamDescription();
        }

        /**
         * 
         */ 
        public VideoStreamDescription highVideoStream;
        
        /**
         * 
         */ 
        public VideoStreamDescription lowVideoStream;
        
        /**
         * 
         */ 
        public bool autopublishVideo;
        
        /**
         * 
         */ 
        public bool autopublishAudio;
        
        /**
         * 
         */ 
        public string url;
        
        /**
         * 
         */ 
        public string scopeId;
        
        /**
         * 
         */ 
        public string token;


        internal static CloudeoSdkWrapper.CDOConnectionDescriptor toNative(ConnectionDescription connectionDescription)
        {
            CloudeoSdkWrapper.CDOConnectionDescriptor connDescr = new CloudeoSdkWrapper.CDOConnectionDescriptor();
            connDescr.highVideoStream = VideoStreamDescription.toNative(connectionDescription.highVideoStream);
            connDescr.lowVideoStream = VideoStreamDescription.toNative(connectionDescription.lowVideoStream);
            connDescr.autopublishAudio = connectionDescription.autopublishAudio;
            connDescr.autopublishVideo = connectionDescription.autopublishVideo;
            connDescr.url = StringHelper.toNative(connectionDescription.url);
            
            return connDescr;
        }
    }
}
