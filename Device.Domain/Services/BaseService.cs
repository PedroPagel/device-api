using Device.Domain.Interfaces;
using Device.Domain.Models;

namespace Device.Domain.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        protected BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected void NotifyError(string message)
        {
            _notificator.HandleError(new Notification(message));
        }
    }
}
