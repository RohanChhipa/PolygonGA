using System;
using System.Linq;
using MersenneTwister;

namespace PolygonGa.Algorithm.Crossover
{
    public class SinglePointCrossover
    {
        public (Chromosome, Chromosome) Apply(Chromosome parentA, Chromosome parentB)
        {
            var point = Randoms.Next(Math.Min(parentA.Polygons.Count, parentB.Polygons.Count));

            var childAPolygons = parentA.Polygons.Take(point)
                .Concat(parentB.Polygons.Skip(point))
                .Select(polygon => new ImagePolygon(polygon))
                .ToList();

            var childBPolygons = parentB.Polygons.Take(point)
                .Concat(parentA.Polygons.Skip(point))
                .Select(polygon => new ImagePolygon(polygon))
                .ToList();

            var childA = new Chromosome(childAPolygons);
            var childB = new Chromosome(childBPolygons);

            return (childA, childB);
        }
    }
}
