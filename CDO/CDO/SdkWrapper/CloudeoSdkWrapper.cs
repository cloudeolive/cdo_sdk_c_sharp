using System.Runtime.InteropServices;
using System;


namespace CDO
{
    using CDOH = IntPtr;
    internal class CloudeoSdkWrapper
    {
        /**
         * =============================================================================
         *  General Data type definitions
         * ============================================================================= 
         */

        /**
         * Max length of the String used to communicate with Cloudeo
         */
        private const int CDO_STRING_MAX_LEN = 512;

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOString
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=CDO_STRING_MAX_LEN)]
            public string body;
            public UInt32 length;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOError
        {
            public int err_code;
            //[MarshalAs(UnmanagedType.LPStruct)]
            public CDOString err_message;
        }

        /**
         * Cloudeo SDK Handle
         */
         //public class CDOH : IntPtr {} 

        /**
         * Device description used by the Cloudeo platform.
         */
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDODevice 
        {
            public CDOString label;
            public CDOString id;
        }

        /**
         * 
         */
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct VideoStreamDescriptor
        {
            public uint maxWidth;
            public uint maxHeight;
            public uint maxBitRate;
            public uint maxFps;
            public bool publish;
            public bool receive;

        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOConnectionDescriptor
        {
            public VideoStreamDescriptor lowVideoStream;
            public VideoStreamDescriptor highVideoStream;
            public bool autopublishVideo;
            public bool autopublishAudio;
            public CDOString url;
            public CDOString scopeId;
            public CDOString token;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOMediaPublishOptions
        {
            /**
              *
              */
            public CDOString windowId;

            /**
              *
              */
            public int nativeWidth;
        }

        /**
         * =============================================================================
         *  Rendering structures
         * =============================================================================
         */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void invalidate_clbck_t(IntPtr opaque);

        /**
         * Structure defining all attributes required to start rendering video sink.
         */
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDORenderRequest
        {
            /**
             * Id of video sink to render.
             */
            public CDOString sinkId;
            /**
             * Name of scaling filter to be used when scaling the frames.
             */
            public CDOString filter;
            /**
             * Flag defining whether the frames should be mirrored or not.
             */
            public bool mirror;
            /**
             * Opaque, platform specific window handle used for rendering purposes.
             */
            public IntPtr windowHandle;
            /**
             * Opaque pointer passed to the invalidateCallback
             */
            public IntPtr opaque;
            /**
             * Callback that should be used to indicate the need to redraw the content
             * of rendering window.
             */
            public invalidate_clbck_t invalidateCallback;
        }

        /**
         * Defines all attributes needed to redraw video feed.
         */
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDODrawRequest
        {
            /**
             * Area of the target window to render on.
             */
            public int top;
            public int left;
            public int bottom;
            public int right;
            /**
             * Platform-specific window handle.
             */
            public IntPtr windowHandle;
            /**
             * Id of renderer which should be affected by the Draw request.
             */
            public int rendererId;
        }

        /**
         * =============================================================================
         *  CloudeoServiceListener-related definitions
         * =============================================================================
         */

        /**
         * Events
         * =============================================================================
         */

        //#define CDO_MEDIA_TYPE_AUDIO "audio"
        //#define CDO_MEDIA_TYPE_VIDEO "video"
        //#define CDO_MEDIA_TYPE_SCREEN "screen"

        //#define CDO_CONN_TYPE_NOT_CONNECTED "MEDIA_TRANSPORT_TYPE_NOT_CONNECTED"
        //#define CDO_CONN_TYPE_UDP_RELAY "MEDIA_TRANSPORT_TYPE_UDP_RELAY"
        //#define CDO_CONN_TYPE_UDP_P2P "MEDIA_TRANSPORT_TYPE_UDP_P2P"
        //#define CDO_CONN_TYPE_TCP_RELAY "MEDIA_TRANSPORT_TYPE_TCP_RELAY"

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOVideoFrameSizeChangedEvent
        {
            public CDOString sinkId;
            public int height;
            public int width;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOConnectionLostEvent
        {
            public CDOString scopeId;
            public int errCode;
            public CDOString errMessage;
        } ;

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOUserStateChangedEvent
        {
            public CDOString scopeId;

            public Int64 userId;

            public bool isConnected;

            public bool audioPublished;

            public bool videoPublished;
            public CDOString videoSinkId;

            public bool screenPublished;
            public CDOString screenSinkId;

            public CDOString mediaType;

        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOMicActivityEvent
        {
            public int activity;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOMicGainEvent
        {
            public int gain;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDODeviceListChangedEvent
        {
            public bool audioIn;
            public bool audioOut;
            public bool videoIn;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOMediaStats
        {
            public int layer;      // video only
            public float bitRate;
            public float cpu;
            public float totalCpu;
            public float rtt;
            public float queueDelay;
            public float psnr;       // video only
            public float fps;        // video only
            public int totalLoss;
            public float loss;
            public int quality;
            public float jbLength;
            public float avgJitter;
            public float maxJitter;
            public float avOffset;    // audio only
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOMediaStatsEvent
        {
            CDOString scopeId;
            CDOString mediaType;
            Int64 remoteUserId;
            CDOMediaStats stats;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOMessageEvent
        {
            public CDOString data;
            Int64 srcUserId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOMediaConnTypeChangedEvent
        {
            public CDOString scopeId;
            public CDOString mediaType;
            public CDOString connectionType;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOEchoEvent
        {
            public CDOString echoValue;
        }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_video_frame_size_changed_clbck_t(IntPtr opaque, ref CDOVideoFrameSizeChangedEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_connection_lost_clbck_t(IntPtr opaque, ref CDOConnectionLostEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_user_event_clbck_t(IntPtr opaque, ref CDOUserStateChangedEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_media_stream_clbck_t(IntPtr opaque, ref CDOUserStateChangedEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_mic_activity_clbck_t(IntPtr opaque, ref CDOMicActivityEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_mic_gain_clbck_t(IntPtr opaque, ref CDOMicGainEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_device_list_changed_clbck_t(IntPtr opaque, ref CDODeviceListChangedEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_media_stats_clbck_t(IntPtr opaque, ref CDOMediaStatsEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_message_clbck_t(IntPtr opaque, ref CDOMessageEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_media_conn_type_changed_clbck_t(IntPtr opaque, ref CDOMediaConnTypeChangedEvent e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void on_echo_clbck_t(IntPtr opaque, ref CDOEchoEvent e);

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOServiceListener
        {
            IntPtr opaque;
            on_video_frame_size_changed_clbck_t onVideoFrameSizeChanged;
            on_connection_lost_clbck_t onConnectionLost;
            on_user_event_clbck_t onUserEvent;
            on_media_stream_clbck_t onMediaStreamEvent;
            on_mic_activity_clbck_t onMicActivity;
            on_mic_gain_clbck_t onMicGain;
            on_device_list_changed_clbck_t onDeviceListChanged;
            on_media_stats_clbck_t onMediaStats;
            on_message_clbck_t onMessage;
            on_media_conn_type_changed_clbck_t onMediaConnTypeChanged;
            on_echo_clbck_t onEcho;
        } 

        /**
         * =============================================================================
         *  Platform initialization
         * =============================================================================
         */

        /**
         * Platform initialization options
         */
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
        public struct CDOInitOptions
        {
            /**
             * Path to the Cloudeo Logic shared library.
             */
            public CDOString logicLibPath;
        }


        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool cdo_no_error(ref CDOError error);


        /**
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void cdo_platform_init_done_clbck(IntPtr ptr, ref CDOError err, CDOH h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void cdo_platform_init_progress_clbck(IntPtr ptr, short sh);


        /**
         *
         * @param resultCallback
         * @param initializationOptions
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_init_platform(cdo_platform_init_done_clbck resultCallback, ref CDOInitOptions initializationOptions, IntPtr opaque);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_release_platform(CDOH handle);

        /**
         * =============================================================================
         *  Platform API
         * =============================================================================
         */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void void_rclbck_t(ref CDOString str);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void cdo_void_rclbck_t(IntPtr opaque, ref CDOError error);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void cdo_void_rclbck_t2(IntPtr opaque, IntPtr error);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void cdo_string_rclbck_t(IntPtr opaque, ref CDOError error, ref CDOString str);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void cdo_int_rclbck_t(IntPtr opaque, ref CDOError error, int i);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void cdo_get_device_names_rclbck_t(IntPtr opaque, ref CDOError error, ref CDODevice device, UIntPtr size_t);


        /**
         * Retrieves version of the SDK
         *
         * @since 0.1.0
         * @param resultHandler Address of a function receiving the result of the call
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_version(cdo_string_rclbck_t resultHandler, CDOH handle, IntPtr opaque);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_add_service_listener(cdo_void_rclbck_t resultHandler, CDOH handle, IntPtr opaque, ref CDOServiceListener listener);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_send_echo_notification(cdo_void_rclbck_t resultHandler, CDOH handle, IntPtr opaque, ref CDOString content);

        //=====================================================================
        //=============== Video devices dealing ===============================
        //=====================================================================
        /**
         * Retrieves list of currently installed video capture devices.
         *
         * @param rclbck result callback
         * @param handle platform handle
         * @param opaque PIMPL pointer
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_video_capture_device_names(cdo_get_device_names_rclbck_t rclbck, CDOH handle, IntPtr opaque);


        /**
         * Sets the video capture device to be used by platform
         *
         * @param rclbck
         * @param handle
         * @param opaque
         * @param device_id
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_set_video_capture_device(cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, ref CDOString device_id);

        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_video_capture_device(cdo_string_rclbck_t rclbck, CDOH handle, IntPtr opaque);

        //=============================================================================
        //=============== Audio capture devices dealing ===============================
        //=============================================================================
        /**
         * Retrieves list of currently installed video capture devices.
         *
         * @param rclbck result callback
         * @param handle platform handle
         * @param opaque PIMPL pointer
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_audio_capture_device_names(cdo_get_device_names_rclbck_t rclbck, CDOH handle, IntPtr opaque);


        /**
         * Sets the video capture device to be used by platform
         *
         * @param rclbck
         * @param handle
         * @param opaque
         * @param device_id
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_set_audio_capture_device(cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, ref CDOString device_id);

        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_audio_capture_device(cdo_string_rclbck_t rclbck, CDOH handle, IntPtr opaque);

        //============================================================================
        //=============== Audio output devices dealing ===============================
        //============================================================================
        /**
         * Retrieves list of currently installed video capture devices.
         *
         * @param rclbck result callback
         * @param handle platform handle
         * @param opaque PIMPL pointer
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_audio_output_device_names(cdo_get_device_names_rclbck_t rclbck, CDOH handle, IntPtr opaque);


        /**
         * Sets the video capture device to be used by platform
         *
         * @param rclbck
         * @param handle
         * @param opaque
         * @param device_id
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_set_audio_output_device(cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, ref CDOString device_id);

        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_audio_output_device(cdo_string_rclbck_t rclbck, CDOH handle, IntPtr opaque);

        //========================================================================
        //=============== Local preview management ===============================
        //========================================================================
        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_start_local_video(cdo_string_rclbck_t rclbck, CDOH handle, IntPtr opaque);


        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_stop_local_video(cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque);

        //============================================================
        //=============== Connectivity ===============================
        //============================================================
        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         * @param connDescr
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_connect(cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, ref CDOConnectionDescriptor connDescr);

        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         * @param scopeId
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_disconnect(cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, ref CDOString scopeId);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_publish(cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, ref CDOString scopeId, ref CDOString what, ref CDOMediaPublishOptions options);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_unpublish(cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, ref CDOString scopeId, ref CDOString what);

        //=========================================================
        //=============== Rendering ===============================
        //=========================================================
        /**
         *
         * @param request
         * @return
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_render_sink(cdo_int_rclbck_t rclbck, CDOH handle, IntPtr opaque, ref CDORenderRequest request);

        /**
         *
         * @param rendererId
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_stop_render(cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, int rendererId);

        /**
         *
         * @param request
         * @return
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_draw(CDOH handle, ref CDODrawRequest request);
    }
}
