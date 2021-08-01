using System.Collections.Generic;
using System.Text.Json;
using Application.Models;
using Application.Request;

namespace Application.Flows.Assets.Queries
{
    public class ListAssetsQuery : Request<ICollection<AssetObjectModel>>
    {
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}