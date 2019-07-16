using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StonePayments.Util
{
    /// <summary>
    /// Classe responsável pela recuperação da StringConnection do DataBase descriptografada.
    /// Essa StringConnection deve ser um campo de uma string json.
    /// 
    /// Ex.: 
    /// "{
    ///   "ConnectionString": "1qJ0X5P5nHM89Xr1CEdvjZTecB7CcK7idn6XWWxZwRo1I+w67nRbx+SVwMFGO7Acb8qImnFAGyhVUUPiXJgV6x2E8SME3EBntxzXa1dvdt8HuyjksOyq3VpkU/7xGMMmt453V8xQowYHU4NYjF1zzQ=="
    /// }"
    /// </summary>
public static class DataBaseConnectionDecryptedString
    {
        /// <summary>
        /// Retorna a string connection descriptografada
        /// </summary>
        /// <param name="path"></param>
        /// <param name="keyString"></param>
        /// <returns></returns>
        public static string Execute(string path, string keyString)
        {
            string connectionString = string.Empty;

            using (StreamReader file = File.OpenText(path))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject json = (JObject)JToken.ReadFrom(reader);

                    JToken token = json["ConnectionString"];

                    if(token == null || string.IsNullOrEmpty(token.ToString()))
                    {
                        throw new ArgumentNullException("ConnectionString não encontrada no Json !");
                    }

                    string connectionStringEncrypted = token.ToString();

                    connectionString = Cryptography.Decrypt(connectionStringEncrypted, keyString);
                }
            }

            return connectionString;
        }
    }
}
