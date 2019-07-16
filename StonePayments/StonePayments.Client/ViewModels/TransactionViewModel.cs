using LightInject;
using StonePayments.Business;
using StonePayments.Client.Commands;
using StonePayments.Client.Util;
using StonePayments.Util;
using System.Collections.ObjectModel;

namespace StonePayments.Client.ViewModels
{
    public class TransactionViewModel : BaseViewModel, ITransactionViewModel
    {
        [Inject]
        public ITransactionModel TransactionModel { get; set; }

        private ObservableCollection<TransactionModel> transactionModelList;

        private ObservableCollection<TransactionModel> allTransactionModelList;

        public IViewObservable MainViewObservable { get; set; }

        public IViewOpenObservable GetTransactionsViewObservable { get; set; }

        public SendTransactionCommand SendTransactionCommand
        { get
            {
                return new SendTransactionCommand(this);
            }
        }

        public CloseApplicationCommand CloseApplicationCommand
        {
            get
            {
                return new CloseApplicationCommand(this);
            }
        }

        public GetTransactionsCommand GetTransactionsCommand
        {
            get
            {
                return new GetTransactionsCommand(this);
            }
        }

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

        public ObservableCollection<TransactionModel> TransactionModelList
        {
            get
            {
               return transactionModelList;
            }
            set
            {
                if(value == null)
                {
                    transactionModelList = new ObservableCollection<TransactionModel>();
                }
                else
                {
                    transactionModelList = value;
                }

                OnPropertyChange("TransactionModelList");
            }
        }
        public ObservableCollection<TransactionModel> AllTransactionModelList
        {
            get
            {
                return allTransactionModelList;
            }
            set
            {
                if (value == null)
                {
                    allTransactionModelList = new ObservableCollection<TransactionModel>();
                }
                else
                {
                    allTransactionModelList = value;
                }

                OnPropertyChange("AllTransactionModelList");
            }
        }
    }
}
