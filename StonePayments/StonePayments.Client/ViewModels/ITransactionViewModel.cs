using StonePayments.Business;
using System.ComponentModel;

namespace StonePayments.Client.ViewModels
{
    public interface ITransactionViewModel
    {
        SendTransactionCommand SendTransactionCommand { get; }

        event PropertyChangedEventHandler PropertyChanged;

        long? CardNumber { get; set; }

        double? Amount { get; set; }

        TransactionType Type { get; set; }

        TransactionType[] TypeList { get; }

        byte? Number { get; set; }
    }
}