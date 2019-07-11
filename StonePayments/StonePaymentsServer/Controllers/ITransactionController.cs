using StonePayments.Business;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace StonePayments.Server.Controllers
{
    public interface ITransactionController
    {
        Task<IHttpActionResult> GetTransactions();

        Task<IHttpActionResult> SendTransaction([FromBody]TransactionModel json);
    }
}