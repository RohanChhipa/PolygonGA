using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MersenneTwister;

namespace PolygonGa.Algorithm
{
    public class Chromosome
    {
        private const double ScaleOffset = 1.5;
        private const int MinPolygonCap = 3;

        public List<ImagePolygon> Polygons { get; }

        public double Fitness { get; set; }

        public Chromosome(Size imageSize, int polygonCap, int pointCap)
        {
            Fitness = double.MaxValue;

            var polygons = (int) (Randoms.NextDouble() * polygonCap);
            var widthAdjusted = imageSize.Width * ScaleOffset;
            var heightAdjusted = imageSize.Height * ScaleOffset;

            var widthOffset = (widthAdjusted - imageSize.Width) / 2;
            var heightOffset = (heightAdjusted - imageSize.Height) / 2;

            Polygons = new List<ImagePolygon>();
            for (var k = 0; k < polygons; k++)
            {
                var points = (int) (Randoms.NextDouble() * pointCap);
                if (points < MinPolygonCap)
                    points = MinPolygonCap;


                var imagePolygon = new ImagePolygon
                {
                    Rgb = new[]
                    {
                        (int) (Randoms.NextDouble() * 255),
                        (int) (Randoms.NextDouble() * 255),
                        (int) (Randoms.NextDouble() * 255),
                    },
                    Points = Enumerable.Range(0, points)
                        .Select(_ => new Point
                        {
                            X = (int) (Randoms.NextDouble() * widthAdjusted - widthOffset),
                            Y = (int) (Randoms.NextDouble() * heightAdjusted - heightOffset)
                        })
                        .ToList()
                };

                Polygons.Add(imagePolygon);
            }
        }

        public Chromosome(List<ImagePolygon> polygons)
        {
            Fitness = double.MaxValue;
            Polygons = polygons;
        }
    }
}
