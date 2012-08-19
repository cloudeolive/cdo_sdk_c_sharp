namespace CDO
{
    internal static class StringHelper
    {
        internal static CloudeoSdkWrapper.CDOString toNative(string s)
        {
            CloudeoSdkWrapper.CDOString result = new CloudeoSdkWrapper.CDOString();
            if (s != null)
            {
                result.body = s;
                result.length = (uint)s.Length;
            }
            return result;
        }

        internal static string fromNative(CloudeoSdkWrapper.CDOString cdos)
        {
            return cdos.body;
        }
    }
}