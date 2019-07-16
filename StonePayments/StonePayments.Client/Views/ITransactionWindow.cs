using StonePayments.Client.ViewModels;

namespace StonePayments.Client.Views
{
    internal interface ITransactionWindow
    {
        ITransactionViewModel TransactionViewModel { get; set; }
        void Show();
    }
}