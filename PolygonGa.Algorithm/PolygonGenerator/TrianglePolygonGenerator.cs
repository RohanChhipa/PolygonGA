using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MersenneTwister;
using PolygonGa.Algorithm.Extensions;

namespace PolygonGa.Algorithm.PolygonGenerator
{
    public class TrianglePolygonGenerator : IPolygonGenerator
    {
        private readonly Matrix _matrix;
        private const double SquareScale = 0.15;

        public TrianglePolygonGenerator()
        {
            _matrix = new Matrix();
        }
        
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
            
            _matrix.Reset();
            _matrix.RotateAt((float) (Randoms.NextDouble() * 360), points.GetMidPoint());
            if (Randoms.NextDouble() < 0.5)
                _matrix.Shear((float) Randoms.NextDouble(), 0);

            if (Randoms.NextDouble() < 0.5)
                _matrix.Shear(0, (float) Randoms.NextDouble());

            _matrix.TransformPoints(points);
            

            return new ImagePolygon
            {
                Rgb = rgb,
                Points = points
            };
        }
    }
}
