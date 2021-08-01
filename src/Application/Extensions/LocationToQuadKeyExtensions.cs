using Application.Models;

namespace Application.Extensions
{
    public static class LocationToQuadKeyExtensions
    {
        public static string LocationToQuadKey(this LightningStrikeObjectModel model)
        {
            TileSystem.LocationToPixel(model.Latitude, model.Longitude, 12, out var pixelX, out var pixelY);
            TileSystem.PixelToTile(pixelX, pixelY, out var tileX, out var tileY);
            return TileSystem.TileToQuadKey(tileX, tileY, 12);
        }
    }
}