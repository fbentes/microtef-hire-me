using StonePayments.Util.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePayments.Util.Services
{
    /// <summary>
    /// Esta classe implementa o padrão Bridge para operações CRUD com a classe BaseCRUDRepository que
    /// implementa IBaseCRUDRepository.
    /// </summary>
    /// <typeparam name="D">Interface das entidades do modelo.</typeparam>
    /// <typeparam name="E">Exceções personalizadas para a entidade do modelo D.</typeparam>
    public class BaseCRUDServiceBridge<D, E> : BaseEntityValidateModelService<D, E>,
                                                IBaseCRUDServiceBridge<D>
                                                where D : IBaseEntityModel
                                                where E : Exception, new()
    {
        public virtual IBaseCRUDRepository<D> BaseCRUDRepository { get; set; }

        public virtual async Task Insert(D entityModel)
        {
            Validate(entityModel);

            await BaseCRUDRepository.Insert(entityModel);
        }

        public virtual async Task Update(D entityModel)
        {
            Validate(entityModel);

            await BaseCRUDRepository.Update(entityModel);
        }

        public virtual async Task Delete(D entityModel)
        {
            await BaseCRUDRepository.Delete(entityModel);
        }

        public virtual async Task<List<D>> Select(D entityModel)
        {
            return await BaseCRUDRepository.Select(entityModel);
        }

        public virtual async Task<List<D>> Select()
        {
            return await BaseCRUDRepository.Select();
        }
    }
}
