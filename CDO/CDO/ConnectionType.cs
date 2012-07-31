
namespace CDO
{
    public sealed class ConnectionType
    {


        /**
         *
         */
        public static string NOT_CONNECTED = "MEDIA_TRANSPORT_TYPE_NOT_CONNECTED";

        /**
         *
         */
        public static string UDP_RELAY = "MEDIA_TRANSPORT_TYPE_UDP_RELAY";

        /**
         *
         */
        public static string UDP_P2P = "MEDIA_TRANSPORT_TYPE_UDP_P2P";

        /**
         *
         */
        public static string TCP_RELAY = "MEDIA_TRANSPORT_TYPE_TCP_RELAY";


        /**
         * Non-constructable
         */
        private ConnectionType()
        {
        }

    }
}
