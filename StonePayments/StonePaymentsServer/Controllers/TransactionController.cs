using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Util;
using LightInject;
using StonePaymentsBusiness;
using StonePaymentsServer.Dal;
using StonePaymentsServer.Services;

namespace StonePaymentsServer.Controllers
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
        public async Task<IHttpActionResult> SendTransaction([FromBody]TransactionModel json)
        {
            //var transactionModel = JSONHelper.Deserialize<TransactionModel>(json.ToString());

            try
            {
                await TransactionService.SendTransaction(json);

                return Ok<string>(StonePaymentResource.TransactionSendOk);
            }
            catch (Exception E)
            {
                return Ok<string>(E.Message);

            }
        }
    }
}
