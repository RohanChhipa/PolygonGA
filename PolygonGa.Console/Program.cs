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

            var runner = new AlgorithmRunner(logger);
            await runner.Run("Images/polygon.png");
        }
    }
}
