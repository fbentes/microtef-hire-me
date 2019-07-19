using StonePayments.Business;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace StonePayments.Server.Controllers
{
    public interface ICustomerController
    {
        Task<IHttpActionResult> GetAllCustomers();
        Task<IHttpActionResult> SendCustomer([FromBody]CustomerModel json);
    }
}