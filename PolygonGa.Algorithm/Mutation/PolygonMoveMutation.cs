using System.Drawing;
using System.Drawing.Drawing2D;
using MersenneTwister;

namespace PolygonGa.Algorithm.Mutation
{
    public class PolygonMoveMutation : IMutation
    {
        private const float ScaleFactor = 0.6f;

        private readonly Size _size;
        private readonly Matrix _matrix;

        public PolygonMoveMutation(Size size)
        {
            _size = size;
            _matrix = new Matrix();
        }

        public void Apply(Chromosome chromosome)
        {
            _matrix.Reset();

            var i = Randoms.Next(chromosome.Polygons.Count);
            var polygon = chromosome.Polygons[i];

            var xOffset = (float) (Randoms.NextDouble() - 0.5) * (_size.Width * ScaleFactor);
            var yOffset = (float) (Randoms.NextDouble() - 0.5) * (_size.Height * ScaleFactor);

            _matrix.Translate(xOffset, yOffset);
            _matrix.TransformPoints(polygon.Points);
        }
    }
}
