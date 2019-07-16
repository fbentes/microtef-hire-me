using StonePayments.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StonePayments.Business.Commands
{
    public class CloseApplicationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ITransactionViewModel transactionViewModel;

        public CloseApplicationCommand(ITransactionViewModel transactionViewModel)
        {
            this.transactionViewModel = transactionViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            transactionViewModel.MainViewObservable.Shutdown();
        }
    }
}
