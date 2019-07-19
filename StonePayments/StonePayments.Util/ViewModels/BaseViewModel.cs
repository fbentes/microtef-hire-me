using System.ComponentModel;

namespace StonePayments.Util.ViewModels
{
    /// <summary>
    /// Classe mãe para classes da camada ViewModel para a implementação do padrão MVVM.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool IsPropertyChangedNotNull
        {
            get
            {
                return PropertyChanged != null;
            }
        }

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
