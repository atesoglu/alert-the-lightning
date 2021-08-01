using System.Text.Json;
using Application.Models.Base;
using Domain.Models;

namespace Application.Models
{
    public class AssetObjectModel : ObjectModelBase<AssetModel>
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string AssetOwner { get; set; }
        public string QuadKey { get; set; }

        public AssetObjectModel()
        {
        }

        public AssetObjectModel(AssetModel entity)
        {
            AssignFrom(entity);
        }

        public sealed override void AssignFrom(AssetModel entity)
        {
            AssetId = entity.AssetId;
            AssetName = entity.AssetName;
            AssetOwner = entity.AssetOwner;
            QuadKey = entity.QuadKey;
        }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}