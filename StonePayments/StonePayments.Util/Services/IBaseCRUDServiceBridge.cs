using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Util.Services
{
    public interface IBaseCRUDServiceBridge<D> where D : IBaseEntityModel
    {
        Task Insert(D entity);

        Task Update(D entity);

        Task Delete(D entity);

        Task<List<D>> Select(D entity);

        Task<List<D>> Select();
    }
}
