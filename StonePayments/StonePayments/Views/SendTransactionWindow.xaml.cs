using StonePayments.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StonePayments.Client.Views
{
    public partial class SendTransactionWindow : Window
    {
        private readonly TransactionViewModel transactionViewModel;

        public SendTransactionWindow()
        {
            InitializeComponent();

            transactionViewModel = new TransactionViewModel();

            DataContext = transactionViewModel;
        }
    }
}
