using LightInject;
using StonePayments.Business;
using StonePayments.Client.ViewModels;
using StonePayments.Client.Views;
using System.Windows;

namespace StonePayments.Client
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
            var container = new ServiceContainer();

            container.EnableAnnotatedPropertyInjection();

            container.Register<ITransactionModel, TransactionModel>();
            container.Register<ISendTransactionViewModel, SendTransactionViewModel>();
            container.Register<ISendTransactionWindow, SendTransactionWindow>();

            ISendTransactionWindow sendTransactionWindow = container.GetInstance<ISendTransactionWindow>();
            sendTransactionWindow.Show();
        }
    }
}