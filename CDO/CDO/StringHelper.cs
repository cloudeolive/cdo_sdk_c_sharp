namespace CDO
{
    internal static class StringHelper
    {
        public static CloudeoSdkWrapper.CDOString toNative(string s)
        {
            CloudeoSdkWrapper.CDOString result = new CloudeoSdkWrapper.CDOString();
            if (s != null)
            {
                result.body = s;
                result.length = (uint)s.Length;
            }
            return result;
        }

        public static string fromNative(CloudeoSdkWrapper.CDOString cdos)
        {
            return cdos.body;
        }
    }
}