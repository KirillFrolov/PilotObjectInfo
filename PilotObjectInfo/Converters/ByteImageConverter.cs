using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PilotObjectInfo.Converters
{
    public class ByteImageConverter : IValueConverter
    {
        private readonly SvgToDrawingConverter _svgConverter = new SvgToDrawingConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                Debug.WriteLine("ByteImageConverter: value is null");
                return null;
            }

            var bytes = value as byte[];
            if (bytes == null)
            {
                Debug.WriteLine($"ByteImageConverter: value is not byte[] but {value.GetType().Name}");
                return null;
            }
            
            if (bytes.Length == 0)
            {
                Debug.WriteLine("ByteImageConverter: byte array is empty");
                return null;
            }

            Debug.WriteLine($"ByteImageConverter: Processing {bytes.Length} bytes");

            try
            {
                // Check if this is SVG data
                if (IsSvg(bytes))
                {
                    Debug.WriteLine("ByteImageConverter: Detected SVG format, delegating to SvgConverter");
                    return _svgConverter.Convert(value, targetType, parameter, culture);
                }
                
                // Try to load as regular bitmap image (PNG, JPEG, BMP, etc.)
                var image = new BitmapImage();
                using (var mem = new MemoryStream(bytes))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                Debug.WriteLine($"ByteImageConverter: Successfully loaded image {image.PixelWidth}x{image.PixelHeight}");
                return image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ByteImageConverter: Failed to load image - {ex.Message}");
                return null;
            }
        }

        private bool IsSvg(byte[] bytes)
        {
            if (bytes.Length < 5)
                return false;

            // Check for '<' at start (SVG XML)
            if (bytes[0] == '<' || bytes[0] == 0x3C)
            {
                // Try to parse the beginning as string to verify it's SVG
                try
                {
                    var start = Encoding.UTF8.GetString(bytes, 0, Math.Min(100, bytes.Length));
                    return start.Contains("<svg") || start.Contains("<?xml");
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

