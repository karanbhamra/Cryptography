using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Steganography
{
    public class ImageHider
    {
        private static string nibble = "0000";

        public static Bitmap HideImage(Bitmap firstImage, Bitmap secondImage)
        {
            if (firstImage.Width != secondImage.Width || firstImage.Height != secondImage.Height)
            {
                throw new Exception("Images must have same dimensions.");
            }

            var pixelsOne = GetPixelsFromBitmap(firstImage);
            var pixelsTwo = GetPixelsFromBitmap(secondImage);

            var finalPixels = CombinePixels(pixelsOne, pixelsTwo);

            return new Bitmap(BitmapFromPixels(finalPixels, firstImage.Width, firstImage.Height));
        }

        private static List<Color> CombinePixels(List<Color> pixelsOne, List<Color> pixelsTwo)
        {
            var finalPixels = new List<Color>();

            for (int i = 0; i < pixelsOne.Count; i++)
            {
                var first = pixelsOne[i];
                var second = pixelsTwo[i];

                var final = SpliceAndCombinePixels(first, second);
                finalPixels.Add(final);
            }

            return finalPixels;
        }

        private static Color SpliceAndCombinePixels(Color first, Color second)
        {
            var firstRed = BaseConverter.DecimalToBinary(first.R);
            var firstGreen = BaseConverter.DecimalToBinary(first.G);
            var firstBlue = BaseConverter.DecimalToBinary(first.B);

            var secondRed = BaseConverter.DecimalToBinary(second.R);
            var secondGreen = BaseConverter.DecimalToBinary(second.G);
            var secondBlue = BaseConverter.DecimalToBinary(second.B);

            var finalRed = GetSignificantBits(firstRed) + GetSignificantBits(secondRed);
            var finalGreen = GetSignificantBits(firstGreen) + GetSignificantBits(secondGreen);
            var finalBlue = GetSignificantBits(firstBlue) + GetSignificantBits(secondBlue);

            return Color.FromArgb(BaseConverter.BinaryToDecimal(finalRed),
                BaseConverter.BinaryToDecimal(finalGreen),
                BaseConverter.BinaryToDecimal(finalBlue)
            );
        }

        private static string GetLeastSignificantBits(string input)
        {
            return input.Substring(4, 4);
        }

        private static string GetSignificantBits(string input)
        {
            return input.Substring(0, 4);
        }

        private static Bitmap BitmapFromPixels(List<Color> finalPixels, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);

            int read = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bitmap.SetPixel(x, y, finalPixels[read]);
                    read++;
                }
            }

            return bitmap;
        }

        private static List<Color> GetPixelsFromBitmap(Bitmap firstImage)
        {
            var list = new List<Color>();

            for (int y = 0; y < firstImage.Height; y++)
            {
                for (int x = 0; x < firstImage.Width; x++)
                {
                    list.Add(firstImage.GetPixel(x, y));
                }
            }

            return list;
        }

        public static (Bitmap imageOne, Bitmap imageTwo) SeparateImages(Bitmap finalImage)
        {
            var pixels = GetPixelsFromBitmap(finalImage);

            Bitmap imageOne = new Bitmap(finalImage.Width, finalImage.Height);
            Bitmap secondOne = new Bitmap(finalImage.Width, finalImage.Height);

            var imageOnePixels = new List<Color>();
            var imageTwoPixels = new List<Color>();

            foreach (var pixel in pixels)
            {
                var (pixOne, pixTwo) = GetSeperatePixels(pixel);

                imageOnePixels.Add(pixOne);
                imageTwoPixels.Add(pixTwo);
            }

            var bitmapOne = BitmapFromPixels(imageOnePixels, finalImage.Width, finalImage.Height);
            var bitmapTwo = BitmapFromPixels(imageTwoPixels, finalImage.Width, finalImage.Height);

            return (bitmapOne, bitmapTwo);
        }

        private static (Color pixOne, Color pixTwo) GetSeperatePixels(Color pixel)
        {
            var binR = BaseConverter.DecimalToBinary(pixel.R);
            var binG = BaseConverter.DecimalToBinary(pixel.G);
            var binB = BaseConverter.DecimalToBinary(pixel.B);

            var pixOneR = GetSignificantBits(binR) + nibble;
            var pixTwoR = GetLeastSignificantBits(binR) + nibble;

            var pixOneG = GetSignificantBits(binG) + nibble;
            var pixTwoG = GetLeastSignificantBits(binG) + nibble;

            var pixOneB = GetSignificantBits(binB) + nibble;
            var pixTwoB = GetLeastSignificantBits(binB) + nibble;

            Color pixOne = Color.FromArgb(BaseConverter.BinaryToDecimal(pixOneR),
                BaseConverter.BinaryToDecimal(pixOneG),
                BaseConverter.BinaryToDecimal(pixOneB)
            );

            Color pixTwo = Color.FromArgb(BaseConverter.BinaryToDecimal(pixTwoR),
                BaseConverter.BinaryToDecimal(pixTwoG),
                BaseConverter.BinaryToDecimal(pixTwoB)
            );

            return (pixOne, pixTwo);
        }
    }
}