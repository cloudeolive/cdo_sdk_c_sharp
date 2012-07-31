using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{

    public enum VideoScalingFilter
    {
        FAST_BILINEAR,
        BICUBIC
    }

    public class RenderOptions
    {

        /**
         * 
         */
        private string _sinkId;

        /**
         * 
         */
        private bool _mirror;

        /**
         * 
         */
        private VideoScalingFilter _filter;

        /**
         * UI element that will be used for rendering
         */
        // private SomeUIComponent _container;


        public string sinkId
        {
            get { return this._sinkId; }
            set { this._sinkId = value; }
        }


        public bool mirror
        {
            get { return this._mirror; }
            set { this._mirror = value; }
        }

        public VideoScalingFilter filter
        {
            get { return this.filter; }
            set { this._filter = value; }
        }
    }
}
