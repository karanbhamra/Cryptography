using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;

namespace Steganography
{
    public class TextEmbedder
    {
        public static string ReadEmbeddedTextFromImage(Bitmap image)
        {
            StringBuilder stringBuilder = new StringBuilder();

            bool exit = false;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var temppix = image.GetPixel(x, y);
                    
                    // process the pixel here, if its a null terminator, exit the processing
                    char letter = ReadValueFromPixel(temppix);

                    if (letter == '\0')
                    {
                        exit = true;
                        break;
                    }

                    stringBuilder.Append(letter);
                }

                if (exit)
                {
                    break;
                }
            }


            return stringBuilder.ToString();
        }

        private static char ReadValueFromPixel(Color temppix)
        {
            // if the character is the null terminator aka '\0' argb (0,0,0,255), we are done
            if (temppix.ToArgb() == Color.Black.ToArgb())
            {
                return '\0';
            }
            
            var binRed = new List<char>(BaseConverter.DecimalToBinary(temppix.R));
            var binGreen = new List<char>(BaseConverter.DecimalToBinary(temppix.G));
            var binBlue = new List<char>(BaseConverter.DecimalToBinary(temppix.B));
            var binAlpha = new List<char>(BaseConverter.DecimalToBinary(temppix.A));

            var list = new List<List<char>>() {binRed, binGreen, binBlue, binAlpha};

            char letter = ReadCharFromBinary(list);

            return letter;
        }

        private static char ReadCharFromBinary(List<List<char>> list)
        {
            StringBuilder sb = new StringBuilder();

            int numBitsToRead = 2;

            foreach (var indvList in list)
            {
                sb.Append(indvList[^2]); // read second to last
                sb.Append(indvList[^1]); // read last
            }

            return (char) (BaseConverter.BinaryToDecimal(sb.ToString()));
        }

        public static Bitmap EmbedTextInExistingBitmap(string message, Bitmap existingImage,
            string outfilename = "modifiedImage.jpg")
        {
            Bitmap newImage = (Bitmap) existingImage.Clone();

            List<char> lettersToEncode = new List<char>(message);
            lettersToEncode.Add('\0');

            List<Color> newpixels = new List<Color>();
            var pixelsBeingReplaced = GetNPixelsFromBitmap(existingImage, lettersToEncode.Count);
            int count = 0;

            // TODO:
            // get the N amount of pixels we need to replace from the copy image, <--done
            // modify those pixels <--- done
            // insert those pixels back into the copy image (only touch as many as needed to reduce the writing)
            // return the image

            foreach (var letter in lettersToEncode)
            {
                var newPixel = CharToPixel(pixelsBeingReplaced[count], letter);
                count++;

                newpixels.Add(newPixel);
            }


            // now we replace the pixels in the copy image
            int replaced = 0;
            bool done = false;
            for (int y = 0; y < newImage.Height; y++)
            {
                for (int x = 0; x < newImage.Width; x++)
                {
                    if (replaced < newpixels.Count)
                    {
                        newImage.SetPixel(x, y, newpixels[replaced]);
                        replaced++;
                    }
                    else if (done)
                    {
                        break;
                    }
                    else
                    {
                        done = true;
                    }
                }

                if (done)
                {
                    break;
                }
            }

            return newImage;
        }

        private static List<Color> GetNPixelsFromBitmap(Bitmap image, int n)
        {
            List<Color> pixels = new List<Color>();

            int count = 0;
            bool exitEarly = false;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (count < n)
                    {
                        pixels.Add(image.GetPixel(x, y));
                        count++;
                    }
                    else if (exitEarly)
                    {
                        break;
                    }
                    else
                    {
                        exitEarly = true;
                    }
                }

                if (exitEarly)
                {
                    break;
                }
            }

            return pixels;
        }

        // Take original pixel values (r,g,b,a) and take 2 least sig bits from each value to represent char 
        private static Color CharToPixel(Color orig, char letter)
        {
            Color pixel;

            var red = new List<char>(BaseConverter.DecimalToBinary(orig.R));
            var green = new List<char>(BaseConverter.DecimalToBinary(orig.G));
            var blue = new List<char>(BaseConverter.DecimalToBinary(orig.B));
            var alpha = new List<char>(BaseConverter.DecimalToBinary(orig.A));

            var letterBinary = new List<char>(BaseConverter.DecimalToBinary((int) letter));

            var list = new List<List<char>>() {red, green, blue, alpha};

            pixel = EmbedInfoInPixel(list, letterBinary);

            return pixel;
        }

        private static Color EmbedInfoInPixel(List<List<char>> rgbBinary, List<char> letterBinary)
        {
            // take 2 bits at a time from letterBinary and insert it into the rbg list
            int bitsToReplace = 2;

            int currentList = 0;
            for (int i = 0; i < letterBinary.Count; i += bitsToReplace)
            {
                var letter = letterBinary[i];
                var nextLetter = letterBinary[i + 1];

                rgbBinary[currentList][letterBinary.Count - 2] = letter;
                rgbBinary[currentList][letterBinary.Count - 1] = nextLetter;
                currentList++;
                ;
            }

            var vals = new List<int>();

            foreach (var list in rgbBinary)
            {
                string temp = list.EnumerableToString<char>();
                vals.Add(BaseConverter.BinaryToDecimal(temp));
            }

            ;
            return Color.FromArgb(vals[3], vals[0], vals[1], vals[2]); // a,r,g,b
        }
    }
}