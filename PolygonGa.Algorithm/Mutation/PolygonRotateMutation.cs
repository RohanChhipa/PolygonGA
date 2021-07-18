using System.Drawing.Drawing2D;
using MersenneTwister;
using PolygonGa.Algorithm.Extensions;

namespace PolygonGa.Algorithm.Mutation
{
    public class PolygonRotateMutation : IMutation
    {
        private readonly Matrix _matrix;

        public PolygonRotateMutation()
        {
            _matrix = new Matrix();
        }

        public void Apply(Chromosome chromosome)
        {
            _matrix.Reset();

            var i = Randoms.Next(chromosome.Polygons.Count);
            var polygon = chromosome.Polygons[i];

            _matrix.Reset();
            _matrix.RotateAt((float) (Randoms.NextDouble() * 360), polygon.Points.GetMidPoint());
            _matrix.TransformPoints(polygon.Points);
        }
    }
}
