using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using LightInject;
using StonePayments.Business;
using StonePayments.Util.Services;

namespace StonePayments.Server.Controllers
{
    public class CustomerController : ApiController, ICustomerController
    {
        [Inject]
        public IBaseCRUDServiceBridge<CustomerModel> CustomerService { get; set; }


        /// <summary>
        /// Retorna a lista de todos os clientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("stone/customers")]
        public async Task<IHttpActionResult> GetAllCustomers()
        {
            var customers = await CustomerService.Select();

            return Ok<List<CustomerModel>>(customers);
        }

        /// <summary>
        /// Retorna a lista de todos os clientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("stone/customer/{id}")]
        public async Task<IHttpActionResult> GetCustomer(string id)
        {
            var customers = 
                await CustomerService.Select(new CustomerModel { Id = Guid.Parse(id) });

            var customer = new CustomerModel();

            if(customers.Count > 0)
            {
                customer = customers[0];
            }

            return Ok<CustomerModel>(customer);
        }

        /// <summary>
        /// Envia uma instanção da transação para ser persistida no banco de dados.
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("stone/sendCustomer")]
        public async Task<IHttpActionResult> SendCustomer([FromBody]CustomerModel customerModel)
        {
            try
            {
                await CustomerService.Insert(customerModel);

                return Ok<string>(StonePaymentResource.CustomerSendOk);
            }
            catch (Exception E)
            {
                return Ok<string>(E.Message);
            }
        }

        /// <summary>
        /// Deleta um Customer.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("stone/deleteCustomer/{id}")]
        public async Task<IHttpActionResult> DeleteCustomers(string id)
        {
            try
            {
                await CustomerService.Delete(new CustomerModel { Id = Guid.Parse(id) });

                return Ok<string>(StonePaymentResource.CustomerDeleteOk);
            }
            catch (Exception E)
            {
                return Ok<string>(E.Message);
            }
        }

        /// <summary>
        /// Atualiza um Customer.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("stone/updateCustomer")]
        public async Task<IHttpActionResult> UpdateCustomer([FromBody]CustomerModel customerModel)
        {
            try
            {
                await CustomerService.Update(customerModel);

                return Ok<string>(StonePaymentResource.CustomerUpdateOk);
            }
            catch (Exception E)
            {
                return Ok<string>(E.Message);
            }
        }
    }
}
