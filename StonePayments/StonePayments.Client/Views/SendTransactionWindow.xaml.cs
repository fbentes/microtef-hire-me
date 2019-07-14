using LightInject;
using StonePayments.Client.ViewModels;
using System;
using System.Windows;

namespace StonePayments.Client.Views
{
    public partial class SendTransactionWindow : Window, ISendTransactionWindow
    {
        [Inject]
        public ITransactionViewModel TransactionViewModel { get; set; }

        public SendTransactionWindow()
        {
            InitializeComponent();

            //transactionViewModel = new TransactionViewModel();

            //DataContext = TransactionViewModel;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            DataContext = TransactionViewModel;
        }
    }
}
