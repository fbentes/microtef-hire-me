using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Util.Repositories
{
    /// <summary>
    /// Classe mãe abstrata para operações CRUD para uso classes de repósitório.
    /// </summary>
    /// <typeparam name="D">Tipo da entidade do domínio do problema que implemente IBaseEntityModel.</typeparam>
    /// <typeparam name="E">Exceção específica para a entidade do domínio do problema. 
    /// Se não houver, então use Exception ou outra classe de Exceção que herde de Exception.</typeparam>
    /// <typeparam name="C">Tipo da classe para conexção do banco que herde de System.Data.Entity.DBContext.</typeparam>
    public abstract class AbstractCRUDRepository<D, E, C> : IBaseCRUDRepository<D> 
        where D: IBaseEntityModel, new()
        where E: Exception, new()
        where C: DbContext, new()
    {
        private delegate void PrepareEntityToWrite(D entityModel, C context);

        #region Métodos abstratos para serem implementados pela classe filha concreta. Somentes estes são necessários implementar.

        protected abstract void PrepareEntityToInsert(D entityModel, C context);

        protected abstract void PrepareEntityToUpdate(D entityModel, C context);

        protected abstract void PrepareEntityToDelete(D entityModel, C context);

        protected abstract Task<List<D>> PrepareEntityToSelect(D entityModel, C context);

        #endregion

        #region Método privado para as operações de escrita no banco, Insert, Updade e Delete.

        /// <summary>
        /// Reponsável pelas operações de escrita no banco (Insert, Update, Delete).
        /// </summary>
        /// <param name="entityModel">Instância de uma classe que implementa IBaseEntityModel.</param>
        /// <param name="prepareEntityToWrite"></param>
        /// <returns></returns>
        private async Task InputData(D entityModel, PrepareEntityToWrite prepareEntityToInput)
        {
            using (var context = new C())
            {
                var trans = context.Database.BeginTransaction();

                try
                {
                    prepareEntityToInput(entityModel, context);

                    await context.SaveChangesAsync();

                    trans.Commit();
                }
                catch (Exception E)
                {
                    trans.Rollback();

                    string message = E.Message;

                    if(E.InnerException?.InnerException != null)
                    {
                        message = E.InnerException?.InnerException.Message;
                    }

                    throw (E)Activator.CreateInstance(typeof(E), message);
                }
            }
        }

        #endregion

        #region Métodos públicos CRUD a serem expostos para os objetos clientes.

        public async Task Insert(D entityModel)
        {
            await InputData(entityModel, new PrepareEntityToWrite(PrepareEntityToInsert));
        }

        public async Task Update(D entityModel)
        {
            await InputData(entityModel, new PrepareEntityToWrite(PrepareEntityToUpdate));
        }

        public async Task Delete(D entityModel)
        {
            await InputData(entityModel, new PrepareEntityToWrite(PrepareEntityToDelete));
        }

        public async Task<List<D>> Select()
        {
            return await Select(default);
        }

        public async Task<List<D>> Select(D entityModel)
        {
            using (var context = new C())
            {
                if(entityModel == null)
                {
                    entityModel = new D();
                }

                var result = await PrepareEntityToSelect(entityModel, context);

                return result;
            }
        }

        #endregion
    }
}
