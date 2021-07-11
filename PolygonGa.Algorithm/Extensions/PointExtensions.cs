using System.Drawing;
using System.Linq;

namespace PolygonGa.Algorithm.Extensions
{
    public static class PointExtensions
    {
        public static PointF GetMidPoint(this Point[] polygon)
        {
            var minX = polygon.Min(point => point.X);
            var maxX = polygon.Max(point => point.X);

            var minY = polygon.Min(point => point.Y);
            var maxY = polygon.Max(point => point.Y);

            var midX = (maxX - minX) / 2.0 + minX;
            var midY = (maxY - minY) / 2.0 + minY;

            return new PointF((float) midX, (float) midY);
        }
    }
}
