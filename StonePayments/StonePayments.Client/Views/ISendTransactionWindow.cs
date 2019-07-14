using StonePayments.Client.ViewModels;

namespace StonePayments.Client.Views
{
    internal interface ISendTransactionWindow
    {
        ITransactionViewModel TransactionViewModel { get; set; }
        void Show();
    }
}