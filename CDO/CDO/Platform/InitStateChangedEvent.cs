/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

namespace CDO
{
    public class InitStateChangedEvent
    {
        public enum InitState { INITIALIZED, ERROR }

        private InitState _state;
        private int _errCode;
        private string _errMessage;

        public InitState state
        {
            get { return this._state; }
        }

        public int errCode
        {
            get { return this._errCode; }
        }

        public string errMessage
        {
            get { return this._errMessage; }
        }

        internal InitStateChangedEvent(InitState state, int errCode,
            string errMessage)
        {
            this._state = state;
            this._errCode = errCode;
            this._errMessage = errMessage;
        }
    }
}