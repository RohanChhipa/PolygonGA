using System.Drawing.Drawing2D;
using MersenneTwister;

namespace PolygonGa.Algorithm.Mutation
{
    public class PolygonShearMutationMutation : IMutation
    {
        private const float ScaleFactor = 0.3f;

        private readonly Matrix _matrix;

        public PolygonShearMutationMutation()
        {
            _matrix = new Matrix();
        }

        public void Apply(Chromosome chromosome)
        {
            _matrix.Reset();

            var i = Randoms.Next(chromosome.Polygons.Count);
            var polygon = chromosome.Polygons[i];

            var xShear = (float) (Randoms.NextDouble() * ScaleFactor);
            var yShear = (float) (Randoms.NextDouble() * ScaleFactor);

            _matrix.Shear(xShear, yShear);
            _matrix.TransformPoints(polygon.Points);
        }
    }
}
