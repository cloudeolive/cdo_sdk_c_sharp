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
        public int maxWidth;
        
        /**
         * 
         */ 
        public int maxHeight;
        
        /**
         * 
         */ 
        public int maxBitRate;
        
        /**
         * 
         */ 
        public int maxFps;
        
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
    }
}
