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
using System.Runtime.InteropServices;
namespace CDO
{

    /// <summary>
    /// ResultHandler defines a signature of methods that will be receiving asynchronous results of API method calls.
    /// </summary>
    /// <see cref="CDO.Responder"/>
    /// <typeparam name="T">Type of the API call result. Specific to each method</typeparam>
    /// <param name="result">The actuall result of the method call.</param>
    public delegate void ResultHandler<T>(T result);
    
    /// <summary>
    /// ErrHandler defines a signature for methods that receives error results of API method calls.
    /// </summary>
    /// <param name="errCode">Error code explicitly identyfing source of an issue.</param>
    /// <param name="errMessage">Additional, human-readable error message</param>
    public delegate void ErrHandler(int errCode, string errMessage);

    
    /// <summary>
    /// Class Platform is an entry point to the complete Cloudeo SDK. It provides a methods for 
    /// platform initialization and fot getting the CloudeoService interface.
    /// </summary>
    public class Platform
    {
        
        #region Members
        /*
         * Class Members
         * =====================================================================
         */

        /// <summary>
        /// Default directory where the Cloudeo Native SDK should be available
        /// </summary
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
        /// Static initialization. Private.
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
        /// Initializes the Cloudeo SDK using the default options.
        /// </summary>
        /// <param name="listener">
        /// Object that will process initialization state change notifications. 
        /// This includes progress change and different states.
        /// </param>
        public static void init(PlatformInitListener listener)
        {
            init(listener, null);
        }

        /// <summary>
        /// Initializes the Cloudeo SDK with custom options.
        /// </summary>
        /// <param name="listener">
        /// Object that will process initialization state change notifications. 
        /// This includes progress change and different states.
        /// </param>
        /// <param name="options">
        /// Additionall initialization options container.
        /// </param>
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
            SetDllDirectory(path);
            CDOString str = new CDOString();
            str.body = path;
            str.length = (UInt32)path.Length;

            CDOInitOptions initOptions = new CDOInitOptions();
            initOptions.logicLibPath = str;
            doInit(initOptions);
        }

        // =====================================================================

        /// <summary>
        /// Releases the platform resources.
        /// </summary>
        public static void release() 
        { 
            /* dispose the platform */
            if (_platformHandle == IntPtr.Zero)
                return;
            // notify service that the platform was disposed so all dangling 
            // references does not crash.
            ((CloudeoServiceImpl)_service).platformDisposed();
            NativeAPI.cdo_release_platform(_platformHandle);
            _platformHandle = IntPtr.Zero;
            // release the service to deallocate it.
            _service = null;
        }

        // =====================================================================

        /// <summary>
        /// Returns the CloudeoService interface. The 
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

            if (_service != null)
            {
                _service.renderSink(responder, options);
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

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);


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
