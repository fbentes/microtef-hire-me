namespace StonePayments.Client.Util
{
    public interface IViewObservable
    {
        void StartProcess();
        void EndProcess();

        void SendResultMessage(string message, string caption = "");
    }
}