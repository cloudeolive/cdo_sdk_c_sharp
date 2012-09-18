using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{
    public sealed class ErrorCodes
    {

        public static class Logic
        {

            /// <summary>
            /// Returned when application tries to perform operation on Cloudeo Service 
            /// in context of media scope to which service is not currently connected.
            /// </summary>
            /// <see cref="CDO.CloudeoService.disconnect"/>
            /// <see cref="CDO.CloudeoService.publish"/>
            /// <see cref="CDO.CloudeoService.unpublish"/>
            /// <see cref="CDO.CloudeoService.sendMessage"/>
            /// <since>1.0.0.0</since>
            public static int INVALID_ROOM = 1001;

            /**
             * Returned when application passed somehow invalid argument to any of the
             * CloudeoService methods.
             *
             * @since 1.0.0.0
             */
            public static int INVALID_ARGUMENT = 1002;

            /**
             * Returned when CDO.CloudeoService#getProperty or
             * CDO.CloudeoService#getProperty was called with invalid (unknown) key.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#getProperty
             * @see CDO.CloudeoService#setProperty
             */
            public static int INVALID_JS_PARAMETER_KEY = 1003;

            /**
             * Indicates that there was unknown; fatal error during platform
             * initialization. Such a platform initialization includes e.g.
             * initialization of the COM model on windows.
             *
             * @since 1.0.0.0
             */
            public static int PLATFORM_INIT_FAILED = 1004;

            /**
             * Indicates that client tried to create service while Cloudeo Plugin is
             * performing auto-update.
             *
             * @since 1.0.0.0
             */
            public static int PLUGIN_UPDATING = 1005;

            /**
             * Indicates that there was internal; logic failure. Most likely it's caused
             * by bug in the Cloudeo Plug-in code.
             *
             * @since 1.0.0.0
             */
            public static int INTERNAL = 1006;

            /**
             * Indicates that plugin container couldn't load logic library; most likely
             * because it is running in Windows Low Integrity mode (less privileged) and
             * the lib is already loaded by process that runs in medium integrity mode
             * (more privileged). Such a situation may occur if the Cloudeo SDK is used
             * by user in 2 browsers in same time. The first browser launched was
             * non-IE; the second (the one where error is reported) is IE.
             *
             * @since 1.0.0.0
             */
            public static int LIB_IN_USE = 1007;

            /**
             * Indicates that the user's platform is unsupported for given operation.
             *
             * @since 1.15.0.6
             */
            public static int PLATFORM_UNSUPPORTED = 1009;

            /**
             * Indicates that given operation is invalid in current state of
             * the platform.
             *
             * @since 1.15.0.6
             */
            public static int INVALID_STATE = 1010;
        }

        public static class Communication
        {
            /**
            * Indicates that Cloudeo Service was trying to connect to streaming server;
            * but cannot find given host (cannot resolve host with given IP/domain
            * address). This may happen if user lost the connection to the Internet.
            *
            * @since 1.0.0.0
            * @see CDO.CloudeoService#connect
            */
            public static int INVALID_HOST = 2001;

            /**
             * Indicate that plugin was unsuccessful with connect attempt. It managed
             * to resolve host address and connect to it; so streaming host is running;
             * but it couldn't connect to streamer application. This may happen if
             * there is a firewall device blocking communication with the streamer's
             * management endpoint.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#connect
             */
            public static int INVALID_PORT = 2002;

            /**
             * Plugin tried to connect to streamer server; established communication
             * channel; but credentials provided by JS-client were rejected by it.
             *
             * Can be caused by = 
             * - invalid credentials used by JS-client (JS-client application bug)
             * - session timeout on core server
             *
             * JS-client could try to recover by = 
             * - no recovery
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#connect
             */
            public static int BAD_AUTH = 2003;


            /**
             * Plugin tried to connect to streamer server; established management
             * communication link; but multimedia communication link failed; so there
             * is no way to transmit media data.
             * This error code can be used before OR after successful connection. When
             * triggered after successful connection; it indicates that media
             * connection was lost.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#connect
             * @see CDO.CloudeoServiceListener#onConnectionLost
             * @see CDO.ConnectionLostEvent
             */
            public static int MEDIA_LINK_FAILURE = 2005;

            /**
             * Indicates that plug-in lost connection to streaming server. Most likely
             * due to user losing Internet connection. In case of this error; it is
             * advised to notify the user about the issue and periodically try to
             * reestablish  connection to given media scope.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoServiceListener#onConnectionLost
             * @see CDO.ConnectionLostEvent
             */
            public static int REMOTE_END_DIED = 2006;


            /**
             * Indicates that plug-in couldn't connect to streaming server due to
             * internal; unknown and unexpected error. This error always indicates an
             * bug in Cloudeo Plug-In.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#connect
             * @see CDO.CloudeoServiceListener#onConnectionLost
             * @see CDO.ConnectionLostEvent
             */
            public static int INTERNAL = 2007;


            /**
             * Streamer rejected connection request because user with given id already
             * joined given media scope. User may join media scope only once.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#connect
             */
            public static int ALREADY_JOINED = 2009;

            /**
             * Indicates that the plug-in used by client is not supported by the
             * streamer to which connection attempt was made.
             *
             * Such a case will happen most likely when trying to connect using
             * a plug-in from the beta release channel to a streamer for stable channel.
             */
            public static int PLUGIN_VERSION_NOT_SUPPORTED = 2011;
        }

        public static class Media
        {
            /**
    * Indicates that currently configured video capture
    * device (webcam) is invalid and cannot be used by Cloudeo Service.
    *
    * @since 1.0.0.0
    * @see CDO.CloudeoService#startLocalVideo
    * @see CDO.CloudeoService#connect
    * @see CDO.CloudeoService#setVideoCaptureDevice
    * @see CDO.CloudeoService#publish
    */
            public static int INVALID_VIDEO_DEV = 4001;


            /**
             * Indicates that audio capture device (microphone) haven't been configured
             * using setAudioCaptureDevice; but there is attempt to make a call with
             * audio published.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#connect
             * @see CDO.CloudeoService#publish
             */
            public static int NO_AUDIO_IN_DEV = 4002;

            /**
             * Indicates that given audio capture device is invalid. May be thrown
             * with setAudioCaptureDevice or setAudioOutputDevice.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#connect
             * @see CDO.CloudeoService#publish
             */
            public static int INVALID_AUDIO_IN_DEV = 4003;

            /**
             * Indicates that given audio output device is invalid. May be thrown
             * with setAudioOutputDevice or setAudioCaptureDevice.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#connect
             * @see CDO.CloudeoService#publish
             */
            public static int INVALID_AUDIO_OUT_DEV = 4004;

            /**
             * Indicates that either audio output or capture device initialization
             * failed and plugin cannot differ or that given audio capture and output
             * devices are for some reason incompatible.
             *
             * @since 1.0.0.0
             * @see CDO.CloudeoService#connect
             * @see CDO.CloudeoService#publish
             */
            public static int INVALID_AUDIO_DEV = 4005;

        }

        public static class Common
        {
            /**
 * Indicates; general unhandled error. In general it means a bug in Cloudeo
 * Service or Cloudeo Plugin.
 *
 * @since 1.0.0.0
 */
            public static int DEFAULT_ERROR = -1;
        }
    }
}
