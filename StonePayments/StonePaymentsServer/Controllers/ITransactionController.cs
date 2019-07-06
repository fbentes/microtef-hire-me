using StonePaymentsBusiness;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace StonePaymentsServer.Controllers
{
    public interface ITransactionController
    {
        Task<IHttpActionResult> GetTransactions();

        Task<IHttpActionResult> SendTransaction([FromBody]TransactionModel json);
    }
}