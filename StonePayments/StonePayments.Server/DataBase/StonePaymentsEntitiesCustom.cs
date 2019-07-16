using StonePayments.Util;
using System;

namespace StonePayments.Server
{
    /// <summary>
    /// Classe especializada para capturar a StringConnection descriptografada e setá-la para
    /// o DataBase.
    /// </summary>
    public class StonePaymentsEntitiesCustom : StonePaymentsEntities
    {
        public StonePaymentsEntitiesCustom()
        {
            string path = Environment.CurrentDirectory + "\\DataBaseConnection.json";

            string stringConnection = DataBaseConnectionDecryptedString.Execute(path, KeyStringCryptography.VALUE);

            Database.Connection.ConnectionString = stringConnection;
        }
    }
}