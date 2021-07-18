using System.Drawing;

namespace PolygonGa.Algorithm.Fitness
{
    public interface IFitnessFunction
    {
        double Calculate(Bitmap image);
    }
}
