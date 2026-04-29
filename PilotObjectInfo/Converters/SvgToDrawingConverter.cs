using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace PilotObjectInfo.Converters
{
    /// <summary>
    /// Converts SVG byte data to WPF Drawing
    /// Simple SVG parser for basic SVG icons
    /// </summary>
    public class SvgToDrawingConverter : IValueConverter
    {
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
                var svgString = Encoding.UTF8.GetString(bytes);
                Debug.WriteLine($"SvgToDrawingConverter: Processing SVG ({bytes.Length} bytes)");

                // Parse SVG XML
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(svgString);

                var svgElement = xmlDoc.DocumentElement;
                if (svgElement == null || svgElement.Name != "svg")
                {
                    Debug.WriteLine("SvgToDrawingConverter: Not a valid SVG");
                    return null;
                }

                // Extract viewBox or width/height
                double width = 16, height = 16;
                var viewBox = svgElement.GetAttribute("viewBox");
                if (!string.IsNullOrEmpty(viewBox))
                {
                    var parts = viewBox.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 4)
                    {
                        double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out width);
                        double.TryParse(parts[3], NumberStyles.Float, CultureInfo.InvariantCulture, out height);
                    }
                }

                // Create a DrawingGroup to hold all SVG elements
                var drawingGroup = new DrawingGroup();

                // Process SVG elements (simplified - just paths for now)
                ProcessSvgElements(svgElement, drawingGroup);

                if (drawingGroup.Children.Count == 0)
                {
                    Debug.WriteLine("SvgToDrawingConverter: No drawable elements found");
                    return null;
                }

                // Create DrawingImage
                var drawingImage = new DrawingImage(drawingGroup);
                drawingImage.Freeze();

                Debug.WriteLine($"SvgToDrawingConverter: Successfully converted SVG");
                return drawingImage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Error - {ex.Message}");
                return null;
            }
        }

        private void ProcessSvgElements(XmlElement element, DrawingGroup group)
        {
            foreach (XmlNode child in element.ChildNodes)
            {
                if (child is XmlElement xmlElement)
                {
                    switch (xmlElement.Name.ToLower())
                    {
                        case "path":
                            ProcessPath(xmlElement, group);
                            break;
                        case "rect":
                            ProcessRect(xmlElement, group);
                            break;
                        case "circle":
                            ProcessCircle(xmlElement, group);
                            break;
                        case "g":
                            ProcessSvgElements(xmlElement, group);
                            break;
                    }
                }
            }
        }

        private void ProcessPath(XmlElement pathElement, DrawingGroup group)
        {
            var d = pathElement.GetAttribute("d");
            if (string.IsNullOrEmpty(d))
                return;

            try
            {
                var geometry = Geometry.Parse(d);
                var fill = GetBrush(pathElement.GetAttribute("fill"), Brushes.Black);
                var stroke = GetBrush(pathElement.GetAttribute("stroke"), null);
                
                var geometryDrawing = new GeometryDrawing
                {
                    Geometry = geometry,
                    Brush = fill,
                    Pen = stroke != null ? new Pen(stroke, 1) : null
                };

                group.Children.Add(geometryDrawing);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Failed to parse path - {ex.Message}");
            }
        }

        private void ProcessRect(XmlElement rectElement, DrawingGroup group)
        {
            try
            {
                var x = ParseDouble(rectElement.GetAttribute("x"));
                var y = ParseDouble(rectElement.GetAttribute("y"));
                var width = ParseDouble(rectElement.GetAttribute("width"));
                var height = ParseDouble(rectElement.GetAttribute("height"));

                var rect = new RectangleGeometry(new Rect(x, y, width, height));
                var fill = GetBrush(rectElement.GetAttribute("fill"), Brushes.Black);

                var geometryDrawing = new GeometryDrawing
                {
                    Geometry = rect,
                    Brush = fill
                };

                group.Children.Add(geometryDrawing);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Failed to parse rect - {ex.Message}");
            }
        }

        private void ProcessCircle(XmlElement circleElement, DrawingGroup group)
        {
            try
            {
                var cx = ParseDouble(circleElement.GetAttribute("cx"));
                var cy = ParseDouble(circleElement.GetAttribute("cy"));
                var r = ParseDouble(circleElement.GetAttribute("r"));

                var circle = new EllipseGeometry(new Point(cx, cy), r, r);
                var fill = GetBrush(circleElement.GetAttribute("fill"), Brushes.Black);

                var geometryDrawing = new GeometryDrawing
                {
                    Geometry = circle,
                    Brush = fill
                };

                group.Children.Add(geometryDrawing);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Failed to parse circle - {ex.Message}");
            }
        }

        private Brush GetBrush(string colorString, Brush defaultBrush)
        {
            if (string.IsNullOrEmpty(colorString) || colorString == "none")
                return null;

            try
            {
                return (Brush)new BrushConverter().ConvertFromString(colorString);
            }
            catch
            {
                return defaultBrush;
            }
        }

        private double ParseDouble(string value, double defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                return result;

            return defaultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

