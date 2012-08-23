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

/**
 * Structure defining Strings used when calling cloudeo methods or
 * passed by cloudeo as results.
 */
typedef struct
{
    char body[CDO_STRING_MAX_LEN];
    size_t length;
} CDOString;

/**
 * Structure defining an error. Used by the Platform to indicate any processing
 * error. Contains a code and a message.
 */
typedef struct
{
    /**
     * Code of an error - allows developer to explicitly identify source of
     * a problem.
     */
    int err_code;

    /**
     * Additional, human-readable error message.
     */
    CDOString err_message;

} CDOError;

/**
 * Cloudeo SDK Handle type. 
 */
typedef void* CDOH;

/**
 * Device description used by the Cloudeo platform.
 * 
 * @see cdo_get_video_capture_device_names
 * @see cdo_get_audio_capture_device_names
 * @see cdo_get_audio_output_device_names
 * @see cdo_get_video_capture_device
 * @see cdo_set_video_capture_device
 * @see cdo_get_audio_capture_device
 * @see cdo_set_audio_capture_device
 * @see cdo_get_audio_output_device
 * @see cdo_set_audio_output_device
 */
typedef struct
{
    /**
     * Human readable label of a device.
     */
    CDOString label;

    /**
     * Identifier to be used when selecting a device.
     */
    CDOString id;
} CDODevice;

/**
 * Describes a video stream
 * 
 * @see cdo_connect
 * @see CDOConnectionDescriptor
 */
typedef struct
{
    /**
     * Max width of the video stream.
     */
    unsigned int maxWidth;

    /**
     * Max height of the vide ostream.
     */
    unsigned int maxHeight;

    /**
     * Max bit rate to be used by the stream.
     */
    unsigned int maxBitRate;

    /**
     * Max amount of frames per second to be transmitted.
     */
    unsigned int maxFps;

    /**
     * Flag defining whether the client wishes to publish this layer.
     */
    bool publish;

    /**
     * Flag defining whether the client wishes to receive this layer.
     */
    bool receive;

} VideoStreamDescriptor;

/**
 * Describes a connection establishement request
 * 
 * @see cdo_connect
 */
typedef struct
{
    /**
     * Configuration of the baseline (low quality) video stream
     */
    VideoStreamDescriptor lowVideoStream;

    /**
     * Configuration of the high quality video stream
     */
    VideoStreamDescriptor highVideoStream;

    /**
     * Flag defining whether the video stream should be published after
     * establishing a connection to the streaming server.
     */
    bool autopublishVideo;

    /**
     * Flag defining whether the audio stream should be published after
     * establishing a connection to the streaming server.
     */
    bool autopublishAudio;

    /**
     * URL of scope to connect to. 
     */
    CDOString url;

    /**
     * Id of scope to connect to
     */
    CDOString scopeId;

    /**
     * Client authentication token.
     */
    CDOString token;
} CDOConnectionDescriptor;

/**
 * Defines how to publish media
 * 
 * @see cdo_publish
 */
typedef struct
{
    /**
     * Used with screen media type - defines which screen sharing source to 
     * publish.
     */
    CDOString windowId;

    /**
     * Used with screen media type - defines the resolution of the rendering
     * component used to render the screen sharing stream.
     */
    int nativeWidth;

} CDOMediaPublishOptions;

/**
 * =============================================================================
 *  Rendering structures
 * =============================================================================
 */

/**
 * Defines signature of frame invalidation callback. This callback is set
 * when starting render, using the CDORenderRequest.invalidateCallback property.
 * 
 * The single one parameter passed to the callback is equal to opaque pointer
 * defined when starting the rendering - the CDORenderRequest.opaque property.
 * 
 * The implementator of the callback should obtain the actual window handle
 * or drawing context handle and request the SDK to draw the content of the 
 * sink.
 * 
 * @see cdo_render_sink
 * @see cdo_draw
 */
typedef void(*invalidate_clbck_t)(void* opaque);

/**
 * Structure defining all attributes required to start rendering video sink.
 * 
 * @see cdo_render_sink
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
 * 
 * @see cdo_draw
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
     * Platform-specific window handle or drawing context if rendering only
     * on part of the window.
     * 
     * On windows this will be either HWND or HDC from the GDI system.
     */
    void* windowHandle;

    /**
     * Id of renderer which should be affected by the Draw request.
     */
    int rendererId;

} CDODrawRequest;


