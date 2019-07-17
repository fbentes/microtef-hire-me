using System.Collections.Generic;
using System.Threading.Tasks;
using LightInject;
using StonePayments.Business;
using StonePayments.Server.Repositories;
using StonePayments.Util.Services;

namespace StonePayments.Server.Services
{
    public class CustomerService : BaseEntityModelService<CustomerModel, CustomerException>, ICustomerService
    {
        [Inject]
        public ICustomerRepository CustomerRepository { get; set; }

        public async Task Insert(CustomerModel customerModel)
        {
            validate(customerModel);

            await CustomerRepository.Insert(customerModel);
        }

        public async Task Update(CustomerModel customerModel)
        {
            validate(customerModel);

            await CustomerRepository.Update(customerModel);
        }

        public async Task Delete(string id)
        {
            await CustomerRepository.Delete(id);
        }

        public async Task<List<CustomerModel>> GetCustomers(string id = "")
        {
            return await CustomerRepository.GetCustomers(id);
        }
    }
}