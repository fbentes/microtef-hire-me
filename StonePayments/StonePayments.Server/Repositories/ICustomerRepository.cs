using StonePayments.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePayments.Server.Repositories
{
    public interface ICustomerRepository
    {
        Task Insert(CustomerModel customerModel);

        Task Update(CustomerModel customerModel);

        Task<List<CustomerModel>> GetCustomers(string id = "");

        Task Delete(string id);
    }
}
