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
                Generations = 300,
                PopulationSize = 50,
                PolygonCap = 100,
                PointCap = 6,
                TournamentSize = 5,
                MutationRate = 0.15,
                TargetImage = "Images/polygon.png"
            }, logger);
            await runner.Run();
        }
    }
}
