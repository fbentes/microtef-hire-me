using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StonePayments.Business;

namespace StonePayments.Client.ViewModels
{
    public class GetTransactionsViewModel : INotifyPropertyChanged, IGetTransactionsViewModel
    {
        public ObservableCollection<TransactionModel> TransactionModelList { get; set;  }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
