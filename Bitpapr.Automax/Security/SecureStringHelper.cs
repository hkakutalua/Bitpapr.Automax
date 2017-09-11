using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Security
{
    /// <summary>
    /// Helpers for <see cref="SecureString"/> data
    /// </summary>
    public static class SecureStringHelper
    {
        public static string Unsecure(this SecureString secureString)
        {
            if (secureString == null)
                return string.Empty;

            var unmanagedString = IntPtr.Zero;

            try
            {
                // Unsecures the password
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // Clean the unmanaged allocation
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
