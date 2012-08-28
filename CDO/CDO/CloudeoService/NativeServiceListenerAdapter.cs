/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{

    class NativeServiceListenerAdapter
    {

        private CloudeoServiceListener _listener;

        private on_video_frame_size_changed_clbck_t
                _on_video_frame_size_changed_callback_t;
        private on_connection_lost_clbck_t
                _on_connection_lost_callback_t;
        private on_user_event_clbck_t
                _on_user_event_callback_t;
        private on_media_stream_clbck_t
                _on_media_stream_callback_t;
        private on_mic_activity_clbck_t
                _on_mic_activity_callback_t;
        private on_mic_gain_clbck_t
                _on_mic_gain_callback_t;
        private on_device_list_changed_clbck_t
                _on_device_list_changed_callback_t;
        private on_media_stats_clbck_t
                _on_media_stats_callback_t;
        private on_message_clbck_t _on_message_callback_t;
        private on_media_conn_type_changed_clbck_t
                _on_media_conn_type_changed_callback_t;
        private on_echo_clbck_t
                _on_echo_callback_t;

        public NativeServiceListenerAdapter(CloudeoServiceListener listener)
        {
            _listener = listener;
            _on_video_frame_size_changed_callback_t =
                new on_video_frame_size_changed_clbck_t(
                    on_video_frame_size_changed_callback_t);
            _on_connection_lost_callback_t = new on_connection_lost_clbck_t(
                on_connection_lost_callback_t);
            _on_user_event_callback_t = new on_user_event_clbck_t(
                on_user_event_callback_t);
            _on_media_stream_callback_t = new on_media_stream_clbck_t(
                on_media_stream_callback_t);
            _on_mic_activity_callback_t = new on_mic_activity_clbck_t(
                on_mic_activity_callback_t);
            _on_mic_gain_callback_t = new on_mic_gain_clbck_t(
                on_mic_gain_callback_t);
            _on_device_list_changed_callback_t =
                new on_device_list_changed_clbck_t(
                    on_device_list_changed_callback_t);
            _on_media_stats_callback_t =
                new on_media_stats_clbck_t(on_media_stats_callback_t);
            _on_message_callback_t =
                new on_message_clbck_t(on_message_callback_t);
            _on_media_conn_type_changed_callback_t =
                new on_media_conn_type_changed_clbck_t(
                    on_media_conn_type_changed_callback_t);
            _on_echo_callback_t = new on_echo_clbck_t(on_echo_callback_t);
        }

        public CDOServiceListener toNative()
        {

            CDOServiceListener nListener = new CDOServiceListener();
            nListener.opaque = IntPtr.Zero;
            nListener.onConnectionLost = _on_connection_lost_callback_t;
            nListener.onDeviceListChanged = _on_device_list_changed_callback_t;
            nListener.onEcho = _on_echo_callback_t;
            nListener.onMediaConnTypeChanged =
                _on_media_conn_type_changed_callback_t;
            nListener.onMediaStats = _on_media_stats_callback_t;
            nListener.onMediaStreamEvent = _on_media_stream_callback_t;
            nListener.onMessage = _on_message_callback_t;
            nListener.onMicActivity = _on_mic_activity_callback_t;
            nListener.onMicGain = _on_mic_gain_callback_t;
            nListener.onUserEvent = _on_user_event_callback_t;
            nListener.onVideoFrameSizeChanged =
                _on_video_frame_size_changed_callback_t;
            return nListener;
            
        }


        // CDOServiceListener callback handlers

        private void on_video_frame_size_changed_callback_t(IntPtr opaque,
            ref CDOVideoFrameSizeChangedEvent e)
        {
            if (_listener != null)
                _listener.onVideoFrameSizeChanged(
                    VideoFrameSizeChangedEvent.FromNative(e));
        }

        private void on_connection_lost_callback_t(IntPtr opaque,
            ref CDOConnectionLostEvent e)
        {
            if (_listener != null)
                _listener.onConnectionLost(
                    ConnectionLostEvent.FromNative(e));
        }

        private void on_user_event_callback_t(IntPtr opaque,
            ref CDOUserStateChangedEvent e)
        {
            if (_listener != null)
                _listener.onUserEvent(
                    UserStateChangedEvent.FromNative(e));
        }

        private void on_media_stream_callback_t(IntPtr opaque,
            ref CDOUserStateChangedEvent e)
        {
            if (_listener != null)
                _listener.onMediaStreamEvent(
                    UserStateChangedEvent.FromNative(e));
        }

        private void on_mic_activity_callback_t(IntPtr opaque,
            ref CDOMicActivityEvent e)
        {
            if (_listener != null)
                _listener.onMicActivity(
                    MicActivityEvent.FromNative(e));
        }

        private void on_mic_gain_callback_t(IntPtr opaque,
            ref CDOMicGainEvent e)
        {
            if (_listener != null)
                _listener.onMicGain(MicGainEvent.FromNative(e));
        }

        private void on_device_list_changed_callback_t(IntPtr opaque,
            ref CDODeviceListChangedEvent e)
        {
            if (_listener != null)
                _listener.onDeviceListChanged(
                    DeviceListChangedEvent.FromNative(e));
        }

        private void on_media_stats_callback_t(IntPtr opaque,
            ref CDOMediaStatsEvent e)
        {
            if (_listener != null)
                _listener.onMediaStats(
                    MediaStatsEvent.FromNative(e));
        }

        private void on_message_callback_t(IntPtr opaque,
            ref CDOMessageEvent e)
        {
            if (_listener != null)
                _listener.onMessage(MessageEvent.FromNative(e));
        }

        private void on_media_conn_type_changed_callback_t(IntPtr opaque,
            ref CDOMediaConnTypeChangedEvent e)
        {
            if (_listener != null)
                _listener.onMediaConnTypeChanged(
                    MediaConnTypeChangedEvent.FromNative(e));
        }

        private void on_echo_callback_t(IntPtr opaque, ref CDOEchoEvent e)
        {
            if (_listener != null)
                _listener.onEchoEvent(EchoEvent.FromNative(e));

        }
    }
}