/**
 * Media types
 * =============================================================================
 */

/**
 * Audio media type
 */
#define CDO_MEDIA_TYPE_AUDIO "audio"


/**
 * Video media type
 */
#define CDO_MEDIA_TYPE_VIDEO "video"

/**
 * Screen media type
 */
#define CDO_MEDIA_TYPE_SCREEN "screen"

/**
 * Connection types
 * =============================================================================
 */

/**
 * Media connection type - not connected
 */
#define CDO_CONN_TYPE_NOT_CONNECTED "MEDIA_TRANSPORT_TYPE_NOT_CONNECTED"

/**
 * Media connection type - UDP relayed
 */
#define CDO_CONN_TYPE_UDP_RELAY "MEDIA_TRANSPORT_TYPE_UDP_RELAY"

/**
 * Media connection type - UDP P2P
 */
#define CDO_CONN_TYPE_UDP_P2P "MEDIA_TRANSPORT_TYPE_UDP_P2P"

/**
 * Media connection type - TCP relayed
 */
#define CDO_CONN_TYPE_TCP_RELAY "MEDIA_TRANSPORT_TYPE_TCP_RELAY"



/**
 * =============================================================================
 *  CloudeoServiceListener-related definitions
 * =============================================================================
 */



/**
 *  Events
 * =============================================================================
 */

/**
 * Video frame size changed event - dispatched when content provided by given
 * video sink changes resolution (e.g. due to adaptation).
 */
typedef struct
{
    /**
     * Sink that changed the resolution.
     */
    CDOString sinkId;

    /**
     * New height of the video feed.
     */
    int height;

    /**
     * New width of the video feed.
     */
    int width;
} CDOVideoFrameSizeChangedEvent;

/**
 * Event indicating that the connection to the streaming server was lost.
 */
typedef struct
{
    /**
     * Id of scope to which connection was lost.
     */
    CDOString scopeId;

    /**
     * Error code identifying source of the problem.
     */
    int errCode;

    /**
     * Additional human-readable text message.
     */
    CDOString errMessage;
} CDOConnectionLostEvent;

/**
 * Event indicating change in remote user state. It includes:
 * - new user connected to given scope
 * - user disconnected from the scope
 * - user published or unpublished media of given type within the scope
 */
typedef struct
{
    /**
     * Id of scope to which this event is related
     */
    CDOString scopeId;

    /**
     * Id of remote user to which this event is related.
     */
    long long userId;

    /**
     * Flag defining whether user joined the scope or left it. Value is defined
     * only when the event is used with CDOServiceListener.onUserEvent.
     */
    bool isConnected;

    /**
     * Flag defining whether the audio stream is published by the user. Used 
     * when informing about new user or when status of audio publishing by given 
     * user was changed.
     */
    bool audioPublished;

    /**
     * Flag defining whether the video stream is published by the user. Used 
     * when informing about new user or when status of video publishing by given 
     * user was changed.
     */
    bool videoPublished;

    /**
     * Id of video sink that can be used to render user's video stream. Defined
     * only if videoPublished == true.
     */
    CDOString videoSinkId;

    /**
     * Flag defining whether the screen sharing stream is published by the user. 
     * Used  when informing about new user or when status of screen sharing 
     * publishing by given user was changed.
     */
    bool screenPublished;

    /**
     * Id of video sink that can be used to render user's screen sharing stream. 
     * Defined only if videoPublished == true.
     */
    CDOString screenSinkId;

    /**
     * Type of media which streaming status has changed.
     */
    CDOString mediaType;

} CDOUserStateChangedEvent;

/**
 * Event notifies about the change in mic activity levels (aka speech level)
 */
typedef struct
{
    /**
     * New mic activity.
     */
    int activity;
} CDOMicActivityEvent;

/**
 * Event notifies about the change in mic gain levels (done by the AGC 
 * component)
 */
typedef struct
{
    /**
     * New mic gain
     */
    int gain;
} CDOMicGainEvent;

/**
 * Event describing a change in media devices configuration (e.g. device was 
 * plugged in or plugged out).
 */
typedef struct
{
    /**
     * Flag indicating that there was either new mic plugged in or plugged out.
     */
    bool audioIn;

    /**
     * Flag indicating that there were either new speakers plugged in or plugged 
     * out.
     */
    bool audioOut;
    
    /**
     * Flag indicating that there was either new webcam in or plugged 
     * out.
     */
    bool videoIn;
} CDODeviceListChangedEvent;


