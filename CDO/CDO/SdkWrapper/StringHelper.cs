/*!
 * Cloudeo SDK C# bindings.
 * http://www.cloudeo.tv
 *
 * Copyright (C) SayMama Ltd 2012
 * Released under the BSD license.
 */

namespace CDO
{
    internal static class StringHelper
    {
        internal static CDOString toNative(string s)
        {
            CDOString result = new CDOString();
            if (s != null)
            {
                result.body = s;
                result.length = (uint)s.Length;
            }
            return result;
        }

        internal static string fromNative(CDOString cdos)
        {
            return cdos.body;
        }
    }
}