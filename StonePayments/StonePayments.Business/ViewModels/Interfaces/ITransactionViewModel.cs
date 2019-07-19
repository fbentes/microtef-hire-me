using StonePayments.Business;
using StonePayments.Business.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StonePayments.Business.ViewModels
{
    public interface ITransactionViewModel
    {
        ITransactionModel TransactionModel { get; set; }

        IViewObservable MainViewObservable { get; set; }

        IViewOpenObservable GetTransactionsViewObservable { get; set; }

        ObservableCollection<TransactionModel> TransactionModelList { get; set; }

        ObservableCollection<TransactionModel> AllTransactionModelList { get; set; }

        SendTransactionCommand SendTransactionCommand { get; }

        event PropertyChangedEventHandler PropertyChanged;

        long? CardNumber { get; set; }

        double? Amount { get; set; }

        TransactionType Type { get; set; }

        TransactionType[] TypeList { get; }

        byte? Number { get; set; }

        string Password { get; set; }
    }
}