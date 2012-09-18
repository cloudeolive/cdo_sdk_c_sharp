/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

using System.Runtime.InteropServices;
using System;


namespace CDO
{
    using CDOH = IntPtr;

    /**
     * =====================================================================
     *  General Data type definitions
     * =====================================================================
     */


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOString
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5120)]
        public string body;
        public UInt32 length;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOError
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
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDODevice
    {
        public CDOString label;
        public CDOString id;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOMediaPublishOptions
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
        * =====================================================================
        *  Rendering structures
        * =====================================================================
        */

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void invalidate_clbck_t(IntPtr opaque);

    /**
        * Structure defining all attributes required to start rendering video
        * sink.
        */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDORenderRequest
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
        [MarshalAs(UnmanagedType.U1)]
        public bool mirror;
        /**
            * Opaque, platform specific window handle used for rendering
            * purposes.
            */
        public IntPtr windowHandle;
        /**
            * Opaque pointer passed to the invalidateCallback
            */
        public IntPtr opaque;
        /**
            * Callback that should be used to indicate the need to redraw the
            * content of rendering window.
            */
        public invalidate_clbck_t invalidateCallback;
    }

    /**
        * Defines all attributes needed to redraw video feed.
        */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDODrawRequest
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
     * Describes single screen sharing source.
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOScreenCaptureSource
    {
        /**
         * Unique ID of a window.
         */
        public CDOString id;

        /**
         * Actual snaphot of window content.
         */
        [MarshalAs(UnmanagedType.LPStr)]
        public string  imageData;

        /**
         * Size of image data in bytes.
         */
        public UIntPtr imageDataLen;

        /**
         * Width of the window snapshot.
         */
        public int width;

        /**
         * Height of the window snapshot.
         */
        public int height;
    };

    /**
        * =====================================================================
        *  CloudeoServiceListener-related definitions
        * =====================================================================
        */

    /**
        * Events
        * =====================================================================
        */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOVideoFrameSizeChangedEvent
    {
        public CDOString sinkId;
        public int height;
        public int width;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOConnectionLostEvent
    {
        public CDOString scopeId;
        public int errCode;
        public CDOString errMessage;
    } ;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOUserStateChangedEvent
    {
        public CDOString scopeId;

        public Int64 userId;

        [MarshalAs(UnmanagedType.U1)]
        public bool isConnected;

        [MarshalAs(UnmanagedType.U1)]
        public bool audioPublished;

        [MarshalAs(UnmanagedType.U1)]
        public bool videoPublished;
        public CDOString videoSinkId;

        [MarshalAs(UnmanagedType.U1)]
        public bool screenPublished;
        public CDOString screenSinkId;

        public CDOString mediaType;

    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOMicActivityEvent
    {
        public int activity;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOMicGainEvent
    {
        public int gain;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDODeviceListChangedEvent
    {
        [MarshalAs(UnmanagedType.U1)]
        public bool audioIn;
        [MarshalAs(UnmanagedType.U1)]
        public bool audioOut;
        [MarshalAs(UnmanagedType.U1)]
        public bool videoIn;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOMediaStats
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

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOMediaStatsEvent
    {
        public CDOString scopeId;
        public CDOString mediaType;
        public Int64 remoteUserId;
        public CDOMediaStats stats;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOMessageEvent
    {
        public CDOString data;
        public Int64 srcUserId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOMediaConnTypeChangedEvent
    {
        public CDOString scopeId;
        public CDOString mediaType;
        public CDOString connectionType;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOEchoEvent
    {
        public CDOString echoValue;
    }


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_video_frame_size_changed_clbck_t(IntPtr opaque,
        ref CDOVideoFrameSizeChangedEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_connection_lost_clbck_t(IntPtr opaque,
        ref CDOConnectionLostEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_user_event_clbck_t(IntPtr opaque,
        ref CDOUserStateChangedEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_media_stream_clbck_t(IntPtr opaque,
        ref CDOUserStateChangedEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_mic_activity_clbck_t(IntPtr opaque,
        ref CDOMicActivityEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_mic_gain_clbck_t(IntPtr opaque,
        ref CDOMicGainEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_device_list_changed_clbck_t(IntPtr opaque,
        ref CDODeviceListChangedEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_media_stats_clbck_t(IntPtr opaque,
        ref CDOMediaStatsEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_message_clbck_t(IntPtr opaque,
        ref CDOMessageEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_media_conn_type_changed_clbck_t(IntPtr opaque,
        ref CDOMediaConnTypeChangedEvent e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void on_echo_clbck_t(IntPtr opaque, ref CDOEchoEvent e);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOServiceListener
    {
        public IntPtr opaque;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_video_frame_size_changed_clbck_t onVideoFrameSizeChanged;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_connection_lost_clbck_t onConnectionLost;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_user_event_clbck_t onUserEvent;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_media_stream_clbck_t onMediaStreamEvent;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_mic_activity_clbck_t onMicActivity;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_mic_gain_clbck_t onMicGain;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_device_list_changed_clbck_t onDeviceListChanged;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_media_stats_clbck_t onMediaStats;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_message_clbck_t onMessage;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_media_conn_type_changed_clbck_t onMediaConnTypeChanged;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public on_echo_clbck_t onEcho;
    }

    /**
     * =====================================================================
     *  Platform initialization
     * =====================================================================
     */

    /**
     * Platform initialization options
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct CDOInitOptions
    {
        /**
         * Path to the Cloudeo Logic shared library.
         */
        public CDOString logicLibPath;
    }


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void cdo_void_rclbck_t(IntPtr opaque,
        ref CDOError error);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void cdo_string_rclbck_t(IntPtr opaque,
        ref CDOError error, ref CDOString str);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void cdo_int_rclbck_t(IntPtr opaque,
        ref CDOError error, int i);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void cdo_get_device_names_rclbck_t(IntPtr opaque,
        ref CDOError error, IntPtr device, UIntPtr resultListLen);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void cdo_platform_init_done_clbck(IntPtr ptr,
        ref CDOError err, CDOH h);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void cdo_platform_init_progress_clbck(IntPtr ptr,
        short sh);

    /**
 * Defines a signature for receiving a screen capture pseudo-devices list (device maps to a window).
 * 
 * @param opaque        opaque pointer passed as the 3rd param to the function 
 *                      invocation
 * @param err           error indicator
 * @param resultList    list of devices
 * @param resultListLen size of the devices list.
 */

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void cdo_get_screen_capture_srcs_rclbck_t(IntPtr opaque, ref CDOError err,
        IntPtr resultList, UIntPtr resultListLen);

    internal class NativeAPI
    {



        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool cdo_no_error(ref CDOError error);




        /**
         *
         * @param resultCallback
         * @param initializationOptions
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_init_platform(
            cdo_platform_init_done_clbck resultCallback,
            ref CDOInitOptions initializationOptions,
            IntPtr opaque);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_release_platform(CDOH handle);

        /**
         * =============================================================================
         *  Platform API
         * =============================================================================
         */




        /**
         * Retrieves version of the SDK
         *
         * @since 0.1.0
         * @param resultHandler Address of a function receiving the result of the call
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_version(
            cdo_string_rclbck_t resultHandler, CDOH handle, IntPtr opaque);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_set_application_id(cdo_void_rclbck_t rclbck,
        CDOH handle, IntPtr opaque, long applicationId);


        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_add_service_listener(
            cdo_void_rclbck_t resultHandler, CDOH handle, IntPtr opaque,
            ref CDOServiceListener listener);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_send_echo_notification(
            cdo_void_rclbck_t resultHandler, CDOH handle, IntPtr opaque,
            ref CDOString content);

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
        public static extern void cdo_get_video_capture_device_names(
            cdo_get_device_names_rclbck_t rclbck, CDOH handle, IntPtr opaque);


        /**
         * Sets the video capture device to be used by platform
         *
         * @param rclbck
         * @param handle
         * @param opaque
         * @param device_id
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_set_video_capture_device(
            cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque,
            ref CDOString device_id);

        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_video_capture_device(
            cdo_string_rclbck_t rclbck, CDOH handle, IntPtr opaque);

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
        public static extern void cdo_get_audio_capture_device_names(
            cdo_get_device_names_rclbck_t rclbck, CDOH handle, IntPtr opaque);


        /**
         * Sets the video capture device to be used by platform
         *
         * @param rclbck
         * @param handle
         * @param opaque
         * @param device_id
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_set_audio_capture_device(
            cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque,
            ref CDOString device_id);

        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_audio_capture_device(
            cdo_string_rclbck_t rclbck, CDOH handle, IntPtr opaque);

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
        public static extern void cdo_get_audio_output_device_names(
            cdo_get_device_names_rclbck_t rclbck, CDOH handle, IntPtr opaque);


        /**
         * Sets the video capture device to be used by platform
         *
         * @param rclbck
         * @param handle
         * @param opaque
         * @param device_id
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_set_audio_output_device(
            cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque,
            ref CDOString device_id);

        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_audio_output_device(
            cdo_string_rclbck_t rclbck, CDOH handle, IntPtr opaque);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_start_playing_test_sound(
            cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_stop_playing_test_sound(
            cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_set_volume(
            cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, int volume);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_volume(
            cdo_int_rclbck_t rclbck, CDOH handle, IntPtr opaque);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_monitor_mic_activity(
            cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque, bool monitor);

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
        public static extern void cdo_start_local_video(
            cdo_string_rclbck_t rclbck, CDOH handle, IntPtr opaque);

        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_stop_local_video(
            cdo_void_rclbck_t rclbck, CDOH handle, IntPtr opaque);


        //========================================================================
        //=============== Screen sharing           ===============================
        //========================================================================

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_get_screen_capture_sources(
                cdo_get_screen_capture_srcs_rclbck_t rclbck, CDOH handle,
                IntPtr opaque, int targetWidth);

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
        public static extern void cdo_connect_string(cdo_void_rclbck_t rclbck,
            CDOH handle, IntPtr opaque, ref CDOString connDescr);

        /**
         *
         * @param rclbck
         * @param handle
         * @param opaque
         * @param scopeId
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_disconnect(cdo_void_rclbck_t rclbck,
            CDOH handle, IntPtr opaque, ref CDOString scopeId);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_publish(cdo_void_rclbck_t rclbck,
            CDOH handle, IntPtr opaque, ref CDOString scopeId,
            ref CDOString what, ref CDOMediaPublishOptions options);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_unpublish(cdo_void_rclbck_t rclbck,
            CDOH handle, IntPtr opaque, ref CDOString scopeId,
            ref CDOString what);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_send_message(cdo_void_rclbck_t rclbck,
            CDOH handle, IntPtr opaque, ref CDOString scopeId,
            [MarshalAs(UnmanagedType.LPStr)]string msgBody,
            UIntPtr msgSize, ref Int64 recipientId);

        //=========================================================
        //=============== Statistics ==============================
        //=========================================================

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_start_measuring_stats(cdo_void_rclbck_t rclbck, 
            CDOH handle, IntPtr opaque, ref CDOString scopeId, int interval);

        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_stop_measuring_stats(cdo_void_rclbck_t rclbck, CDOH handle, 
            IntPtr opaque, ref CDOString scopeId);

        //=========================================================
        //=============== Rendering ===============================
        //=========================================================
        /**
         *
         * @param request
         * @return
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_render_sink(cdo_int_rclbck_t rclbck,
            CDOH handle, IntPtr opaque, ref CDORenderRequest request);

        /**
         *
         * @param rendererId
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_stop_render(cdo_void_rclbck_t rclbck,
            CDOH handle, IntPtr opaque, int rendererId);

        /**
         *
         * @param request
         * @return
         */
        [DllImport("cdo_sdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cdo_draw(CDOH handle,
            ref CDODrawRequest request);
    }
}
