using System.Threading.Tasks;
using PolygonGa.Algorithm;
using Serilog;

namespace PolygonGa.Console
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            using var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var runner = new AlgorithmRunner(new GaParameters
            {
                ExecutionId = "starry_night",
                Generations = 2500,
                PopulationSize = 100,
                PolygonCap = 1000,
                PointCap = 3,
                TournamentSize = 10,
                MutationRate = 0.10,
                TargetImage = "Images/starry_night.jpg"
            }, logger);
            await runner.Run();
        }
    }
}
