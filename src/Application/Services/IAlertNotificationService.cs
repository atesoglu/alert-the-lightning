using System.Threading;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Services
{
    public interface IAlertNotificationService
    {
        Task ProcessAsync(LightningStrikeObjectModel objectModel, CancellationToken cancellationToken);
    }
}