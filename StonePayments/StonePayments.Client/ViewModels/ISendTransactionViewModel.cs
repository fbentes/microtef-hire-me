using StonePayments.Business;
using StonePayments.Client.Util;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StonePayments.Client.ViewModels
{
    public interface ISendTransactionViewModel
    {
        ITransactionModel TransactionModel { get; set; }

        IViewObservable ViewObservable { get; set; }

        ObservableCollection<TransactionModel> TransactionModelList { get; set; }

        SendTransactionCommand SendTransactionCommand { get; }

        event PropertyChangedEventHandler PropertyChanged;

        long? CardNumber { get; set; }

        double? Amount { get; set; }

        TransactionType Type { get; set; }

        TransactionType[] TypeList { get; }

        byte? Number { get; set; }
    }
}