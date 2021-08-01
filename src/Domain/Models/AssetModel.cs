using System.Text.Json;
using Domain.Models.Base;

namespace Domain.Models
{
    public class AssetModel : ModelBase
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string AssetOwner { get; set; }
        public string QuadKey { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}