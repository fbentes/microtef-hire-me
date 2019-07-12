using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using LightInject;
using StonePayments.Business;
using StonePayments.Server.Services;

namespace StonePayments.Server.Controllers
{
    public class TransactionController : ApiController, ITransactionController
    {
        [Inject]
        public ITransactionService TransactionService { get; set; }

        [HttpGet]
        [Route("stone/transactions")]
        public async Task<IHttpActionResult> GetTransactions()
        {
            var transactions = await TransactionService.GetTransactions();

            return Ok<List<TransactionModel>>(transactions);
        }

        [HttpPost]
        [Route("stone/sendTransaction")]
        public async Task<IHttpActionResult> SendTransaction([FromBody]TransactionModel transactionModel)
        {
            try
            {
                List<TransactionModel> transactionModelList  = 
                    await TransactionService.SendTransaction(transactionModel);

                return Ok<List<TransactionModel>>(transactionModelList);
            }
            catch (Exception E)
            {
                return Ok<string>(E.Message);
            }
        }
    }
}
