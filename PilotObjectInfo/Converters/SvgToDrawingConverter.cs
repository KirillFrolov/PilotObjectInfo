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
                        case "ellipse":
                            ProcessEllipse(xmlElement, group);
                            break;
                        case "line":
                            ProcessLine(xmlElement, group);
                            break;
                        case "polyline":
                            ProcessPolyline(xmlElement, group);
                            break;
                        case "polygon":
                            ProcessPolygon(xmlElement, group);
                            break;
                        case "g":
                            ProcessGroup(xmlElement, group);
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
                
                // Handle opacity
                var opacity = ParseDouble(pathElement.GetAttribute("opacity"), 1.0);
                var fillOpacity = ParseDouble(pathElement.GetAttribute("fill-opacity"), 1.0);
                var strokeOpacity = ParseDouble(pathElement.GetAttribute("stroke-opacity"), 1.0);
                
                // Apply fill opacity
                if (fill != null && (opacity < 1.0 || fillOpacity < 1.0))
                {
                    fill = fill.Clone();
                    fill.Opacity = opacity * fillOpacity;
                }
                
                // Apply stroke opacity
                Pen pen = null;
                if (stroke != null)
                {
                    if (opacity < 1.0 || strokeOpacity < 1.0)
                    {
                        stroke = stroke.Clone();
                        stroke.Opacity = opacity * strokeOpacity;
                    }
                    var strokeWidth = ParseDouble(pathElement.GetAttribute("stroke-width"), 1.0);
                    pen = new Pen(stroke, strokeWidth);
                }
                
                var geometryDrawing = new GeometryDrawing
                {
                    Geometry = geometry,
                    Brush = fill,
                    Pen = pen
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
                var stroke = GetBrush(rectElement.GetAttribute("stroke"), null);

                var geometryDrawing = new GeometryDrawing
                {
                    Geometry = rect,
                    Brush = fill,
                    Pen = stroke != null ? new Pen(stroke, 1) : null
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
                var stroke = GetBrush(circleElement.GetAttribute("stroke"), null);

                var geometryDrawing = new GeometryDrawing
                {
                    Geometry = circle,
                    Brush = fill,
                    Pen = stroke != null ? new Pen(stroke, 1) : null
                };

                group.Children.Add(geometryDrawing);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Failed to parse circle - {ex.Message}");
            }
        }

        private void ProcessEllipse(XmlElement ellipseElement, DrawingGroup group)
        {
            try
            {
                var cx = ParseDouble(ellipseElement.GetAttribute("cx"));
                var cy = ParseDouble(ellipseElement.GetAttribute("cy"));
                var rx = ParseDouble(ellipseElement.GetAttribute("rx"));
                var ry = ParseDouble(ellipseElement.GetAttribute("ry"));

                var ellipse = new EllipseGeometry(new Point(cx, cy), rx, ry);
                var fill = GetBrush(ellipseElement.GetAttribute("fill"), Brushes.Black);
                var stroke = GetBrush(ellipseElement.GetAttribute("stroke"), null);

                var geometryDrawing = new GeometryDrawing
                {
                    Geometry = ellipse,
                    Brush = fill,
                    Pen = stroke != null ? new Pen(stroke, 1) : null
                };

                group.Children.Add(geometryDrawing);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Failed to parse ellipse - {ex.Message}");
            }
        }

        private void ProcessLine(XmlElement lineElement, DrawingGroup group)
        {
            try
            {
                var x1 = ParseDouble(lineElement.GetAttribute("x1"));
                var y1 = ParseDouble(lineElement.GetAttribute("y1"));
                var x2 = ParseDouble(lineElement.GetAttribute("x2"));
                var y2 = ParseDouble(lineElement.GetAttribute("y2"));

                var line = new LineGeometry(new Point(x1, y1), new Point(x2, y2));
                var stroke = GetBrush(lineElement.GetAttribute("stroke"), Brushes.Black);

                var geometryDrawing = new GeometryDrawing
                {
                    Geometry = line,
                    Pen = new Pen(stroke, 1)
                };

                group.Children.Add(geometryDrawing);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Failed to parse line - {ex.Message}");
            }
        }

        private void ProcessPolyline(XmlElement polylineElement, DrawingGroup group)
        {
            try
            {
                var points = ParsePoints(polylineElement.GetAttribute("points"));
                if (points != null && points.Count > 0)
                {
                    var pathFigure = new PathFigure { StartPoint = points[0] };
                    for (int i = 1; i < points.Count; i++)
                    {
                        pathFigure.Segments.Add(new LineSegment(points[i], true));
                    }

                    var pathGeometry = new PathGeometry();
                    pathGeometry.Figures.Add(pathFigure);

                    var fill = GetBrush(polylineElement.GetAttribute("fill"), null);
                    var stroke = GetBrush(polylineElement.GetAttribute("stroke"), Brushes.Black);

                    var geometryDrawing = new GeometryDrawing
                    {
                        Geometry = pathGeometry,
                        Brush = fill,
                        Pen = stroke != null ? new Pen(stroke, 1) : null
                    };

                    group.Children.Add(geometryDrawing);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Failed to parse polyline - {ex.Message}");
            }
        }

        private void ProcessPolygon(XmlElement polygonElement, DrawingGroup group)
        {
            try
            {
                var points = ParsePoints(polygonElement.GetAttribute("points"));
                if (points != null && points.Count > 0)
                {
                    var pathFigure = new PathFigure { StartPoint = points[0], IsClosed = true };
                    for (int i = 1; i < points.Count; i++)
                    {
                        pathFigure.Segments.Add(new LineSegment(points[i], true));
                    }

                    var pathGeometry = new PathGeometry();
                    pathGeometry.Figures.Add(pathFigure);

                    var fill = GetBrush(polygonElement.GetAttribute("fill"), Brushes.Black);
                    var stroke = GetBrush(polygonElement.GetAttribute("stroke"), null);

                    var geometryDrawing = new GeometryDrawing
                    {
                        Geometry = pathGeometry,
                        Brush = fill,
                        Pen = stroke != null ? new Pen(stroke, 1) : null
                    };

                    group.Children.Add(geometryDrawing);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SvgToDrawingConverter: Failed to parse polygon - {ex.Message}");
            }
        }

        private void ProcessGroup(XmlElement groupElement, DrawingGroup group)
        {
            // Recursively process group contents
            ProcessSvgElements(groupElement, group);
        }

        private PointCollection ParsePoints(string pointsString)
        {
            if (string.IsNullOrEmpty(pointsString))
                return null;

            var points = new PointCollection();
            var coords = pointsString.Split(new[] { ' ', ',', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < coords.Length - 1; i += 2)
            {
                if (double.TryParse(coords[i], NumberStyles.Float, CultureInfo.InvariantCulture, out var x) &&
                    double.TryParse(coords[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out var y))
                {
                    points.Add(new Point(x, y));
                }
            }

            return points.Count > 0 ? points : null;
        }

        private Brush GetBrush(string colorString, Brush defaultBrush)
        {
            if (string.IsNullOrEmpty(colorString) || colorString == "none")
                return null;

            // Handle 'currentColor' - use default
            if (colorString == "currentColor")
                return defaultBrush ?? Brushes.Black;

            try
            {
                // Try to parse as color
                var brush = (Brush)new BrushConverter().ConvertFromString(colorString);
                return brush;
            }
            catch
            {
                // If parsing fails, use default
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

