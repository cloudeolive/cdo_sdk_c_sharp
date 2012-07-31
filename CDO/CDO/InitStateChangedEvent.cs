using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    class InitStateChangedEvent
    {

        private string _state;
        private int _errCode;
        private string _errMessage;

        public InitStateChangedEvent(string state, int errCode, string errMessage)
        {
            this._state = state;
            this._errCode = errCode;
            this._errMessage = errMessage;
        }

        public string state
        {
            get { return this._state; }
        }

        public int errCode
        {
            get { return this._errCode}
        }

        public string errMessage
        {
            get { return this._errMessage; }
        }

    }
}
