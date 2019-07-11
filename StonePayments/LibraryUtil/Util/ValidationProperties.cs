using StonePayments.Util;
using StonePayments.Util.Attributes;
using System;
using System.Reflection;

namespace StonePayments.Util
{
    public static class ValidationProperties
    {
        public static bool IsValid(BaseEntity entity, out ErrorList errorList)
        {
            errorList = new ErrorList();

            foreach (PropertyInfo propInfo in entity.GetType().GetProperties())
            {
                object[] attributes = propInfo.GetCustomAttributes(true);

                foreach (var attribute in attributes)
                {
                    var baseValidatorAttribute = attribute as BaseValidatorAttribute;

                    if(baseValidatorAttribute != null)
                    {
                        object value = propInfo.GetValue(entity);

                        if(baseValidatorAttribute is RequiredValueAttribute)
                        {
                            if(value == null || value.ToString().Trim() == string.Empty)
                            {
                                errorList.Add("O campo " +propInfo.Name+ " não pode ser nulo !");
                            }
                        }
                        else if(baseValidatorAttribute is MinRequiredValueAttribute)
                        {
                            var range = baseValidatorAttribute as MinRequiredValueAttribute;

                            var v = Convert.ToDouble(value);

                            if (v < range.MinValue)
                            {
                                errorList.Add("O campo " + propInfo.Name + " tem que ter valor mínimo = " + range.MinValue + "!");
                            }
                        }
                        else if (baseValidatorAttribute is RangeLengthValuesAttribute)
                        {
                            var range = baseValidatorAttribute as RangeLengthValuesAttribute;

                            if (value == null ||
                                value.ToString().Trim().Length < range.MinLengthValue ||
                                value.ToString().Trim().Length > range.MaxLengthValue)
                            {
                                errorList.Add("O campo " + propInfo.Name + " deve conter de " + range.MinLengthValue + " a " + range.MaxLengthValue + " dígitos !");
                            }
                        }
                    }
                }
            }
             

            return errorList.Count == 0;
        }
    }
}
