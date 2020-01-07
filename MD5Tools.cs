using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Szn.Framework.UtilPackage
{
    public static class MD5Tools
    {
        private static readonly MD5CryptoServiceProvider _md5;

        static MD5Tools()
        {
            _md5 = new MD5CryptoServiceProvider();
        }

        public static string GetStringMd5(string InString)
        {
            return GetBytesMd5(Encoding.UTF8.GetBytes(InString));
        }

        public static string GetFileMd5(string InFilePath)
        {
            if (!string.IsNullOrEmpty(InFilePath) && File.Exists(InFilePath))
            {
                return GetBytesMd5(File.ReadAllBytes(InFilePath));
            }

            return string.Empty;
        }

        public static string GetBytesMd5(byte[] InBytes)
        {
            byte[] hashBytes = _md5.ComputeHash(InBytes);
            int hashBytesLength = hashBytes.Length;
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < hashBytesLength; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}