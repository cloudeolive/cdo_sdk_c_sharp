/*
 * Copyright (C) Cloudeo Ltd 2012
 *
 * All rights reserved. Any use, copying, modification, distribution and selling
 * of this software and it's documentation for any purposes without authors' written
 * permission is hereby prohibited.
 */

/* 
 * File:   cloudeo_sdk.h
 * Author: Tadeusz Kozak
 *
 * Created on 21th July 2012, 16:44
 */

#ifndef CLOUDEO_SDK_H
#define	CLOUDEO_SDK_H

#include <cstdlib>

#ifdef _WIN32
#define LIB_EXPORT(type)   __declspec( dllexport ) type __cdecl
#else
#define LIB_EXPORT(type) type
#endif


#ifdef	__cplusplus
extern "C"
{
#endif

/**
 * =============================================================================
 *  General Data type definitions
 * ============================================================================= 
 */



/**
 * Max length of the String used to communicate with Cloudeo
 */
#define CDO_STRING_MAX_LEN 512

typedef struct
{
    char body[CDO_STRING_MAX_LEN];
    size_t length;
} CDOString;

typedef struct
{
    int err_code;
    CDOString err_message;

} CDOError;

/**
 * Cloudeo SDK Handle
 */
typedef void* CDOH;

/**
 * Device description used by the Cloudeo platform.
 */
typedef struct
{
    CDOString label;
    CDOString id;
} CDODevice;

/**
 * 
 */
typedef struct
{
    unsigned int maxWidth;
    unsigned int maxHeight;
    unsigned int maxBitRate;
    unsigned int maxFps;
    bool publish;
    bool receive;

} VideoStreamDescriptor;

typedef struct
{
    VideoStreamDescriptor lowVideoStream;
    VideoStreamDescriptor highVideoStream;
    bool autopublishVideo;
    bool autopublishAudio;
    CDOString url;
    CDOString scopeId;
    CDOString token;
} CDOConnectionDescriptor;

typedef struct
{
    /**
      *
      */
    CDOString windowId;

    /**
      *
      */
    int nativeWidth;

} CDOMediaPublishOptions;

/**
 * =============================================================================
 *  Rendering structures
 * =============================================================================
 */


typedef void(*invalidate_clbck_t)(void* opaque);

/**
 * Structure defining all attributes required to start rendering video sink.
 */
typedef struct
{
    /**
     * Id of video sink to render.
     */
    CDOString sinkId;


    /**
     * Name of scaling filter to be used when scaling the frames.
     */
    CDOString filter;

    /**
     * Flag defining whether the frames should be mirrored or not.
     */
    bool mirror;

    /**
     * Opaque, platform specific window handle used for rendering purposes.
     */
    void* windowHandle;

    /**
     * Opaque pointer passed to the invalidateCallback
     */
    void* opaque;

    /**
     * Callback that should be used to indicate the need to redraw the content
     * of rendering window.
     */
    invalidate_clbck_t invalidateCallback;

} CDORenderRequest;

/**
 * Defines all attributes needed to redraw video feed.
 */
typedef struct
{
    /**
     * Area of the target window to render on.
     */
    int top;
    int left;
    int bottom;
    int right;

    /**
     * Platform-specific window handle.
     */
    void* windowHandle;

    /**
     * Id of renderer which should be affected by the Draw request.
     */
    int rendererId;
} CDODrawRequest;

/**
 * =============================================================================
 *  CloudeoServiceListener-related definitions
 * =============================================================================
 */

/**
 * Events
 * =============================================================================
 */

#define CDO_MEDIA_TYPE_AUDIO "audio"
#define CDO_MEDIA_TYPE_VIDEO "video"
#define CDO_MEDIA_TYPE_SCREEN "screen"

#define CDO_CONN_TYPE_NOT_CONNECTED "MEDIA_TRANSPORT_TYPE_NOT_CONNECTED"
#define CDO_CONN_TYPE_UDP_RELAY "MEDIA_TRANSPORT_TYPE_UDP_RELAY"
#define CDO_CONN_TYPE_UDP_P2P "MEDIA_TRANSPORT_TYPE_UDP_P2P"
#define CDO_CONN_TYPE_TCP_RELAY "MEDIA_TRANSPORT_TYPE_TCP_RELAY"

typedef struct
{
    CDOString sinkId;
    int height;
    int width;
} CDOVideoFrameSizeChangedEvent;

typedef struct
{
    CDOString scopeId;
    int errCode;
    CDOString errMessage;
} CDOConnectionLostEvent;

typedef struct
{
    CDOString scopeId;

    long long userId;

    bool isConnected;

    bool audioPublished;

    bool videoPublished;
    CDOString videoSinkId;

    bool screenPublished;
    CDOString screenSinkId;

    CDOString mediaType;

} CDOUserStateChangedEvent;

typedef struct
{
    int activity;
} CDOMicActivityEvent;

typedef struct
{
    int gain;
} CDOMicGainEvent;

typedef struct
{
    bool audioIn;
    bool audioOut;
    bool videoIn;
} CDODeviceListChangedEvent;

typedef struct
{
    int layer;      // video only
    float bitRate;
    float cpu;
    float totalCpu;
    float rtt;
    float queueDelay;
    float psnr;       // video only
    float fps;        // video only
    int totalLoss;
    float loss;
    int quality;
    float jbLength;
    float avgJitter;
    float maxJitter;
    float avOffset;    // audio only
}
CDOMediaStats;

typedef struct
{
    CDOString scopeId;
    CDOString mediaType;
    long long remoteUserId;
    CDOMediaStats stats;
} CDOMediaStatsEvent;

typedef struct
{
    CDOString data;
    long long srcUserId;

} CDOMessageEvent;

typedef struct
{
    CDOString scopeId;
    CDOString mediaType;
    CDOString connectionType;
} CDOMediaConnTypeChangedEvent;

typedef struct
{
    CDOString echoValue;
} CDOEchoEvent;


typedef void(*on_video_frame_size_changed_clbck_t)
(void* opaque, const CDOVideoFrameSizeChangedEvent*);

typedef void(*on_connection_lost_clbck_t)
(void* opaque, const CDOConnectionLostEvent*);

typedef void(*on_user_event_clbck_t)
(void* opaque, const CDOUserStateChangedEvent*);

typedef void(*on_media_stream_clbck_t)
(void* opaque, const CDOUserStateChangedEvent*);

typedef void(*on_mic_activity_clbck_t)
(void* opaque, const CDOMicActivityEvent*);

typedef void(*on_mic_gain_clbck_t)
(void* opaque, const CDOMicGainEvent*);

typedef void(*on_device_list_changed_clbck_t)
(void* opaque, const CDODeviceListChangedEvent*);

typedef void(*on_device_list_changed_clbck_t)
(void* opaque, const CDODeviceListChangedEvent*);

typedef void(*on_media_stats_clbck_t)
(void* opaque, const CDOMediaStatsEvent*);

typedef void(*on_message_clbck_t)
(void* opaque, const CDOMessageEvent*);

typedef void(*on_media_conn_type_changed_clbck_t)
(void* opaque, const CDOMediaConnTypeChangedEvent*);

typedef void(*on_echo_clbck_t)
(void* opaque, const CDOEchoEvent*);

typedef struct
{
    void * opaque;
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
} CDOServiceListener;


/**
 * =============================================================================
 *  Platform initialization
 * =============================================================================
 */

/**
 * Platform initialization options
 */
typedef struct
{
    /**
     * Path to the Cloudeo Logic shared library.
     */
    CDOString logicLibPath;
} CDOInitOptions;


LIB_EXPORT(bool) cdo_no_error(const CDOError*);


/**
 */
typedef void (*cdo_platform_init_done_clbck)(void*, const CDOError*, CDOH);
typedef void (*cdo_platform_init_progress_clbck)(void*, short);


/**
 *
 * @param resultCallback
 * @param initializationOptions
 * @param opaque
 */
LIB_EXPORT(void) cdo_init_platform(cdo_platform_init_done_clbck resultCallback,
        CDOInitOptions* initializationOptions, void* opaque);

LIB_EXPORT(void) cdo_release_platform(CDOH handle);

/**
 * =============================================================================
 *  Platform API
 * =============================================================================
 */

typedef void (*cdo_void_rclbck_t)(void*, const CDOError*);
typedef void (*cdo_string_rclbck_t)(void*, const CDOError*, const CDOString*);
typedef void (*cdo_int_rclbck_t)(void*, const CDOError*, int);
typedef void(*cdo_get_device_names_rclbck_t)(void*, const CDOError*, CDODevice*, size_t);




/**
 * Retrieves version of the SDK
 *
 * @since 0.1.0
 * @param resultHandler Address of a function receiving the result of the call
 * @param handle
 * @param opaque
 */
LIB_EXPORT(void) cdo_get_version(cdo_string_rclbck_t resultHandler,
        CDOH handle, void* opaque);

LIB_EXPORT(void) cdo_add_service_listener(cdo_void_rclbck_t resultHandler,
        CDOH handle, void* opaque, const CDOServiceListener* listener);

LIB_EXPORT(void) cdo_send_echo_notification(cdo_void_rclbck_t resultHandler,
        CDOH handle, void* opaque, const CDOString* content);


//=============== Video devices dealing ===============================

/**
 * Retrieves list of currently installed video capture devices.
 *
 * @param rclbck result callback
 * @param handle platform handle
 * @param opaque PIMPL pointer
 */
LIB_EXPORT(void) cdo_get_video_capture_device_names(
        cdo_get_device_names_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 * Sets the video capture device to be used by platform
 *
 * @param rclbck
 * @param handle
 * @param opaque
 * @param device_id
 */
LIB_EXPORT(void) cdo_set_video_capture_device(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque, CDOString* device_id);

/**
 *
 * @param rclbck
 * @param handle
 * @param opaque
 */
LIB_EXPORT(void) cdo_get_video_capture_device(
        cdo_string_rclbck_t rclbck, CDOH handle, void* opaque);


//=============== Audio capture devices dealing ===============================

/**
 * Retrieves list of currently installed video capture devices.
 *
 * @param rclbck result callback
 * @param handle platform handle
 * @param opaque PIMPL pointer
 */
LIB_EXPORT(void) cdo_get_audio_capture_device_names(
        cdo_get_device_names_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 * Sets the video capture device to be used by platform
 *
 * @param rclbck
 * @param handle
 * @param opaque
 * @param device_id
 */
LIB_EXPORT(void) cdo_set_audio_capture_device(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque, CDOString* device_id);

/**
 *
 * @param rclbck
 * @param handle
 * @param opaque
 */
LIB_EXPORT(void) cdo_get_audio_capture_device(
        cdo_string_rclbck_t rclbck, CDOH handle, void* opaque);

//=============== Audio output devices dealing ===============================

/**
 * Retrieves list of currently installed video capture devices.
 *
 * @param rclbck result callback
 * @param handle platform handle
 * @param opaque PIMPL pointer
 */
LIB_EXPORT(void) cdo_get_audio_output_device_names(
        cdo_get_device_names_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 * Sets the video capture device to be used by platform
 *
 * @param rclbck
 * @param handle
 * @param opaque
 * @param device_id
 */
LIB_EXPORT(void) cdo_set_audio_output_device(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque, CDOString* device_id);

/**
 *
 * @param rclbck
 * @param handle
 * @param opaque
 */
LIB_EXPORT(void) cdo_get_audio_output_device(
        cdo_string_rclbck_t rclbck, CDOH handle, void* opaque);


//=============== Local preview management ===============================

/**
 *
 * @param rclbck
 * @param handle
 * @param opaque
 */
LIB_EXPORT(void) cdo_start_local_video(
        cdo_string_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 *
 * @param rclbck
 * @param handle
 * @param opaque
 */
LIB_EXPORT(void) cdo_stop_local_video(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque);


//=============== Connectivity ===============================

/**
 *
 * @param rclbck
 * @param handle
 * @param opaque
 * @param connDescr
 */
LIB_EXPORT(void) cdo_connect(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDOConnectionDescriptor* connDescr);

/**
 *
 * @param rclbck
 * @param handle
 * @param opaque
 * @param scopeId
 */
LIB_EXPORT(void) cdo_disconnect(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDOString* scopeId);

LIB_EXPORT(void) cdo_publish(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDOString* scopeId, const CDOString* what,
        const CDOMediaPublishOptions* options = NULL);

LIB_EXPORT(void) cdo_unpublish(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDOString* scopeId, const CDOString* what);



//=============== Rendering ===============================

/**
 *
 * @param request
 * @return
 */
LIB_EXPORT(void) cdo_render_sink(cdo_int_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDORenderRequest* request);

/**
 *
 * @param rendererId
 */
LIB_EXPORT(void) cdo_stop_render(cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        int rendererId);

/**
 *
 * @param request
 * @return
 */
LIB_EXPORT(void) cdo_draw(CDOH handle, const CDODrawRequest* request);


//==================== Next method =============================================

#ifdef	__cplusplus
}
#endif

#endif	/* CLOUDEO_SDK_H */

