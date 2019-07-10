using Library.Util;
using Library.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Util
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
                        else if(baseValidatorAttribute is RangeValuesAttribute)
                        {
                            var range = baseValidatorAttribute as RangeValuesAttribute;

                            long v = Convert.ToInt64(value);

                            if (value == null || v < range.MinValue || v > range.MaxValue)
                            {
                                errorList.Add("O campo " + propInfo.Name + " deve estar entre " + range.MinValue + " e " + range.MaxValue);
                            }
                        }
                    }
                }
            }
             

            return errorList.Count == 0;
        }
    }
}
