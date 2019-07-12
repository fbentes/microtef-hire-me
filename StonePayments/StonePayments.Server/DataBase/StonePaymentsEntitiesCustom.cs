using StonePayments.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StonePayments.Server
{
    public class StonePaymentsEntitiesCustom : StonePaymentsEntities
    {
        public StonePaymentsEntitiesCustom()
        {
            string path = Environment.CurrentDirectory + "\\DataBaseConnection.json";

            string stringConnection = DataBaseConnection.GetConnectionString(path, KeyStringConnection.VALUE);

            Database.Connection.ConnectionString = stringConnection;
        }
    }
}