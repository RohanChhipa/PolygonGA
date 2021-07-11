using System.Linq;
using MersenneTwister;

namespace PolygonGa.Algorithm.Crossover
{
    public class TwoPointCrossover : ICrossover
    {
        public (Chromosome, Chromosome) Apply(Chromosome parentA, Chromosome parentB)
        {
            var pointA = Randoms.Next(parentA.Polygons.Count);
            var pointB = Randoms.Next(parentA.Polygons.Count - pointA) + pointA;

            var childAPolygons = parentA.Polygons
                .Select(polygon => new ImagePolygon(polygon))
                .ToList();

            for (var k = pointA; k <= pointB; k++)
                childAPolygons[k] = new ImagePolygon(parentB.Polygons[k]);

            var childBPolygons = parentB.Polygons
                .Select(polygon => new ImagePolygon(polygon))
                .ToList();
            
            for (var k = pointA; k <= pointB; k++)
                childBPolygons[k] = new ImagePolygon(parentA.Polygons[k]);

            var childA = new Chromosome(childAPolygons);
            var childB = new Chromosome(childBPolygons);

            return (childA, childB);
        }
    }
}
