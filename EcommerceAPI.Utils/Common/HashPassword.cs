using System.Security.Cryptography;

namespace EcommerceAPI.Utils.Common
{
    public class HashPassword
    {

        public static string Encrypt(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA512);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string encryptedPassword = Convert.ToBase64String(hashBytes);
            return encryptedPassword;
        }

    }
}
