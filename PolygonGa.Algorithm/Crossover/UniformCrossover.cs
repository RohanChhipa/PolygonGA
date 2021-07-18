using System.Linq;

namespace PolygonGa.Algorithm.Crossover
{
    public class UniformCrossover : ICrossover
    {
        public (Chromosome, Chromosome) Apply(Chromosome parentA, Chromosome parentB)
        {
            var childAPolygons = parentA.Polygons
                .Select(polygon => new ImagePolygon(polygon))
                .ToList();

            var childBPolygons = parentB.Polygons
                .Select(polygon => new ImagePolygon(polygon))
                .ToList();

            for (var i = 0; i < childAPolygons.Count; i++)
            {
                var tmp = childAPolygons[i];
                childAPolygons[i] = childBPolygons[i];
                childBPolygons[i] = tmp;
            }

            var childA = new Chromosome(childAPolygons);
            var childB = new Chromosome(childBPolygons);

            return (childA, childB);
        }
    }
}
