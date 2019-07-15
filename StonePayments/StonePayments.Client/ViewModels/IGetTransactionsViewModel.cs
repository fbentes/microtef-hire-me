using StonePayments.Business;
using System.Collections.ObjectModel;

namespace StonePayments.Client.ViewModels
{
    public interface IGetTransactionsViewModel
    {
        ObservableCollection<TransactionModel> TransactionModelList { get; set; }

    }
}