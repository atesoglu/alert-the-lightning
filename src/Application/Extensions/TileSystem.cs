using System;
using System.Text;

namespace Application.Extensions
{

    internal static class TileSystem
    {
/*
        private const double EarthRadius = 6378137;
*/
        private const double MinLatitude = -85.05112878;
        private const double MaxLatitude = 85.05112878;
        private const double MinLongitude = -180;
        private const double MaxLongitude = 180;

        /// <summary>  
        /// Clips a number to the specified minimum and maximum values.  
        /// </summary>  
        /// <param name="n">The number to clip.</param>  
        /// <param name="minValue">Minimum allowable value.</param>  
        /// <param name="maxValue">Maximum allowable value.</param>  
        /// <returns>The clipped value.</returns>  
        private static double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }

        /// <summary>  
        /// Determines the map width and height (in pixels) at a specified level  
        /// of detail.  
        /// </summary>  
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)  
        /// to 23 (highest detail).</param>  
        /// <returns>The map width and height in pixels.</returns>  
        public static uint MapSize(int levelOfDetail)
        {
            return (uint) 256 << levelOfDetail;
        }

        /// <summary>  
        /// Converts a point from latitude/longitude WGS-84 coordinates (in degrees)  
        /// into pixel XY coordinates at a specified level of detail.  
        /// </summary>  
        /// <param name="latitude">Latitude of the point, in degrees.</param>  
        /// <param name="longitude">Longitude of the point, in degrees.</param>  
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)  
        /// to 23 (highest detail).</param>  
        /// <param name="pixelX">Output parameter receiving the X coordinate in pixels.</param>  
        /// <param name="pixelY">Output parameter receiving the Y coordinate in pixels.</param>  
        public static void LocationToPixel(double latitude, double longitude, int levelOfDetail, out int pixelX, out int pixelY)
        {
            latitude = Clip(latitude, MinLatitude, MaxLatitude);
            longitude = Clip(longitude, MinLongitude, MaxLongitude);

            var x = (longitude + 180) / 360;
            var sinLatitude = Math.Sin(latitude * Math.PI / 180);
            var y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            var mapSize = MapSize(levelOfDetail);
            pixelX = (int) Clip(x * mapSize + 0.5, 0, mapSize - 1);
            pixelY = (int) Clip(y * mapSize + 0.5, 0, mapSize - 1);
        }

        /// <summary>  
        /// Converts pixel XY coordinates into tile XY coordinates of the tile containing  
        /// the specified pixel.  
        /// </summary>  
        /// <param name="pixelX">Pixel X coordinate.</param>  
        /// <param name="pixelY">Pixel Y coordinate.</param>  
        /// <param name="tileX">Output parameter receiving the tile X coordinate.</param>  
        /// <param name="tileY">Output parameter receiving the tile Y coordinate.</param>  
        public static void PixelToTile(int pixelX, int pixelY, out int tileX, out int tileY)
        {
            tileX = pixelX / 256;
            tileY = pixelY / 256;
        }

        /// <summary>  
        /// Converts tile XY coordinates into a QuadKey at a specified level of detail.  
        /// </summary>  
        /// <param name="tileX">Tile X coordinate.</param>  
        /// <param name="tileY">Tile Y coordinate.</param>  
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)  
        /// to 23 (highest detail).</param>  
        /// <returns>A string containing the QuadKey.</returns>  
        public static string TileToQuadKey(int tileX, int tileY, int levelOfDetail)
        {
            var quadKey = new StringBuilder();
            for (var i = levelOfDetail; i > 0; i--)
            {
                var digit = '0';
                var mask = 1 << (i - 1);
                if ((tileX & mask) != 0)
                {
                    digit++;
                }

                if ((tileY & mask) != 0)
                {
                    digit++;
                    digit++;
                }

                quadKey.Append(digit);
            }

            return quadKey.ToString();
        }
    }
}