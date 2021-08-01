using System.Threading;
using System.Threading.Tasks;
using Application.Events.Base;
using Application.Services;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class EventDispatcherService : IEventDispatcherService
    {
        private readonly ILogger<EventDispatcherService> _logger;

        public EventDispatcherService(ILogger<EventDispatcherService> logger)
        {
            _logger = logger;
        }

        public async Task Dispatch(EventBase @event, CancellationToken cancellationToken)
        {
            if (@event == null)
            {
                _logger.LogError("Event is null.");
                return;
            }

            _logger.LogDebug("Event is dispatched. Current event name: {Name}. Event data: {evt}", @event.GetType().Name, @event);

            await Task.CompletedTask;
        }
    }
}