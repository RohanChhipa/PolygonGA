using System.Drawing;

namespace PolygonGa.Algorithm.PolygonGenerator
{
    public interface IPolygonGenerator
    {
        ImagePolygon Apply(int numPoints, Size imageSize);
    }
}
