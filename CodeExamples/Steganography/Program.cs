using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using System.IO;

namespace Steganography
{
    class Program
    {
        static void Main(string[] args)
        {

            #region BaseConverter
            //for (int i = 0; i < 128; i++)
            //{
            //    string binary = BaseConverter.DecimalToBinary(i);
            //    int num = BaseConverter.BinaryToDecimal(binary);
            //    string hex = BaseConverter.DecimalToHexadecimal(i);

            //    int hexnu = BaseConverter.HexadecimalToDecimal(hex);

            //    int oct = BaseConverter.DecimalToOctal(i);
            //    int octdec = BaseConverter.OctalToDecimal(oct);
            //    Console.WriteLine($"{i} = {binary} = {num} = {hex} = {hexnu} = {oct} = {octdec}");
            //}

            // text to binary
            //Console.WriteLine("Enter some text");

            //string input = Console.ReadLine();


            //StringBuilder sb = new StringBuilder();

            //List<string> binary = new List<string>();

            //foreach (var letter in input)
            //{
            //    string bin = BaseConverter.DecimalToBinary((int)letter);

            //    while (bin.Length < 8)
            //    {
            //        bin = "0" + bin;
            //    }

            //    binary.Add(bin);

            //}

            //sb.AppendJoin(' ', binary);

            //Console.WriteLine(sb.ToString());


            //// binary to text

            //var binarywords = sb.ToString().Split(' ');

            //foreach (var word in binarywords)
            //{
            //    int ascii = BaseConverter.BinaryToDecimal(word);

            //    char letter = (char)ascii;

            //    Console.Write(letter);
            //}

            //Console.WriteLine();
            #endregion

            #region Text to Pixel Encoder/Decoder
            // Console.WriteLine("Enter a message you would like to encode");
            // var input = Console.ReadLine();
            //
            // var image = PixelConverter.EncodeMessageToBitmap(input);
            //
            // // try resizing the image before saving it, lets see if it works
            //
            // image.Save("output.jpg");
            //
            // //var image = new Bitmap("output.jpg");
            //
            // var decoded = PixelConverter.DecodeBitmapToString(image);
            //
            // Console.WriteLine(decoded);
            #endregion

            #region Text to Existing Image Encoder/Decoder
            // Format will be, #Letters Followed by Message ending with '\0' <---TODO
            // Console.WriteLine("starting replacement");
            // const string message = "this is some example text that will be encoded into a big enough image";
            // var image = new Bitmap("test.jpg");
            // var resultImage = TextEmbedder.EmbedTextInExistingBitmap(message, image);
            // resultImage.Save("modifiedImage.jpg");
            //
            // Console.WriteLine("replacement completed");
            //
            // var decoded = TextEmbedder.ReadEmbeddedTextFromImage(resultImage);
            // Console.WriteLine($"Decoded: {decoded}");
            
            #endregion

            #region HidingImages
            
            var firstImage = new Bitmap("imageOne.jpg");
            var secondImage = new Bitmap("imageTwo.jpg");

            Console.WriteLine("Combining...");
            var finalImage = ImageHider.HideImage(firstImage, secondImage);
            finalImage.Save("finalImage.jpg");
            Console.WriteLine("Finished combining.");

            Console.WriteLine("Separating...");

            var (sepImageOne, sepImageTwo) = ImageHider.SeparateImages(finalImage);
            
            sepImageOne.Save("SepOne.jpg");
            sepImageTwo.Save("SepTwo.jpg");

            Console.WriteLine("Done separating.");
            #endregion
        }
        
    }
}
