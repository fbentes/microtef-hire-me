using StonePayments.Util.Attributes;
using System;
using System.Reflection;

namespace StonePayments.Util
{
    /// <summary>
    /// Classe responsável para validar, via annotation em cada propriedade, todas as propriedades do
    /// objeto. Pode ser usada tanto do lado do cliente quanto do lado do servidor.
    /// </summary>
    public static class ValidationProperties
    {
        /// <summary>
        /// Se não houver pelo menos uma invalidação de propriedade anotada, então retorna true.
        /// Caso contrário retorna false. 
        /// </summary>
        /// <param name="entity">Objeto a ser validado com anotações em cada uma de suas propriedades.</param>
        /// <param name="errorList">Lista de mensagems para cada propriedade inválida. </param>
        /// <returns></returns>
        public static bool IsValid(IBaseEntityModel entity, out ValidationErrorList errorList)
        {
            errorList = new ValidationErrorList();

            foreach (PropertyInfo propInfo in entity.GetType().GetProperties())
            {
                ValidProperty(entity, propInfo, ref errorList);
            }

            return errorList.Count == 0;
        }

        public static void ValidProperty(IBaseEntityModel entity, PropertyInfo propertyInfo, ref ValidationErrorList errorList)
        {
            object[] attributes = propertyInfo.GetCustomAttributes(true);

            foreach (var attribute in attributes)
            {
                var baseValidatorAttribute = attribute as BaseValidatorAttribute;

                if (baseValidatorAttribute != null)
                {
                    object value = propertyInfo.GetValue(entity);

                    if (baseValidatorAttribute is RequiredValueAttribute)
                    {
                        if (value == null || value.ToString().Trim() == string.Empty)
                        {
                            errorList.Add("O campo " + propertyInfo.Name + " não pode ser nulo !");
                        }
                    }
                    else if (baseValidatorAttribute is MinRequiredValueAttribute)
                    {
                        var range = baseValidatorAttribute as MinRequiredValueAttribute;

                        var v = Convert.ToDouble(value);

                        if (v < range.MinValue)
                        {
                            errorList.Add("O campo " + propertyInfo.Name + " deve ser no mínimo igual a " + range.MinValue + " !");
                        }
                    }
                    else if (baseValidatorAttribute is RangeLengthValuesAttribute)
                    {
                        var range = baseValidatorAttribute as RangeLengthValuesAttribute;

                        if (range.IsDecryptValue)
                        {
                            value = Cryptography.Decrypt(value?.ToString(), KeyStringCryptography.VALUE);
                        }

                        if (value == null ||
                            value.ToString().Trim().Length < range.MinLengthValue ||
                            value.ToString().Trim().Length > range.MaxLengthValue)
                        {
                            errorList.Add("O campo " + propertyInfo.Name + " deve conter entre " + range.MinLengthValue + " a " + range.MaxLengthValue + " dígitos !");
                        }
                    }
                }
            }
        }
    }
}
