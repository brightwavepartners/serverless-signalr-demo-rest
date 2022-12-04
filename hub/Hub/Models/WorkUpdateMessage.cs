namespace SignalRService.Models
{
    public class WorkUpdateMessage
    {
        #region Properties

        public int Value { get; }

        #endregion

        #region Constructors

        public WorkUpdateMessage(int value)
        {
            Value = value;
        }

        #endregion
    }
}
