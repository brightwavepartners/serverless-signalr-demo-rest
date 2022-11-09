namespace SignalRService.Models
{
    public class BroadcastMessage
    {
        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        public BroadcastMessage(string message)
        {
            Text = message;
        }

        #endregion
    }
}

