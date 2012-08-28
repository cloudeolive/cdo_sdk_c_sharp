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
    /// <summary>
    /// 
    /// </summary>
    public interface PlatformInitListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
         void onInitProgressChanged(InitProgressChangedEvent e);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="e"></param>
         void onInitStateChanged(InitStateChangedEvent e);
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlatformInitListenerDispatcher : PlatformInitListener
    {

        #region Progress changed handling
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ProgressChangedEventHandler(object sender, InitProgressChangedEvent e);
        
        /// <summary>
        /// 
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        public void onInitProgressChanged(InitProgressChangedEvent e)
        {
            if (ProgressChanged != null)
                ProgressChanged(this, e);
        }
        #endregion

        #region StateChanged handling

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void StateChangedEventHandler(object sender, InitStateChangedEvent e);
        
        /// <summary>
        /// 
        /// </summary>
        public event StateChangedEventHandler StateChanged;
        

        public void onInitStateChanged(InitStateChangedEvent e)
        {
            if (StateChanged != null)
                StateChanged(this, e);
        }

        #endregion
    }
}
