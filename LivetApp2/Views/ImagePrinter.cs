using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    internal class ImagePrinter
    {
        private System.Windows.Controls.Image _imageControl;

        public ImagePrinter(System.Windows.Controls.Image imageControl)
        {
            _imageControl = imageControl;
        }

        public void Print(bool[,] values)
        {
            // 情報源(変更・修正時に参考にして)：
            // https://learn.microsoft.com/ja-jp/dotnet/desktop/wpf/graphics-multimedia/how-to-create-a-new-bitmapsource?view=netframeworkdesktop-4.8

            var gridImage = new GridImageBinary(values);

            // Define parameters used to create the BitmapSource.
            PixelFormat pf = PixelFormats.Gray8;
            int width = gridImage.Width;
            int height = gridImage.Height;
            int rawStride = width;
            byte[] rawImage = gridImage.GetImage();

            // Create a BitmapSource.
            BitmapSource bitmap = BitmapSource.Create(width, height,
                96, 96, pf, null,
                rawImage, rawStride);

            // Set image source.
            _imageControl.Source = bitmap;
        }
    }
}
