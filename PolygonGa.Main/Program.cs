using System;
using System.Drawing;

namespace PolygonGa.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var bitmap = new Bitmap(800, 600);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Aqua);

            bitmap.Save("mlem.png");

            Console.WriteLine("Hello World!");
        }
    }
}