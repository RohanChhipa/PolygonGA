using System;
using System.Collections.Generic;
using System.Drawing;
using MersenneTwister;

namespace PolygonGa.Algorithm.PolygonGenerator
{
    public class RightTrianglePolygonGenerator : IPolygonGenerator
    {
        private const double SquareScale = 0.1;

        public ImagePolygon Apply(int numPoints, Size imageSize)
        {
            var rgb = new[]
            {
                (int) (Randoms.NextDouble() * 255),
                (int) (Randoms.NextDouble() * 255),
                (int) (Randoms.NextDouble() * 255),
            };

            var squareSize = (int) (Math.Max(imageSize.Width, imageSize.Height) * SquareScale);

            var x = (int) (Randoms.NextDouble() * imageSize.Width);
            var y = (int) (Randoms.NextDouble() * imageSize.Height);

            var points = new Point[]
            {
                new(x, y),
                new(x + squareSize, y + squareSize),
                new(x, y + squareSize)
            };

            return new ImagePolygon
            {
                Rgb = rgb,
                Points = points
            };
        }
    }
}
