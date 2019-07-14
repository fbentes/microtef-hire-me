﻿using LightInject;
using StonePayments.Business;
using System.ComponentModel;

namespace StonePayments.Client.ViewModels
{
    public class TransactionViewModel : INotifyPropertyChanged, ITransactionViewModel
    {
        [Inject]
        public ITransactionModel TransactionModel { get; set; }

        public TransactionViewModel()
        {
            //TransactionModel = new TransactionModel();
        }

        public SendTransactionCommand SendTransactionCommand
        { get
            {
                return new SendTransactionCommand(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public long? CardNumber
        {
            get { return TransactionModel.CardNumber; }
            set
            {
                if(TransactionModel.CardNumber != value)
                {
                    TransactionModel.CardNumber = value;

                    OnPropertyChange("CardNumber");
                }
            }
        }

        public double? Amount
        {
            get { return TransactionModel.Amount; }
            set
            {
                if (TransactionModel.Amount != value)
                {
                    TransactionModel.Amount = value;

                    OnPropertyChange("Amount");
                }
            }
        }

        public TransactionType Type
        {
            get { return TransactionModel.Type; }
            set
            {
                if (TransactionModel.Type != value)
                {
                    TransactionModel.Type = value;

                    OnPropertyChange("Type");
                }
            }
        }

        public TransactionType[] TypeList
        {
            get { return TransactionModel.TransactionTypeList; }
        }

        public byte? Number  // Número de parcelas
        {
            get { return TransactionModel.Number; }
            set
            {
                if (TransactionModel.Number != value)
                {
                    TransactionModel.Number = value;

                    OnPropertyChange("Number");
                }
            }
        }

        public string Password  // Número de parcelas
        {
            get { return TransactionModel.Password; }
            set
            {
                if (TransactionModel.Password != value)
                {
                    TransactionModel.Password = value;

                    OnPropertyChange("Password");
                }
            }
        }

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
