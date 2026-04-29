using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;

namespace PilotObjectInfo.Converters
{
    /// <summary>
    /// Converts SVG byte data to WPF DrawingImage using SharpVectors library
    /// Provides full SVG 1.1 support
    /// </summary>
    public class SvgToDrawingConverter : IValueConverter
    {
        private readonly WpfDrawingSettings _settings;

        public SvgToDrawingConverter()
        {
            _settings = new WpfDrawingSettings
            {
                IncludeRuntime = false,
                TextAsGeometry = true,
                OptimizePath = true
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                Debug.WriteLine("SvgToDrawingConverter: value is null");
                return null;
            }

            var bytes = value as byte[];
            if (bytes == null || bytes.Length == 0)
            {
                Debug.WriteLine("SvgToDrawingConverter: no bytes");
                return null;
            }

            try
            {
                Debug.WriteLine($"SvgToDrawingConverter: Processing SVG ({bytes.Length} bytes)");

                using (var stream = new MemoryStream(bytes))
                {
                    var converter = new FileSvgReader(_settings);
                    var drawingGroup = converter.Read(stream);

                    if (drawingGroup == null || drawingGroup.Children.Count == 0)
                    {
                        Debug.WriteLine("SvgToDrawingConverter: No drawable content");
                        return null;
                    }

                    var drawingImage = new DrawingImage(drawingGroup);
                    drawingImage.Freeze();

                    Debug.WriteLine($"SvgToDrawingConverter: Successfully converted SVG with {drawingGroup.Children.Count} elements");
                    return drawingImage;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Error - {ex.Message}");
                Debug.WriteLine($"SvgToDrawingConverter: Stack trace - {ex.StackTrace}");
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

