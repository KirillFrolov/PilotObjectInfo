using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;

namespace PilotObjectInfo.Converters
{
    /// <summary>
    /// Converts SVG byte[] data to DrawingImage using SharpVectors
    /// </summary>
    public class SvgBytesToImageConverter : IValueConverter
    {
        private static readonly WpfDrawingSettings Settings = new WpfDrawingSettings
        {
            IncludeRuntime = false,
            TextAsGeometry = true
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] bytes && bytes.Length > 0)
            {
                try
                {
                    using (var stream = new MemoryStream(bytes))
                    {
                        var reader = new FileSvgReader(Settings);
                        var drawing = reader.Read(stream);
                        
                        if (drawing != null)
                        {
                            var image = new DrawingImage(drawing);
                            image.Freeze();
                            return image;
                        }
                    }
                }
                catch
                {
                    // Ignore conversion errors
                }
            }
            
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