/**
 * Struct describing media statisics.
 */
typedef struct
{
    int layer; // video only
    float bitRate;
    float cpu;
    float totalCpu;
    float rtt;
    float queueDelay;
    float psnr; // video only
    float fps; // video only
    int totalLoss;
    float loss;
    int quality;
    float jbLength;
    float avgJitter;
    float maxJitter;
    float avOffset; // audio only
}
CDOMediaStats;

/**
 * New media statistics reporting event.
 */
typedef struct
{
    CDOString scopeId;
    CDOString mediaType;
    long long remoteUserId;
    CDOMediaStats stats;
} CDOMediaStatsEvent;

/**
 * Event containing remote user's message broadcasted by the Cloudeo Streaming 
 * Server.
 */
typedef struct
{
    CDOString data;
    long long srcUserId;

} CDOMessageEvent;

/**
 * Event describing a change in media connection type.
 */
typedef struct
{
    CDOString scopeId;
    CDOString mediaType;
    CDOString connectionType;
} CDOMediaConnTypeChangedEvent;

/**
 * Echo event used to testing the notifications facility.
 * 
 * @private
 */
typedef struct
{
    CDOString echoValue;
} CDOEchoEvent;

/**
 *  Listener method signatures
 * =============================================================================
 */

/**
 * Defines signature for CDOVideoFrameSizeChangedEvent handler.
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_video_frame_size_changed_clbck_t)
(void* opaque, const CDOVideoFrameSizeChangedEvent* e);

/**
 * Defines signature for CDOVideoFrameSizeChangedEvent handler.
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_connection_lost_clbck_t)
(void* opaque, const CDOConnectionLostEvent* e);

/**
 * Defines signature for user state changed events handler (dispatched when 
 * remote user joins or leaves scope).
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_user_event_clbck_t)
(void* opaque, const CDOUserStateChangedEvent* e);

/**
 * Defines signature for streaming status changed events handler (dispatched 
 * when remote user publishes or stops publishing media of given type).
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_media_stream_clbck_t)
(void* opaque, const CDOUserStateChangedEvent* e);


/**
 * Defines signature for CDOMicActivityEvent handler.
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_mic_activity_clbck_t)
(void* opaque, const CDOMicActivityEvent* e);

/**
 * Defines signature for CDOMicGainEvent handler.
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_mic_gain_clbck_t)
(void* opaque, const CDOMicGainEvent* e);

/**
 * Defines signature for CDODeviceListChangedEvent handler.
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_device_list_changed_clbck_t)
(void* opaque, const CDODeviceListChangedEvent* e);



/**
 * Defines signature for CDOMediaStatsEvent handler.
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_media_stats_clbck_t)
(void* opaque, const CDOMediaStatsEvent* e);

/**
 * Defines signature for CDOMessageEvent handler.
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_message_clbck_t)
(void* opaque, const CDOMessageEvent* e);

/**
 * Defines signature for CDOMediaConnTypeChangedEvent handler.
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_media_conn_type_changed_clbck_t)
(void* opaque, const CDOMediaConnTypeChangedEvent* e);

/**
 * Defines signature for CDOEchoEvent handler.
 * 
 * @param opaque pointer passed when registering the listener using the 
 *               CDOServiceListener.opaque property
 * @param e      event pointer
 */
typedef void(*on_echo_clbck_t)
(void* opaque, const CDOEchoEvent* e);

/**
 * Struct that defines Cloudeo Service listener. Used when registering a 
 * listener.
 * 
 * @see cdo_add_service_listener
 */
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
 * Helper function that an be used to easily tell whether the call was finished
 * with an error or not
 * 
 * @param e error to check
 * @return  true if the CDOError struct indicates an error or success.
 */
LIB_EXPORT(bool) cdo_no_error(const CDOError* e);


/**
 * =============================================================================
 *  Platform initialization
 * =============================================================================
 */

/**
 * Platform initialization options.
 * 
 * @see cdo_init_platform
 */
typedef struct
{
    /**
     * Path to the Cloudeo Logic shared library.
     */
    CDOString logicLibPath;
} CDOInitOptions;




