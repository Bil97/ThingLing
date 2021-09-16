using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ThingLing.ImageResizer
{
    public class Resizer
    {
        /// <summary>
        /// Changes the size of an image located on the physical disk.
        /// </summary>
        /// <param name="inputFileName">The path to the image</param>
        /// <param name="outputFileName">The name of the resized image</param>
        /// <param name="width">Width of the resized image</param>
        /// <param name="height">Height of the resized image</param>
        public static void ResizeImageFromFile(string inputFileName, string outputFileName, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            var image = Image.FromFile(inputFileName);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
            destImage.Save(outputFileName);
        }

        /// <summary>
        /// Changes the size of an image located in a stream.
        /// </summary>
        /// <param name="inputStream">The stream containing the image</param>
        /// <param name="outputFileName">The name of the resized image</param>
        /// <param name="width">Width of the resized image</param>
        /// <param name="height">Height of the resized image</param>

        public static void ResizeImageFromStream(Stream inputStream, string outputFileName, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            var image = Image.FromStream(inputStream);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
            destImage.Save(outputFileName);
        }
    }
}
