using StonePayments.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePayments.Util.Repositories
{
    /// <summary>
    /// Interface para operações CRUD para uso de repósitório para qualquer banco de dados.
    /// </summary>
    /// <typeparam name="D"></typeparam>
    public interface IBaseCRUDRepository<D> where D : IBaseEntityModel
    {
        Task Insert(D entity);

        Task Update(D entity);

        Task Delete(D entity);

        Task<List<D>> Select(D entity);

        Task<List<D>> Select();
    }
}
