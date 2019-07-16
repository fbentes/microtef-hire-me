using System.Windows;

namespace StonePayments.ClientWPF.Views
{
    /// <summary>
    /// Interaction logic for MessageWaitingProcess.xaml
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
