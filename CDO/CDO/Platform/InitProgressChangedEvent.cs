/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

using System;

namespace CDO
{
    public class InitProgressChangedEvent: EventArgs

    {
        private int _progress;

        public int progress
        {
            get { return this._progress; }
        }


        public InitProgressChangedEvent(int progress)
        {
            this._progress = progress;
        }
    }
}
