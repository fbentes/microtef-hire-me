using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library.Util
{
    public static class DataBaseConnection
    {
        public static string GetConnectionString(string path, string keyString)
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
