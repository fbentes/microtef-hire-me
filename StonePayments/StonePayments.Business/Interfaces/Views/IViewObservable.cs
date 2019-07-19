namespace StonePayments.Business.Interfaces
{
    public interface IViewObservable : IViewShutdown
    {
        void StartProcess();
        void EndProcess();

        void SendResultMessage(string message, string caption = "");
    }
}