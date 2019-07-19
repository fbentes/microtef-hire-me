using StonePayments.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePayments.Server.Services
{ 
    public interface ICustomerService
    {
        Task Insert(CustomerModel customerModel);

        Task Update(CustomerModel customerModel);

        Task<List<CustomerModel>> GetCustomers(string id = "");

        Task Delete(string id);
    }
}
