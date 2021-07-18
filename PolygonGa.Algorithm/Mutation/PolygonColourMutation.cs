using MersenneTwister;

namespace PolygonGa.Algorithm.Mutation
{
    public class PolygonColourMutation : IMutation
    {
        public void Apply(Chromosome chromosome)
        {
            var i = Randoms.Next(chromosome.Polygons.Count);
            chromosome.Polygons[i].Rgb = new[]
            {
                (int) (Randoms.NextDouble() * 255),
                (int) (Randoms.NextDouble() * 255),
                (int) (Randoms.NextDouble() * 255),
            };
        }
    }
}
