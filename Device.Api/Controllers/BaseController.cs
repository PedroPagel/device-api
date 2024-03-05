using Device.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Device.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotificator _notificator;

        protected BaseController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected ActionResult CustomResponse(object result)
        {
            if (!_notificator.GetErrorNotifications().Any())
            {
                return new OkObjectResult(new
                {
                    success = true,
                    data = result,
                });
            }

            throw new Exception(string.Join(Environment.NewLine, _notificator.GetErrorNotifications().Select(n => n.Message)));
        }
    }
}
