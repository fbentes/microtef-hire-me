using System.ComponentModel;

namespace StonePayments.Util
{
    /// <summary>
    /// Classe mãe para classes da camada ViewModel para a implementação do padrão MVVM.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
