using StonePayments.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StonePayments.Client.Views
{
    /// <summary>
    /// Interaction logic for GetTransactions.xaml
    /// </summary>
    public partial class GetTransactionsWindow : Window, IGetTransactionsWindow
    {
        public IGetTransactionsViewModel GetTransactionsViewModel { get; set; }

        public GetTransactionsWindow()
        {
            InitializeComponent();

            GetTransactionsViewModel = new GetTransactionsViewModel();

            Loaded += GetTransactionsWindow_Loaded;
        }

        private void GetTransactionsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
