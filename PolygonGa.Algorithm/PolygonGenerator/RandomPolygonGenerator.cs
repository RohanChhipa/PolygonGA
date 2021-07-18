using System;
using System.Drawing;
using System.Linq;
using MersenneTwister;

namespace PolygonGa.Algorithm.PolygonGenerator
{
    public class RandomPolygonGenerator : IPolygonGenerator
    {
        private const double ScaleOffset = 0.5;

        public ImagePolygon Apply(int numPoints, Size imageSize)
        {
            var widthAdjusted = imageSize.Width * ScaleOffset;
            var heightAdjusted = imageSize.Height * ScaleOffset;

            var widthOffset = Math.Abs((widthAdjusted - imageSize.Width) / 2);
            var heightOffset = Math.Abs((heightAdjusted - imageSize.Height) / 2);

            var imagePolygon = new ImagePolygon
            {
                Rgb = new[]
                {
                    (int) (Randoms.NextDouble() * 255),
                    (int) (Randoms.NextDouble() * 255),
                    (int) (Randoms.NextDouble() * 255),
                    (int) (Randoms.NextDouble() * 255),
                },
                Points = Enumerable.Range(0, numPoints)
                    .Select(_ => new Point
                    {
                        X = (int) (Randoms.NextDouble() * widthAdjusted) +
                            (int) ((Randoms.NextDouble() - 0.3) * imageSize.Width),
                        Y = (int) (Randoms.NextDouble() * heightAdjusted) +
                            (int) ((Randoms.NextDouble() - 0.3) * imageSize.Height)
                    })
                    .ToArray()
            };

            return imagePolygon;
        }
    }
}
