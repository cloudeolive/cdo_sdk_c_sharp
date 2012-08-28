/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

using System;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
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
        
        private static RenderSupport _renderSupport;

        #endregion


        #region Constructors
        
        /**
         * Singleton!
         */
        static Platform()
        {
            _platformHandle = IntPtr.Zero;
            
        }

        ~Platform() 
        { 
            release(); 
        }

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

        public static void init(PlatformInitListener listener,
            PlatformInitOptions options)
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

            NativeAPI.cdo_init_platform(cdo_platform_init_done_callback,
                ref initOptions, IntPtr.Zero);
        }

        private static void cdo_platform_init_done_callback(IntPtr ptr,
            ref CDOError err, IntPtr h)
        {
            InitStateChangedEvent.InitState state;
            if (err.err_code == 0)
            {
                _platformHandle = h;
                _renderSupport = new RenderSupport(h);
                state = InitStateChangedEvent.InitState.INITIALIZED;
            }
            else
            {
                state = InitStateChangedEvent.InitState.ERROR;
            }
            if (_listener != null)
            {
                InitStateChangedEvent e = new InitStateChangedEvent(state,
                    err.err_code, err.err_message.body);
                _listener.onInitStateChanged(e);
            }
        }

        private static void cdo_platform_init_progress_callback(IntPtr ptr,
            short sh)
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
            if (_platformHandle == IntPtr.Zero)
                return;
            _renderSupport.shutdown();
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

        public static void renderSink(Responder<RenderingWidget> responder,
            RenderOptions options)
        {
            if (_renderSupport != null)
            {
                _renderSupport.renderSink(responder, options);
            }
            else 
            {
                responder.errHandler(-1, "Platform not initialized");
            }

        }
        
        // *****************************************************************
        // ********************* ResponderAdapter **************************

        public static Responder<T> createResponder<T>(
            ResultHandler<T> rh = null, ErrHandler errH = null)
        {
            return new ResponderAdapter<T>(rh, errH);
        }

        public static Responder<T> R<T>(ResultHandler<T> rh = null,
            ErrHandler errH = null)
        {
            return createResponder<T>(rh, errH);
        }

        #endregion

        public static object initOptions { get; set; }
    }
}
