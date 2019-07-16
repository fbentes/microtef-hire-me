namespace StonePayments.Util
{
    /// <summary>
    /// Classe que armazena a chave base criptografada para ser usada por Cryptography.Encrypt e 
    /// Cryptography.Decrypt.
    /// É recomendável gerar essa chave por um utilitário externo ou até mesmo num projeto de testes.
    /// Ex.: keyString = cryptography.GenerateAPassKey("Teste para a Stone");
    /// Após gerar essa keyString, é preciso copiar e colar na constante KeyStringCryptography.VALUE.
    /// </summary>
    public sealed class KeyStringCryptography
    {
        public static readonly string VALUE = "GbUS1idGPF6QLSt6Q7faKo4TL8zQSZc=";
    }
}
