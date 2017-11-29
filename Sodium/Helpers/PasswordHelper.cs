using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Sodium.Helpers
{
    internal static class PasswordHelper
    {
        internal static PasswordEntropyData Protect(SecureString password) {
            var bytes = Encoding.UTF8.GetBytes(ConvertToString(password));
            var entropy = new byte[32];

            using (var provide = new RNGCryptoServiceProvider()) {
                provide.GetBytes(entropy);
            }

            var @protected = ProtectedData.Protect(bytes, entropy, DataProtectionScope.CurrentUser);

            return new PasswordEntropyData(@protected, entropy);
        }

        internal static SecureString Unprotect(PasswordEntropyData data) {
            return ConvertToSecureString(Encoding.UTF8.GetString(ProtectedData.Unprotect(data.Password, data.Entropy, DataProtectionScope.CurrentUser)));
        }

        internal static string ConvertToString(SecureString secure) {
            if (secure == null)
                throw new ArgumentNullException(nameof(secure));

            var unmanagedString = IntPtr.Zero;

            try {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secure);
                return Marshal.PtrToStringUni(unmanagedString);
            } finally {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        internal static SecureString ConvertToSecureString(string password) {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            unsafe {
                fixed (char* chars = password) {
                    var secure = new SecureString(chars, password.Length);
                    secure.MakeReadOnly();
                    return secure;
                }
            }
        }
    }

    public class PasswordEntropyData
    {
        public byte[] Password { get; set; }
        public byte[] Entropy { get; set; }

        internal PasswordEntropyData(byte[] password, byte[] entropy) {
            Password = password;
            Entropy = entropy;
        }
    }
}
