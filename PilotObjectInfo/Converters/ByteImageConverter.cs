using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PilotObjectInfo.Converters
{
    public class ByteImageConverter : IValueConverter
    {
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
                // Check if this is SVG data (starts with '<' or '<?xml')
                if (bytes.Length > 0 && (bytes[0] == '<' || (bytes.Length > 5 && bytes[0] == 0x3C)))
                {
                    Debug.WriteLine("ByteImageConverter: Detected SVG format - not supported yet");
                    // SVG rendering requires additional libraries
                    return null;
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

