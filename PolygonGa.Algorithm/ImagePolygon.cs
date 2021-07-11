using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PolygonGa.Algorithm
{
    public class ImagePolygon
    {
        public int[] Rgb { get; set; }

        public List<Point> Points { get; set; }

        public ImagePolygon()
        {
        }

        public ImagePolygon(ImagePolygon imagePolygon)
        {
            Rgb = imagePolygon.Rgb.Select(i => i).ToArray();
            Points = imagePolygon.Points.Select(point => new Point(point.X, point.Y)).ToList();
        }
    }
}
