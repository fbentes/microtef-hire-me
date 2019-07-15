using StonePayments.Client.ViewModels;

namespace StonePayments.Client.Views
{
    public interface IGetTransactionsWindow
    {
        IGetTransactionsViewModel GetTransactionsViewModel { get; set; }
    }
}