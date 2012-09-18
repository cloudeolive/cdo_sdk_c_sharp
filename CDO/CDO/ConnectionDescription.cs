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


        internal string toJSON()
        {
            string json = "";
            json = "{" +
                    "\"publish\":" + bool2JsonString(publish) +
                    ",\"receive\":" + bool2JsonString(receive) +
                    ",\"maxWidth\":" + maxWidth +
                    ",\"maxHeight\":" + maxHeight +
                    ",\"maxBitRate\":" + maxBitRate +
                    ",\"maxFps\":" + maxFps + "}";
            return json;
        }

        private string bool2JsonString(bool value)
        {
            return value ? "true" : "false";
        }

    }

    public class AuthDetails
    {
        public long expires;

        public long userId;

        public string salt;

        public string signature;

        internal string toJSON()
        {
            string json = "";
            json = "{" +
                    "\"expires\":" + expires +
                    ",\"userId\":" + userId +
                    ",\"salt\":\"" + salt + "\"" +
                    ",\"signature\":\"" + signature + "\"}";
            return json;
        }
    }

    public class ConnectionDescription
    {

        public ConnectionDescription()
        {
            highVideoStream = new VideoStreamDescription();
            lowVideoStream = new VideoStreamDescription();
            authDetails = new AuthDetails();
        }

        /**
         * 
         */ 
        public VideoStreamDescription highVideoStream;
        
        /**
         * 
         */ 
        public VideoStreamDescription lowVideoStream;

        public AuthDetails authDetails;

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
                    "\"lowVideoStream\":" + lowVideoStream.toJSON() +
                    ",\"highVideoStream\":" + highVideoStream.toJSON() +
                    ",\"autopublishVideo\":" + bool2JsonString(autopublishVideo) +
                    ",\"autopublishAudio\":" + bool2JsonString(autopublishAudio);
            if (authDetails != null && authDetails.signature != null && authDetails.signature.Length > 0)
            {
                json += ",\"authDetails\":" + authDetails.toJSON();
            }
            json += ",\"url\":\"" + url + "\",\"token\":\"" + token + "\"}";
            return json;
        }

    }
}
