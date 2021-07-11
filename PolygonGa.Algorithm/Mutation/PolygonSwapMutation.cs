using MersenneTwister;

namespace PolygonGa.Algorithm.Mutation
{
    public class PolygonSwapMutation : IMutation
    {
        public void Apply(Chromosome chromosome)
        {
            for (var k = 0; k < chromosome.Polygons.Count * 0.1; k++)
            {
                var i = Randoms.Next(chromosome.Polygons.Count);
                var j = Randoms.Next(chromosome.Polygons.Count);

                var tmp = chromosome.Polygons[i];
                chromosome.Polygons[i] = chromosome.Polygons[j];
                chromosome.Polygons[j] = tmp;
            }
        }
    }
}
