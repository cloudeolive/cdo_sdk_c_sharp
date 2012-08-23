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

        private string bool2JsonString(bool value)
        {
            return value ? "true" : "false";
        }

        internal string toJSON()
        {
            string json = "";
            json = "{" +
                    "\"lowVideoStream\":" +
                    "{" +
                    "\"publish\":" + bool2JsonString(lowVideoStream.publish) +
                    ",\"receive\":" + bool2JsonString(lowVideoStream.receive) +
                    ",\"maxWidth\":" + lowVideoStream.maxWidth +
                    ",\"maxHeight\":" + lowVideoStream.maxHeight +
                    ",\"maxBitRate\":" + lowVideoStream.maxBitRate +
                    ",\"maxFps\":" + lowVideoStream.maxFps + "}" +
                    ",\"highVideoStream\":" +
                    "{" +
                    "\"publish\":" + bool2JsonString(highVideoStream.publish) +
                    ",\"receive\":" + bool2JsonString(highVideoStream.receive) +
                    ",\"maxWidth\":" + highVideoStream.maxWidth +
                    ",\"maxHeight\":" + highVideoStream.maxHeight +
                    ",\"maxBitRate\":" + highVideoStream.maxBitRate +
                    ",\"maxFps\":" + highVideoStream.maxFps + "}" +
                    ",\"autopublishVideo\":" + bool2JsonString(autopublishVideo) +
                    ",\"autopublishAudio\":" + bool2JsonString(autopublishAudio) +
                    ",\"url\":\"" + url + "\",\"token\":\"" + token + "\"}";
            return json;
        }

    }
}
