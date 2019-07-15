﻿using StonePayments.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StonePayments.Client.Commands
{
    public class CloseApplicationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ISendTransactionViewModel transactionViewModel;

        public CloseApplicationCommand(ISendTransactionViewModel transactionViewModel)
        {
            this.transactionViewModel = transactionViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
