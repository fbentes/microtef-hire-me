using System.Windows;

namespace StonePayments.ClientWPF.Views
{
    /// <summary>
    /// Classe responsável para informar espera de processamento para o usuário.
    /// </summary>
    public partial class MessageWaitingProcessWindow : Window
    {
        public string Message
        {
            set
            {
                txtMessage.Text = value;
            }

        }

        public MessageWaitingProcessWindow()
        {
            InitializeComponent();
        }
    }
}
