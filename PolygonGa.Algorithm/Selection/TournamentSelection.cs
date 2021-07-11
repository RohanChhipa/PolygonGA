using System.Collections.Generic;
using System.Linq;
using MersenneTwister;

namespace PolygonGa.Algorithm.Selection
{
    public class TournamentSelection
    {
        private readonly int _tournamentSize;

        public TournamentSelection(int tournamentSize)
        {
            _tournamentSize = tournamentSize;
        }

        public Chromosome Apply(List<Chromosome> population)
        {
            var chromosome = population.First();
            for (var k = 0; k < _tournamentSize; k++)
            {
                var i = Randoms.Next(population.Count);
                chromosome = population[i].Fitness < chromosome.Fitness
                    ? population[i]
                    : chromosome;
            }

            return chromosome;
        }
    }
}
