using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PolygonGa.Algorithm.PolygonGenerator;

namespace PolygonGa.Algorithm
{
    public class Chromosome
    {
        public List<ImagePolygon> Polygons { get; }

        public double Fitness { get; set; }

        public Chromosome(Size imageSize, int maxPolygons, int maxPoints)
        {
            var polygonGenerator = new SquarePolygonGenerator();

            Fitness = double.MaxValue;
            Polygons = Enumerable.Range(0, maxPolygons)
                .Select(_ => polygonGenerator.Apply(maxPoints, imageSize))
                .ToList();
        }

        public Chromosome(List<ImagePolygon> polygons)
        {
            Fitness = double.MaxValue;
            Polygons = polygons;
        }
    }
}
