using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Util;
using StonePaymentsBusiness;
using StonePaymentsServer.Dal;

namespace StonePaymentsServer.Controllers
{
    public class TransactionController : ApiController
    {
        private TransactionDao dao;

        public TransactionController()
        {
            dao = new TransactionDao();
        }

        [HttpGet]
        [Route("stone/transactions")]
        public async Task<IHttpActionResult> GetTransactions()
        {
            var transactions = await dao.GetTransactions();

            return Ok<List<TransactionModel>>(transactions);
        }

        [HttpPost]
        public async Task<IHttpActionResult> SendTransaction([FromBody]object json)
        {
            var transactionModel = JSONHelper.GetObjectFromJSONString<TransactionModel>(json.ToString());

            try
            {
                await dao.SendTransaction(transactionModel);

                return Ok<string>(StonePaymentResource.TransactionSendOk);
            }
            catch (Exception E)
            {
                return Ok<string>(E.Message);

            }
        }
    }
}
