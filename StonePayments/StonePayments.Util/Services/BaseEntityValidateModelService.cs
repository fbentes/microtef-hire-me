using System;

namespace StonePayments.Util.Services
{
    /// <summary>
    /// Classe base para ser usada por alguma casse de serviço de negócio passando o tipo do objeto
    /// de domínio (que implementa IBaseEntity) e uma exceção, caso tenha, específica para este objeto.
    /// </summary>
    /// <typeparam name="D">Tipo de objeto de domínio.</typeparam>
    /// <typeparam name="E">Tipo da exceção a ser disparada para o tipo de objeto de domínio D</typeparam>
    public class BaseEntityValidateModelService<D, E>
        where D : IBaseEntityModel
        where E : Exception, new()
    {
        protected void Validate(D entity)
        {
            ValidationErrorList errorList = new ValidationErrorList();

            bool isValid = ValidationProperties.IsValid(entity, out errorList);

            if (!isValid)
            {
                // Dispara a exceção E caso o objeto seja inválido.
                throw (E)Activator.CreateInstance(typeof(E), errorList.ToString());
            }
        }
    }
}