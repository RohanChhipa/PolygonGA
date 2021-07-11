using System;
using System.Drawing;

namespace PolygonGa.Algorithm.Fitness
{
    public class MseFitnessFunction
    {
        private int[][] TargetImage { get; }

        public MseFitnessFunction(Image image)
        {
            var bitmap = new Bitmap(image);
            TargetImage = new int[bitmap.Height][];
            for (var k = 0; k < bitmap.Height; k++)
            {
                TargetImage[k] = new int[bitmap.Width];
                for (var j = 0; j < bitmap.Width; j++)
                {
                    TargetImage[k][j] = GetColourAverage(bitmap.GetPixel(j, k));
                }
            }
        }

        public double Calculate(Bitmap image)
        {
            var sum = 0d;
            for (var k = 0; k < image.Height; k++)
            {
                for (var j = 0; j < image.Width; j++)
                {
                    var colour = GetColourAverage(image.GetPixel(j, k));
                    sum += Math.Pow(TargetImage[k][j] - colour, 2);
                }
            }

            return sum / (image.Height * image.Width);
        }

        private int GetColourAverage(Color pixel)
        {
            return (pixel.R + pixel.G + pixel.B) / 3;
        }
    }
}
