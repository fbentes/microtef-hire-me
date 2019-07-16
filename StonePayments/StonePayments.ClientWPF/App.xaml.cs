using LightInject;
using StonePayments.Business;
using StonePayments.Business.ViewModels;
using StonePayments.ClientWPF.Views;
using System.Windows;

namespace StonePayments.ClientWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTIxMzY5QDMxMzcyZTMyMmUzMGFaWHJTZE92ejduTmkydVUwY2ZsTktlSmFtMlRQUE83KzVYS1JPVkd4Mzg9");

            var container = new ServiceContainer();

            container.EnableAnnotatedPropertyInjection();

            container.Register<ITransactionModel, TransactionModel>();
            container.Register<ITransactionViewModel, TransactionViewModel>();
            container.Register<ITransactionWindow, TransactionWindow>();

            ITransactionWindow sendTransactionWindow = container.GetInstance<ITransactionWindow>();
            sendTransactionWindow.Show();
        }
    }
}