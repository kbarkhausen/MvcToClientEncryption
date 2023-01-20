using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace WebMvc.Services
{
    public interface IEncryptService
    {
        string Encrypt(string strPlainText);
    }

    public class EncryptService : IEncryptService
    {
        private readonly IConfiguration _configuration;
        
        private RijndaelManaged myRijndael = new RijndaelManaged();
        private int iterations;
        private byte[] salt;

        public EncryptService(
            IConfiguration configuration)
        {
            _configuration = configuration;            
        }

        public string Encrypt(string strPlainText)
        {
            myRijndael.BlockSize = 128;
            myRijndael.KeySize = 128;
            myRijndael.IV = HexStringToByteArray(_configuration["Encryption:IV"]);

            myRijndael.Padding = PaddingMode.PKCS7;
            myRijndael.Mode = CipherMode.CBC;
            iterations = 1000;
            salt = Encoding.UTF8.GetBytes(_configuration["Encryption:Salt"]);
            myRijndael.Key = GenerateKey(_configuration["Encryption:Password"]);

            byte[] strText = new System.Text.UTF8Encoding().GetBytes(strPlainText);
            ICryptoTransform transform = myRijndael.CreateEncryptor();
            byte[] cipherText = transform.TransformFinalBlock(strText, 0, strText.Length);

            return Convert.ToBase64String(cipherText);
        }

        private byte[] HexStringToByteArray(string strHex)
        {
            dynamic r = new byte[strHex.Length / 2];
            for (int i = 0; i <= strHex.Length - 1; i += 2)
            {
                r[i / 2] = Convert.ToByte(Convert.ToInt32(strHex.Substring(i, 2), 16));
            }
            return r;
        }

        private byte[] GenerateKey(string strPassword)
        {
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(System.Text.Encoding.UTF8.GetBytes(strPassword), salt, iterations);

            return rfc2898.GetBytes(128 / 8);
        }
    }
}
