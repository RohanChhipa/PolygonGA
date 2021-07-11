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
                Generations = 50,
                PopulationSize = 10,
                PolygonCap = 50,
                PointCap = 10,
                TournamentSize = 5,
                TargetImage = "Images/polygon.png"
            }, logger);
            await runner.Run();
        }
    }
}
