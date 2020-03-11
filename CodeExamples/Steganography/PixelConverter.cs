using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace Steganography
{
    public class PixelConverter
    {
        public static string DecodeBitmapToString(Bitmap image)
        {
            if (image == null)
            {
                throw new NullReferenceException("No valid image selected.");
            }

            StringBuilder output = new StringBuilder();

            List<Color> pixels = new List<Color>();

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    pixels.Add(image.GetPixel(x, y));
                }
            }

            foreach (var pixel in pixels)
            {
                var vals = GetPixelValues(pixel);

                vals.ForEach((x) =>
                {
                    if (x == 0)
                    {
                        return;
                    }
                    char c = (char)x;
                    output.Append(c);
                });
            }

            return output.ToString();
        }

        public static List<int> GetPixelValues(Color pixel)
        {
            List<int> values = new List<int>();

            values.Add(pixel.R);
            values.Add(pixel.G);
            values.Add(pixel.B);

            return values;

        }

        public static Bitmap EncodeMessageToBitmap(string message)
        {
            int partsPerPixel = 3;
            List<int> asciivalues = message.Select(x => (int)x).ToList();
            int totalPixelsNeeded = (int)Math.Ceiling((double)asciivalues.Count / partsPerPixel);


            while (asciivalues.Count / partsPerPixel < totalPixelsNeeded)
            {
                asciivalues.Add(0);
            }

            List<Color> pixels = new List<Color>();

            for (int i = 0; i < asciivalues.Count; i += 3)
            {
                Color temp = Color.FromArgb(asciivalues[i], asciivalues[i + 1], asciivalues[i + 2]);

                pixels.Add(temp);
            }

            int width = 0;
            int height = 0;

            if (!IsPerfectSquare(pixels.Count))
            {
                int nextPerfectSquare = GetNextPerfectSquareValue(pixels.Count);
                width = (int)Math.Sqrt(nextPerfectSquare);
                height = width;
            }
            else
            {
                width = (int)Math.Sqrt(pixels.Count);
                height = width;
            }

            while (pixels.Count < Math.Pow(width, 2))
            {
                pixels.Add(Color.FromArgb(0, 0, 0));
            }


            int count = 0;
            Bitmap image = new Bitmap(width, height);

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    image.SetPixel(col, row, pixels[count]);

                    count++;
                }
            }

            return image;
        }

        private static int GetNextPerfectSquareValue(int count)
        {

            for (int i = count; i < int.MaxValue; i++)
            {
                if (IsPerfectSquare(i))
                {
                    return i;
                }
            }

            return count;
        }

        public static bool IsPerfectSquare(int val)
        {
            if (val < 1)
            {
                return false;
            }

            int sqrt = (int)Math.Sqrt(val);

            if (sqrt * sqrt == val)
            {
                return true;
            }

            return false;

        }
    }
}
