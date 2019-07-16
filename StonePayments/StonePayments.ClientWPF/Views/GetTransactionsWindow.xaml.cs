using StonePayments.Business;
using StonePayments.Business.Interfaces;
using StonePayments.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StonePayments.ClientWPF.Views
{
    /// <summary>
    /// GUI para a exibição da lista de todas as transações efetuadas.
    /// </summary>
    public partial class GetTransactionsWindow : Window, IViewOpenObservable
    {
        public ITransactionViewModel TransactionViewModel { get; set; }

        private MessageWaitingProcessWindow messageWaitingProcessWindow;

        public GetTransactionsWindow(ITransactionViewModel transactionViewModel)
        {
            InitializeComponent();

            TransactionViewModel = transactionViewModel;

            initEvents();
        }

        private void initEvents()
        {
            Loaded += GetTransactionsWindow_Loaded;

            Closing += GetTransactionsWindow_Closing;
        }

        private void GetTransactionsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;

            (TransactionViewModel.MainViewObservable as Window).Focus();
        }

        private void GetTransactionsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = TransactionViewModel;
        }

        public void StartProcess()
        {
            Cursor = Cursors.Wait;

            messageWaitingProcessWindow = new MessageWaitingProcessWindow();
            messageWaitingProcessWindow.Owner = this;
            messageWaitingProcessWindow.Topmost = true;
            messageWaitingProcessWindow.Message = StonePaymentResource.FetchingAllTransactions;
            messageWaitingProcessWindow.Show();
        }

        public void EndProcess()
        {
            messageWaitingProcessWindow.Close();
            Cursor = Cursors.Arrow;
        }

        public void SendResultMessage(string message, string caption = "")
        {
            if(messageWaitingProcessWindow != null)
            {
                messageWaitingProcessWindow.Close();
            }

            MessageBox.Show(message, caption);

            this.Focus();
        }
        public void Shutdown() => Application.Current.Shutdown();
    }
}
