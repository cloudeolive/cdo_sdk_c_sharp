using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{

    public enum InitState
    {
        INITIALIZED,ERROR
    }

    public class InitStateChangedEvent
    {

        private InitState _state;
        private int _errCode;
        private string _errMessage;

        public InitStateChangedEvent(InitState state, int errCode, string errMessage)
        {
            this._state = state;
            this._errCode = errCode;
            this._errMessage = errMessage;
        }

        public InitState state
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
