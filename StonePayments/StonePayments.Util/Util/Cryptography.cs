using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Util
{
    /// <summary>
    /// Classe para criptografia de valores strings, seja para senhas, conexões com o banco, etc.
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// Gerador de uma chave base para a criptografia e descriptografia de valores.
        /// Após gerá-la, copiar e colar na variável KeyStringConnection.VALUE.
        /// </summary>
        /// <param name="baseKey">Chave base a ser passada pelo cliente. Pode ser qualquer string.</param>
        /// <returns></returns>
        public string GenerateAPassKey(string baseKey)
        {
            string passPhrase = baseKey;

            string saltValue = baseKey;

            string hashAlgorithm = "SHA1";

            int passwordIterations = 2;

            int keySize = 256;

            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            byte[] Key = pdb.GetBytes(keySize / 11);

            String KeyString = Convert.ToBase64String(Key);

            return KeyString;
        }

        /// <summary>
        /// Criptografa um valor string e o retorna para o cliente.
        /// String EncryptedPassword = Cryptography.Encrypt(@"data source=DESKTOP-QGINSHI\SQLEXPRESS;initial catalog=StonePayments;user id=sa;Password=senha;", KeyStringConnection.VALUE);
        /// 
        /// </summary>
        /// <param name="valueString">Valor a ser criptografado, como senhas, por exemplo.</param>
        /// <param name="KeyString">A chave base criptografada pelo método GenerateAPassKey(string baseKey).</param>
        /// <returns>Retorna valueString criptografado.</returns>
        public static string Encrypt(string valueString, string KeyString)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();

            aesEncryption.KeySize = 256;

            aesEncryption.BlockSize = 128;

            aesEncryption.Mode = CipherMode.ECB;

            aesEncryption.Padding = PaddingMode.ISO10126;

            byte[] KeyInBytes = Encoding.UTF8.GetBytes(KeyString);

            aesEncryption.Key = KeyInBytes;

            byte[] plainText = ASCIIEncoding.UTF8.GetBytes(valueString);

            ICryptoTransform crypto = aesEncryption.CreateEncryptor();

            byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);

            return Convert.ToBase64String(cipherText);
        }

        /// <summary>
        /// Descriptografa um valor string e o retorna para o cliente.
        /// String DecryptedPassword = Cryptography.Decrypt(EncryptedPassword, keyString);
        /// 
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="KeyString"></param>
        /// <returns>Retorna encryptedText descriptografado.</returns>
        public static string Decrypt(string encryptedText, string KeyString)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();

            aesEncryption.KeySize = 256;

            aesEncryption.BlockSize = 128;

            aesEncryption.Mode = CipherMode.ECB;

            aesEncryption.Padding = PaddingMode.ISO10126;

            byte[] KeyInBytes = Encoding.UTF8.GetBytes(KeyString);

            aesEncryption.Key = KeyInBytes;

            ICryptoTransform decrypto = aesEncryption.CreateDecryptor();

            byte[] encryptedBytes = Convert.FromBase64CharArray(encryptedText.ToCharArray(), 0, encryptedText.Length);

            return ASCIIEncoding.UTF8.GetString(decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
        }
    }
}
