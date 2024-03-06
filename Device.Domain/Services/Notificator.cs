using Device.Domain.Interfaces;
using Device.Domain.Models;

namespace Device.Domain.Services
{
    public class Notificator : INotificator
    {
        private readonly List<Notification> _errorsNotification;

        public Notificator()
        {
            _errorsNotification = new List<Notification>();
        }


        public void HandleError(Notification notification)
        {
            _errorsNotification.Add(notification);
        }

        public IEnumerable<Notification> GetErrorNotifications()
        {
            return _errorsNotification;
        }
    }
}
