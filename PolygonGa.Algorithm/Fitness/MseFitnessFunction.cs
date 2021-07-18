using System;
using System.Drawing;

namespace PolygonGa.Algorithm.Fitness
{
    public class MseFitnessFunction : IFitnessFunction
    {
        private readonly int[][][] _pixels;

        public MseFitnessFunction(Image image)
        {
            var targetImage = new Bitmap(image);

            _pixels = new int[targetImage.Height][][];
            for (var y = 0; y < targetImage.Height; y++)
            {
                _pixels[y] = new int[targetImage.Width][];
                for (var x = 0; x < targetImage.Width; x++)
                {
                    var targetPixel = targetImage.GetPixel(x, y);
                    _pixels[y][x] = new int[]
                    {
                        targetPixel.R,
                        targetPixel.G,
                        targetPixel.B
                    };
                }
            }
        }

        public double Calculate(Bitmap image)
        {
            var sum = 0d;
            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    var targetPixel = _pixels[y][x];
                    var srcPixel = image.GetPixel(x, y);

                    sum += Math.Pow(targetPixel[0] - srcPixel.R, 2)
                           + Math.Pow(targetPixel[1] - srcPixel.G, 2)
                           + Math.Pow(targetPixel[2] - srcPixel.B, 2);

                    if (srcPixel.ToKnownColor() == KnownColor.Black)
                    {
                        sum += Math.Pow(255, 2) * 3;
                    }
                }
            }

            return sum;
        }
    }
}
