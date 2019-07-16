using LightInject;
using StonePayments.Business;
using StonePayments.Business.Interfaces;
using StonePayments.Business.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace StonePayments.ClientWPF.Views
{
    public partial class TransactionWindow : Window, ITransactionWindow, IViewObservable
    {
        [Inject]
        public ITransactionViewModel TransactionViewModel { get; set; }

        private MessageWaitingProcessWindow messageWaitingProcessWindow;

        public TransactionWindow()
        {
            InitializeComponent();

            initEvents();
        }

        private void initEvents()
        {
            Loaded += TransactionWindow_Loaded;

            //Sua função está explicada no método CmdTypeList_SelectionChanged();
            cmdTypeList.SelectionChanged += CmdTypeList_SelectionChanged;

            // O componente PasswordBox não possui Binding, e por isso é necessário capturar 
            // seu evento LostFocus para setar o conteúdo para o TransactionViewModel.Password.
            txtPassword.LostFocus += TxtPassword_LostFocus;  

            // Não vi como permitir apenas somente dígitos de 0 a 9, então é necessário capturar
            // seu evento LostFocus não deixar que dígitos diferentes de 0 a 9 sejam informados.
            txtCardNumber.LostFocus += TxtCardNumber_LostFocus;
        }

        private void TxtCardNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            string value = ((System.Windows.Controls.TextBox)e.Source).Text;

            if (string.IsNullOrEmpty(value))
            {
                TransactionViewModel.CardNumber = null;
            }
            else
            {
                long result;

                if(!long.TryParse(value, out result))
                {
                    MessageBox.Show(StonePaymentResource.EnterNumbersOnly, StonePaymentResource.ValidationTitle);
                }
            }
        }

        private void TxtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            TransactionViewModel.Password = txtPassword.Password;
        }

        /// <summary>
        ///  Método utilizado para desabilitar o campo Number na Window e setá-lo pra 1 quando for selecionado 
        ///  'Debit' na ComboBox cmdTypeList, pois este campo só ficará habilitado quando for Credit na ComboBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdTypeList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = (TransactionType)Enum.Parse(typeof(TransactionType), e.AddedItems[0].ToString(), true);

            txtNumber.IsEnabled = item != TransactionType.Debit;
            lblNumber.IsEnabled = txtNumber.IsEnabled;

            if (!txtNumber.IsEnabled)
            {
                TransactionViewModel.Number = 1;
            }
        }

        private void TransactionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = TransactionViewModel;

            TransactionViewModel.MainViewObservable = this;

            TransactionViewModel.GetTransactionsViewObservable = createGetTransactionsWindow();

            fillFieldsTextDefaultValues();
        }

        /// <summary>
        /// Método utilizado apenas para agilizar nos testes da Window.
        /// </summary>
        private void fillFieldsTextDefaultValues()
        {
            Random r = new Random();

            TransactionViewModel.CardNumber = 9874563124569;
            TransactionViewModel.Amount = Math.Round(r.NextDouble() + r.Next(1, 100), 2);
            TransactionViewModel.Number = 1;
            TransactionViewModel.Password = "654321";

            txtPassword.Password = TransactionViewModel.Password;
        }

        private GetTransactionsWindow createGetTransactionsWindow()
        {
            var getTransactionsWindow = new GetTransactionsWindow(TransactionViewModel);
            getTransactionsWindow.Owner = this;

            return getTransactionsWindow;
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
