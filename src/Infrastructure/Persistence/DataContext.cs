using Application.Persistence;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<AssetModel> Assets { get; set; }
        public DbSet<LightningStrikeModel> LightningStrikes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<AssetModel>(entity => entity.HasKey(t => t.AssetId))
                // .Entity<LightningStrikeModel>(entity => entity.HasKey(t => new {t.StrikedAt, t.Latitude, t.Longitude}))
                .Entity<LightningStrikeModel>(entity => entity.HasKey(t => t.LightningStrikeId))
                ;
        }
    }
}