using System.Text.Json;
using Application.Models;
using Application.Request;

namespace Application.Flows.Assets.Commands
{
    public class CreateAssetCommand : Request<AssetObjectModel>
    {
        public string AssetName { get; set; }
        public string AssetOwner { get; set; }
        public string QuadKey { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}