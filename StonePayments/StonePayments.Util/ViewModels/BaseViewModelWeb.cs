using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace StonePayments.Util.ViewModels
{
    /// <summary>
    /// Classe mãe para a implementação do MVVM numa tela Web com dotVVM.
    /// </summary>
    public abstract class BaseViewModelWeb : BaseViewModel
    {
        private readonly Dictionary<string, object> _propertyValueStorage = new Dictionary<string, object>();

        protected T GetValue<T>(Expression<Func<T>> property)
        {
            var expression = property as LambdaExpression;
            if (expression == null)
            {
                throw new ArgumentException("The property is not a valid lambda expression!", "property");
            }

            var propertyName = ExtractPropertyName(expression);
            return GetValue<T>(propertyName);
        }

        private T GetValue<T>(string propertyName)
        {
            object value;
            if (_propertyValueStorage.TryGetValue(propertyName, out value))
            {
                return (T)value;
            }
            return default(T);
        }

        protected void SetValue<T>(Expression<Func<T>> property, T value)
        {
            var expression = property as LambdaExpression;
            if (expression == null)
            {
                throw new ArgumentException("The property is not a valid lambda expression!", "property");
            }

            var propertyName = ExtractPropertyName(expression);
            var currentValue = GetValue<T>(propertyName);

            if (Equals(currentValue, value))
                return;

            _propertyValueStorage[propertyName] = value;

            OnPropertyChange(propertyName);
        }

        protected void RaisePropertyChanged()
        {
            if (!IsPropertyChangedNotNull)
                return;

            var stackTrace = new StackTrace();
            var callingMethodName = stackTrace.GetFrame(1).GetMethod().Name;
            if (!callingMethodName.Contains("_"))
                return;

            var propertyName = callingMethodName.Split('_')[1];
            if (!string.IsNullOrEmpty(propertyName))
            {
                OnPropertyChange(propertyName);
            }
        }

        protected void RaisePropertyChanged(Expression<Func<object>> expression)
        {
            if (!IsPropertyChangedNotNull)
                return;

            var body = expression.Body as MemberExpression;

            if (body == null && expression.Body is UnaryExpression)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                body = unaryExpression.Operand as MemberExpression;
            }

            OnPropertyChange(body.Member.Name);
        }

        [Obsolete("Use the overload which takes a lambda instead!")]
        protected void RaisePropertyChanged(string propertyName)
        {
            AssertPropertyExistence(propertyName);
            OnPropertyChange(propertyName);
        }

        [Conditional("DEBUG")]
        private void AssertPropertyExistence(string propertyName)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(this)[propertyName];
            if (propertyDescriptor == null)
            {
                #if true
                Debug.Fail(string.Format("The property with the propertyName '{0}' doesn't exist.", propertyName));
                #else
                throw new InvalidOperationException(string.Format("The property with the propertyName '{0}' doesn't exist.", propertyName));
                #endif
            }
        }

        private static string ExtractPropertyName(LambdaExpression expression)
        {
            MemberExpression memberExpression;

            if (expression.Body is UnaryExpression)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = expression.Body as MemberExpression;
            }

            return memberExpression.Member.Name;
        }
    }
}
