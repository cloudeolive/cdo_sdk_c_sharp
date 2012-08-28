/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

namespace CDO
{
    public interface PlatformInitListener
    {
         void onInitProgressChanged(InitProgressChangedEvent e);

         void onInitStateChanged(InitStateChangedEvent e);
    }

    
}
