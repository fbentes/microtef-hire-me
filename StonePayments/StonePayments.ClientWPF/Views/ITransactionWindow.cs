
using StonePayments.Business.ViewModels;

namespace StonePayments.ClientWPF.Views
{
    internal interface ITransactionWindow
    {
        ITransactionViewModel TransactionViewModel { get; set; }
        void Show();
    }
}