using System;
using System.Collections.Generic;
using System.Reflection;

namespace Library.Util
{
    /// <summary>
    /// Classe conversora de um tipo origem O para um tipo destino T, desde que ambos
    /// tenham as mesmas propriedades (nomes e tipos), e seus tipos sejam apenas os
    /// ditos primitivos (string, char, byte, int, long, dateTime, float, double, decimal).
    /// </summary>
    /// <typeparam name="O">Objeto de origem a ser copiado.</typeparam>
    /// <typeparam name="T">Objeto de destino que será preenchido e retornado.</typeparam>
    public static class ConvertObject<O, T>
    {
        public static T Execute(O origin)
        {
            Type originType = origin.GetType();

            IList<PropertyInfo> originProps = new List<PropertyInfo>(originType.GetProperties());

            Type targeType = typeof(T);

            IList<PropertyInfo> targetProps = new List<PropertyInfo>(targeType.GetProperties());

            T targetInstance = Activator.CreateInstance<T>();

            foreach (PropertyInfo originProp in originProps)
            {
                foreach (PropertyInfo targetProp in targetProps)
                {
                    if (originProp.Name == targetProp.Name)
                    {
                        object originPropValue = null;

                        if (originProp.PropertyType.Name == targetProp.PropertyType.Name)
                        {
                            originPropValue = originProp.GetValue(origin, null);

                            targetProp.SetValue(targetInstance, originPropValue);
                        }
                        else
                        {
                            if (originProp.PropertyType.Name.ToLower().Contains("datetime") && 
                                targetProp.PropertyType.Name.ToLower().Contains("string"))
                            {
                                originPropValue = originProp.GetValue(origin, null);

                                if (originPropValue != null)
                                {
                                    object[] attrs = targetProp.GetCustomAttributes(true);

                                    foreach (var attr in attrs)
                                    {
                                        DateTimeFormatAttribute dtAttr = attr as DateTimeFormatAttribute;

                                        if (dtAttr != null)
                                        {
                                            DateTime dataHoraOrigem = Convert.ToDateTime(originPropValue);

                                            targetProp.SetValue(targetInstance, dataHoraOrigem.ToString(dtAttr.StringFormat));

                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }

            return targetInstance;
        }

        public static List<T> Execute(List<O> originList)
        {
            List<T> targetList = new List<T>(originList.Capacity);

            foreach (O origin in originList)
            {
                T target = ConvertObject<O, T>.Execute(origin);

                targetList.Add(target);
            }

            return targetList;
        }
    }
}