/**
 * Defines signature for functions receiving successfull platform initialization
 * notification.
 * 
 * @param opaque         opaque pointer passed as last argument to the 
 *                       cdo_init_platform
 * @param err            error indicator
 * @param platformHandle platform handle that can be used when calling the API 
 *                       methods
 */
typedef void (*cdo_platform_init_done_clbck)(void* opaque, const CDOError* err, 
        CDOH platformHandle);

/**
 * Defiens signature for functions receiving initialization progress updates.
 * 
 * @param opaque   opaque pointer passed as last argument to the 
 *                 cdo_init_platform
 * @param progress new progress value
 */
typedef void (*cdo_platform_init_progress_clbck)(void* opaque, short progress);


/**
 * Initializes the Cloudeo Platform.
 * 
 * @see cdo_release_platform
 * @param resultCallback        function pointer that will receive 
 *                              the initialization result
 * @param initializationOptions options defining "how" to initialize the 
 *                              platform
 * @param opaque                opaque pointer to be passed to the callbacks
 */
LIB_EXPORT(void) cdo_init_platform(cdo_platform_init_done_clbck resultCallback,
        CDOInitOptions* initializationOptions, void* opaque);

/**
 * Releases the Cloudeo Platform.
 * 
 * @see cdo_init_platform 
 * @param handle platform handle pointing to platform that should be released.
 */
LIB_EXPORT(void) cdo_release_platform(CDOH handle);

/**
 * =============================================================================
 *  Platform API
 * =============================================================================
 */

/**
 *  Result handler signatures
 * =============================================================================
 */

/**
 * Defines a signature for receiving a "void" call results. 
 * (e.g. cdo_set_video_capture_device).
 * 
 * @param opaque opaque pointer passed as the 3rd param to the function 
 *               invocation
 * @param err    error indicator
 */
typedef void (*cdo_void_rclbck_t)(void* opaque, const CDOError* err);

/**
 * Defines a signature for receiving a "string" call results.
 * (e.g. cdo_set_video_capture_device).
 * 
 * @param opaque       opaque pointer passed as the 3rd param to the function 
 *                     invocation
 * @param err          error indicator
 * @param resultString resulting string
 */
typedef void (*cdo_string_rclbck_t)(void* opaque, const CDOError* err, 
        const CDOString* resultString);

/**
 * Defines a signature for receiving a "string" call results.
 * (e.g. cdo_set_video_capture_device).
 * 
 * @param opaque       opaque pointer passed as the 3rd param to the function 
 *                     invocation
 * @param err          error indicator
 * @param resultint    resulting integer
 */
typedef void (*cdo_int_rclbck_t)(void* opaque, const CDOError* err, 
        int resultInt);

/**
 * Defines a signature for receiving a devices list call results.
 * (e.g. cdo_set_video_capture_device).
 * 
 * @param opaque        opaque pointer passed as the 3rd param to the function 
 *                      invocation
 * @param err           error indicator
 * @param resultList    list of devices
 * @param resultListLen size of the devices list.
 */

typedef void(*cdo_get_device_names_rclbck_t)(void* opaque, const CDOError* err, 
        CDODevice* resultList, size_t resultListLen);

/**
 *  Basic funcition
 * =============================================================================
 */


/**
 * Retrieves version of the SDK
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_get_version(cdo_string_rclbck_t resultHandler,
        CDOH handle, void* opaque);

/**
 * Registers a cloudeo service listener.
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param listener      listener to be registered
 */
LIB_EXPORT(void) cdo_add_service_listener(cdo_void_rclbck_t resultHandler,
        CDOH handle, void* opaque, const CDOServiceListener* listener);

/**
 * @private
 */
LIB_EXPORT(void) cdo_send_echo_notification(cdo_void_rclbck_t resultHandler,
        CDOH handle, void* opaque, const CDOString* content);


/**
 *  Video devices dealing 
 * =============================================================================
 */

/**
 * Retrieves list of currently installed video capture devices.
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_get_video_capture_device_names(
        cdo_get_device_names_rclbck_t rclbck, CDOH handle, void* opaque);

/**
 * Sets the video capture device to be used by platform
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param device_id     id of device to be set
 */
LIB_EXPORT(void) cdo_set_video_capture_device(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque, CDOString* device_id);

/**
 * Gets the video capture device used by the platform
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_get_video_capture_device(
        cdo_string_rclbck_t rclbck, CDOH handle, void* opaque);



/**
 *  Audio capture devices dealing
 * =============================================================================
 */

