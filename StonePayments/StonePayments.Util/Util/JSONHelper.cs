using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace StonePayments.Util
{
    [Obsolete("Obsoleta porque os métodos DeserializeObject() e SerializeObject() da classe Newtonsoft.Json.JsonConvert já resolvem !")]
    public static class JSONHelper
    {
        /// <summary>
        /// Serializa um objeto do tipo T e retorna sua string json.
        /// </summary>
        /// <typeparam name="T">O tipo do objeto</typeparam>
        /// <param name="obj">Uma instância do tipo T</param>
        /// <returns>Retorna uma string json</returns>
        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        /// <summary>
        /// Deserializa uma string json e retorna uma insância do tipo T.
        /// </summary>
        /// <typeparam name="T">O tipo do objeto</typeparam>
        /// <param name="json">Uma string json</param>
        /// <returns>Retorna uma instância de T</returns>
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
    }
}
