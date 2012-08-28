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
        
        #region Members
        /**
         * Class Members
         * =====================================================================
         */

        /// <summary>
        /// Default directory where the Cloudeo Native SDK should be available
        /// </summary>
        private const string DEFAULT_SDK_PATH = "cloudeo_sdk";

        
        /// <summary>
        /// Native SDK handle - used when creating the CloudoeService
        /// </summary>
        private static IntPtr _platformHandle;

        /// <summary>
        /// Listener used when reporting the platfor initialization state.
        /// </summary>
        private static PlatformInitListener _listener;
        
        /// <summary>
        /// Rendering helpers - deals with the startRender method.
        /// </summary>
        private static RenderSupport _renderSupport;

        /// <summary>
        /// Initialization complete delegate - to prevent delegate
        /// dealocation before complete.
        /// </summary>
        private static cdo_platform_init_done_clbck _onInitComplete;

        private static CloudeoService _service;

        #endregion
      
        #region Constructors
        /**
         * Constructors
         * =====================================================================
         */
        
        /// <summary>
        /// Static construction
        /// </summary>
        static Platform()
        {
            _service = null;
            _platformHandle = IntPtr.Zero;            
        }

        /// <summary>
        /// Ensuress that the Native platform will be released
        /// </summary>
        ~Platform() 
        { 
            release(); 
        }

        #endregion

        #region Public API
        /**
         * Public API
         * =====================================================================
         */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listener"></param>
        public static void init(PlatformInitListener listener)
        {
            init(listener, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="options"></param>
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
            doInit(initOptions);
        }

        // =====================================================================

        /// <summary>
        /// 
        /// </summary>
        public static void release() 
        { 
            /* dispose the platform */
            if (_platformHandle == IntPtr.Zero)
                return;
            _renderSupport.shutdown();
            NativeAPI.cdo_release_platform(_platformHandle);
            _platformHandle = IntPtr.Zero;
        }

        // =====================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CloudeoService getService()
        {
            return _service;
        }

        // =====================================================================
        
        /// <summary>
        /// 
        /// </summary>
        public static CloudeoService Service
        {
            get { return _service; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responder"></param>
        /// <param name="options"></param>
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

        // =====================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rh"></param>
        /// <param name="errH"></param>
        /// <returns></returns>
        public static Responder<T> createResponder<T>(
            ResultHandler<T> rh = null, ErrHandler errH = null)
        {
            return new ResponderAdapter<T>(rh, errH);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rh"></param>
        /// <param name="errH"></param>
        /// <returns></returns>
        public static Responder<T> R<T>(ResultHandler<T> rh = null,
            ErrHandler errH = null)
        {
            return createResponder<T>(rh, errH);
        }

        #endregion

        #region Private helpers
        /**
         * Private helpers
         * =====================================================================
         */

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        private static void doInit(CDOInitOptions options)
        {
            _onInitComplete = new cdo_platform_init_done_clbck(onInitComplete);
            NativeAPI.cdo_init_platform(_onInitComplete, ref options,
                IntPtr.Zero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="err"></param>
        /// <param name="h"></param>
        private static void onInitComplete(IntPtr ptr,
            ref CDOError err, IntPtr h)
        {
            InitStateChangedEvent.InitState state;
            if (err.err_code == 0)
            {
                _platformHandle = h;
                _renderSupport = new RenderSupport(h);
                _service = new CloudeoServiceImpl(h);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="sh"></param>
        private static void cdo_platform_init_progress_callback(IntPtr ptr,
            short sh)
        {
            if (_listener != null)
            {
                InitProgressChangedEvent e = new InitProgressChangedEvent(sh);
                _listener.onInitProgressChanged(e);
            }
        }
        #endregion
    }
}
