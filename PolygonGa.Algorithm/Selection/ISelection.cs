using System.Collections.Generic;

namespace PolygonGa.Algorithm.Selection
{
    public interface ISelection
    {
        Chromosome Apply(List<Chromosome> population);
    }
}
