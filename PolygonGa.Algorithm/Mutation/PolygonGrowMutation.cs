using System.Drawing.Drawing2D;
using MersenneTwister;

namespace PolygonGa.Algorithm.Mutation
{
    public class PolygonGrowMutation : IMutation
    {
        private const double ScaleFactor = 1.0;

        private readonly Matrix _matrix;

        public PolygonGrowMutation()
        {
            _matrix = new Matrix();
        }

        public void Apply(Chromosome chromosome)
        {
            _matrix.Reset();

            var i = Randoms.Next(chromosome.Polygons.Count);
            var polygon = chromosome.Polygons[i];

            _matrix.Scale(1 + (float) (Randoms.NextDouble() * ScaleFactor),
                1 + (float) (Randoms.NextDouble() * ScaleFactor));
            _matrix.TransformPoints(polygon.Points);
        }
    }
}
