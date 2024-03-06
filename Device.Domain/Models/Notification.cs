namespace Device.Domain.Models
{
    public class Notification
    {
        public DateTime TimeStamp { get; }
        public string Message { get; }

        public Notification(string message)
        {
            TimeStamp = DateTime.Now;
            Message = message;
        }
    }
}
