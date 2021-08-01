using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence
{
    public interface IDataContext
    {
        DbSet<AssetModel> Assets { get; set; }
        DbSet<LightningStrikeModel> LightningStrikes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}