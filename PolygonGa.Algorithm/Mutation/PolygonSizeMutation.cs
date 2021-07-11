using System.Drawing;
using MersenneTwister;

namespace PolygonGa.Algorithm.Mutation
{
    public class PolygonSizeMutation : IMutation
    {
        public void Apply(Chromosome chromosome)
        {
            foreach (var imagePolygon in chromosome.Polygons)
            {
                if (Randoms.NextDouble() > 0.2)
                    continue;
                
                var points = imagePolygon.Points;
                for (var k = 0; k < points.Count; k++)
                {
                    var point = points[k];
                    points[k] = new Point(point.X / 2, point.Y / 2);
                    // if (Randoms.NextDouble() <= 0.5)
                        // point.X /= 2;

                    // if (Randoms.NextDouble() <= 0.5)
                        // point.Y /= 2;
                }
            }
        }
    }
}
