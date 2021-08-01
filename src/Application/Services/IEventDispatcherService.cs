using System.Threading;
using System.Threading.Tasks;
using Application.Events.Base;

namespace Application.Services
{
    public interface IEventDispatcherService
    {
        Task Dispatch(EventBase @event, CancellationToken cancellationToken);
    }
}