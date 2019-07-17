using System.Collections.Generic;
using System.Threading.Tasks;
using LightInject;
using StonePayments.Business;
using StonePayments.Server.Repositories;
using StonePayments.Util.Repositories;
using StonePayments.Util.Services;

namespace StonePayments.Server.Services
{
    /// <summary>
    /// Classe estendida apenas para a sobrescrita da propriedade BaseCRUDRepository para poder
    /// ser setada pelo LightInject.
    /// </summary>
    public class CustomerService : BaseCRUDServiceBridge<CustomerModel, CustomerException>
    {
        [Inject]
        public override IBaseCRUDRepository<CustomerModel> BaseCRUDRepository { get; set; }
    }
}