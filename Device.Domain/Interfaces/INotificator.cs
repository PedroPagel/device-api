using Device.Domain.Models;

namespace Device.Domain.Interfaces
{
    public interface INotificator
    {
        void HandleError(Notification notification);
        IEnumerable<Notification> GetErrorNotifications();
    }
}
