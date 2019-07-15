using StonePayments.Client.ViewModels;

namespace StonePayments.Client.Views
{
    internal interface ISendTransactionWindow
    {
        ISendTransactionViewModel SendTransactionViewModel { get; set; }
        void Show();
    }
}