/**
 * Retrieves list of currently installed audio capture devices.
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_get_audio_capture_device_names(
        cdo_get_device_names_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 * Sets the audio capture device to be used by platform
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param device_id     id of device to be set
 */
LIB_EXPORT(void) cdo_set_audio_capture_device(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque, CDOString* device_id);

/**
 * Gets the video audio device used by the platform
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_get_audio_capture_device(
        cdo_string_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 * Audio output devices dealing
 * =============================================================================
 */

/**
 * Retrieves list of currently installed audio output devices.
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_get_audio_output_device_names(
        cdo_get_device_names_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 * Sets the audio output device to be used by platform
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param device_id     id of device to be set
 */
LIB_EXPORT(void) cdo_set_audio_output_device(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque, CDOString* device_id);

/**
 * Gets the audio output device used by the platform
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_get_audio_output_device(
        cdo_string_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 * Starts playing test sound
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_start_playing_test_sound(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque);

/**
 * Stops playing test sound
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_stop_playing_test_sound(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque);

/**
 * Configures speakers volume
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param volume        new speakers volume to be set (0-255)
 */
LIB_EXPORT(void) cdo_set_volume(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque, int volume);

/**
 * Gets the currently configured speakers volume
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_get_volume(
        cdo_int_rclbck_t rclbck, CDOH handle, void* opaque);



/**
 * Local preview management
 * =============================================================================
 */

/**
 * Starts capturing local video preview from local user's web cam
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_start_local_video(
        cdo_string_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 * Stops capturing local video preview from local user's web cam
 *
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_stop_local_video(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque);


/**
 * Connectivity
 * =============================================================================
 */

/**
 * Establishes a connection to the Cloudeo streaming server.
 * 
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param connDescr     connection descriptor
 */
LIB_EXPORT(void) cdo_connect(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDOConnectionDescriptor* connDescr);

/**
 * Terminates a connection to the Cloudeo streaming server.
 * 
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 */
LIB_EXPORT(void) cdo_disconnect(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDOString* scopeId);


/**
 * Publishes media stream.
 * 
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param scopeId       id of scope to which media stream should published
 * @param what          type of media stream to be published
 * @param options       publishing options
 */
LIB_EXPORT(void) cdo_publish(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDOString* scopeId, const CDOString* what,
        const CDOMediaPublishOptions* options = NULL);

/**
 * Stops publishes media stream.
 * 
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param scopeId       id of scope to which media stream should unpublished
 * @param what          type of media stream to be ubpublished
 */
LIB_EXPORT(void) cdo_unpublish(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDOString* scopeId, const CDOString* what);


/**
 * Sends a custom message to other users connected to the same scope.
 * 
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param scopeId       id of scope in which message should be broadcasted
 * @param msgBody       string containing the message body
 * @param msgSize       size of the message body string
 * @param recipientId   optional id of recipient. set to NULL if message should 
 *                      be broadcasted to all participants.
 */
LIB_EXPORT(void) cdo_send_message(
        cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDOString* scopeId, const char* msgBody, size_t msgSize,
        long long* recipientId = NULL);

//=============== Rendering ===============================

/**
 * Starts rendering a video sink. This method returns to the callback renderer 
 * id which should be used with cdo_stop_render and cdo_draw requests.
 * 
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param request       rendering request.
 * 
 */
LIB_EXPORT(void) cdo_render_sink(cdo_int_rclbck_t rclbck, CDOH handle, void* opaque,
        const CDORenderRequest* request);

/**
 * Stops rendering a video sink. 
 * 
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param rendererId    id of renderer to be stopped
 * 
 */
LIB_EXPORT(void) cdo_stop_render(cdo_void_rclbck_t rclbck, CDOH handle, void* opaque,
        int rendererId);

/**
 * Requests the platform to redraw the renderer.
 *  
 * @since 0.1.0
 * @param resultHandler function pointer receiving the call result
 * @param handle        platform handle  
 * @param opaque        opaque pointer passed with the result
 * @param request       draw request.
 * 
 */
LIB_EXPORT(void) cdo_draw(CDOH handle, const CDODrawRequest* request);


//==================== Next method =============================================

#ifdef	__cplusplus
}
#endif

#endif	/* CLOUDEO_SDK_H */

