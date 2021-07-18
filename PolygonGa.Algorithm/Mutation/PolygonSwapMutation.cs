using MersenneTwister;

namespace PolygonGa.Algorithm.Mutation
{
    public class PolygonSwapMutation : IMutation
    {
        public void Apply(Chromosome chromosome)
        {
            var i = Randoms.Next(chromosome.Polygons.Count);
            var j = Randoms.Next(chromosome.Polygons.Count);

            var tmp = chromosome.Polygons[i];
            chromosome.Polygons[i] = chromosome.Polygons[j];
            chromosome.Polygons[j] = tmp;
        }
    }
}
