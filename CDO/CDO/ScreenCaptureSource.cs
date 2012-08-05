using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    public class ScreenCaptureSource
    {

        public ScreenCaptureSource(string id, string title)
        {
            this._id = id;
            this._title = title;
        }

        private string _id;
        private string _title;
        //private SomethingThatWillContainThePicture _image;


        public string id { get { return this._id; } }

        public string title { get { return this._title; } }



    }
}
