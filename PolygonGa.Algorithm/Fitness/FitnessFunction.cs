using System;
using System.Drawing;

namespace PolygonGa.Algorithm.Fitness
{
    public class FitnessFunction
    {
        private readonly Bitmap _targetImage;

        public FitnessFunction(Image image)
        {
            _targetImage = new Bitmap(image);
        }

        public double Calculate(Bitmap image)
        {
            var sum = 0d;
            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    var targetPixel = _targetImage.GetPixel(x, y);
                    var srcPixel = image.GetPixel(x, y);

                    sum += Math.Abs(targetPixel.R - srcPixel.R)
                           + Math.Abs(targetPixel.G - srcPixel.G)
                           + Math.Abs(targetPixel.B - srcPixel.B);
                }
            }

            return sum;
        }
    }
}
