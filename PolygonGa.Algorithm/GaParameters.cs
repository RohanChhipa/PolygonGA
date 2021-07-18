namespace PolygonGa.Algorithm
{
    public class GaParameters
    {
        public string ExecutionId { get; set; }
        public string TargetImage { get; init; }
        public int PolygonCap { get; init; }
        public int PointCap { get; init; }
        public int PopulationSize { get; init; }
        public int Generations { get; init; }
        public int TournamentSize { get; init; }
        public double MutationRate { get; init; }
    }
}
