﻿/**
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org/>
*/

/**
 * 
 */
using System;
using System.Reflection;
using System.IO;
namespace CDO
{
    public delegate void ResultHandler<T>(T result);
    public delegate void ErrHandler(int errCode, string errMessage);

    

    public class Platform
    {
        private static string DEFAULT_SDK_PATH = "cloudeo_sdk";

        #region Members
        
        private static Platform _instance;

        private static IntPtr _platformHandle;

        private static PlatformInitListener _listener;
        private static cdo_platform_init_done_clbck _init_done_callback;
        private static cdo_platform_init_progress_clbck _init_progress_callback;

        private static cdo_int_rclbck_t _int_result_callback;

        #endregion


        #region Constructors
        
        /**
         * Singleton!
         */
        static Platform()
        {
            _platformHandle = IntPtr.Zero;
            _init_done_callback = new cdo_platform_init_done_clbck(cdo_platform_init_done_callback);
            _init_progress_callback = new cdo_platform_init_progress_clbck(cdo_platform_init_progress_callback);
            _int_result_callback = new cdo_int_rclbck_t(cdo_int_result_callback);
        }

        ~Platform() { release(); }

        #endregion


        #region Singleton

        public static Platform instance()
        {
            if (_instance == null)
                _instance = new Platform();
            return _instance;
        }

        #endregion  


        static private string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        #region Methods

        public static void init(PlatformInitListener listener)
        {
            init(listener, null);
        }

        public static void init(PlatformInitListener listener, PlatformInitOptions options)
        {
            _listener = listener;

            //Perform platform initialization
            string path;
            if (options != null)
            {
                if (Path.IsPathRooted(options.sdkPath))
                {
                    path = options.sdkPath;
                }
                else
                {
                    path = AssemblyDirectory + options.sdkPath;                       
                }
            }
            else
            {
                path = AssemblyDirectory + "\\" + DEFAULT_SDK_PATH;
            }
            CDOString str = new CDOString();
            str.body = path;
            str.length = (UInt32)path.Length;

            CDOInitOptions initOptions = new CDOInitOptions();
            initOptions.logicLibPath = str;

            NativeAPI.cdo_init_platform(_init_done_callback, ref initOptions, IntPtr.Zero);
        }

        private static void cdo_platform_init_done_callback(IntPtr ptr, ref CDOError err, IntPtr h)
        {
            _platformHandle = h;

            if (_listener != null)
            {
                InitStateChangedEvent.InitState state =
                    (err.err_code == 0) ? InitStateChangedEvent.InitState.INITIALIZED : 
                                          InitStateChangedEvent.InitState.ERROR;
                InitStateChangedEvent e = new InitStateChangedEvent(state, err.err_code, err.err_message.body);
                _listener.onInitStateChanged(e);
            }
        }

        private static void cdo_platform_init_progress_callback(IntPtr ptr, short sh)
        {
            if (_listener != null)
            {
                InitProgressChangedEvent e = new InitProgressChangedEvent(sh);
                _listener.onInitProgressChanged(e);
            }
        }



        public static void release() 
        { 
            /* dispose the platform */
            NativeAPI.cdo_release_platform(_platformHandle);
            _platformHandle = IntPtr.Zero;
        }


        public static CloudeoService getService()
        {
            if (_platformHandle == IntPtr.Zero)
                return null;
            else 
                return new CloudeoServiceImpl(_platformHandle);
        }

        public static void renderSink(RenderOptions options) 
        { 
            /*TODO: implement method*/ 
        }

        private static void cdo_int_result_callback(IntPtr opaque, ref CDOError error, int i)
        {
            // TODO: renderSink responder ?
        }

        // *****************************************************************
        // ********************* ResponderAdapter **************************

        public static Responder<T> createResponder<T>(ResultHandler<T> rh=null, ErrHandler errH=null)
        {
            return new ResponderAdapter<T>(rh, errH);
        }

        #endregion

        public static object initOptions { get; set; }
    }
}
