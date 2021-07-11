namespace PolygonGa.Algorithm.Crossover
{
    public interface ICrossover
    {
        (Chromosome, Chromosome) Apply(Chromosome parentA, Chromosome parentB);
    }
}
