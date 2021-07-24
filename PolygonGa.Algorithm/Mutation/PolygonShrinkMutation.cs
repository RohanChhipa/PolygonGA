using System.Drawing.Drawing2D;
using MersenneTwister;

namespace PolygonGa.Algorithm.Mutation
{
    public class PolygonShrinkMutation : IMutation
    {
        private const float ScaleFactor = 0.8f;

        private readonly Matrix _matrix;

        public PolygonShrinkMutation()
        {
            _matrix = new Matrix();
        }

        public void Apply(Chromosome chromosome)
        {
            _matrix.Reset();

            var i = Randoms.Next(chromosome.Polygons.Count);
            var polygon = chromosome.Polygons[i];

            _matrix.Scale(ScaleFactor, ScaleFactor);
            _matrix.TransformPoints(polygon.Points);
        }
    }
}
