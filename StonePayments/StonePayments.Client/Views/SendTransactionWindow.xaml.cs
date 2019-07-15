using LightInject;
using StonePayments.Business;
using StonePayments.Client.Util;
using StonePayments.Client.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace StonePayments.Client.Views
{
    public partial class SendTransactionWindow : Window, ISendTransactionWindow, IViewObservable
    {
        [Inject]
        public ISendTransactionViewModel SendTransactionViewModel { get; set; }

        private MessageWaitingProcessWindow messageWaitingProcessWindow;

        public SendTransactionWindow()
        {
            InitializeComponent();

            Loaded += SendTransactionWindow_Loaded;
        }

        private void SendTransactionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = SendTransactionViewModel;

            SendTransactionViewModel.ViewObservable = this;
        }

        public void StartProcess()
        {
            Cursor = Cursors.Wait;

            messageWaitingProcessWindow = new MessageWaitingProcessWindow();
            messageWaitingProcessWindow.Owner = this;
            messageWaitingProcessWindow.Topmost = true;
            messageWaitingProcessWindow.Message = StonePaymentResource.SendingTransactionToServer;
            messageWaitingProcessWindow.Show();
        }

        public void EndProcess()
        {
            messageWaitingProcessWindow.Close();
            Cursor = Cursors.Arrow;
        }

        public void SendResultMessage(string message, string caption = "")
        {
            messageWaitingProcessWindow.Close();

            MessageBox.Show(message, caption);
        }
    }
}
