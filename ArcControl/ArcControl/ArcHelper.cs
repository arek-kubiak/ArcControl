using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace ArcControl
{
    public static class ArcHelper
    {
        private const double RADIANS = Math.PI / 180;

        public static Path GetCircleSegment(Point centerPoint, double radius, double angle)
        {
            var path = new Path();
            var pathGeometry = new PathGeometry();

            var circleStart = new Point(centerPoint.X, centerPoint.Y - radius);

            var arcSegment = new ArcSegment
            {
                IsLargeArc = angle > 180.0,
                Point = ScaleUnitCirclePoint(centerPoint, angle, radius),
                Size = new Size(radius, radius),
                SweepDirection = SweepDirection.Clockwise
            };

            var pathFigure = new PathFigure
            {
                StartPoint = circleStart,
                IsClosed = false
            };

            pathFigure.Segments.Add(arcSegment);
            pathGeometry.Figures.Add(pathFigure);

            path.Data = pathGeometry;
            return path;
        }

        private static Point ScaleUnitCirclePoint(Point origin, double angle, double radius)
        {
            return new Point(origin.X + Math.Sin(RADIANS * angle) * radius, origin.Y - Math.Cos(RADIANS * angle) * radius);
        }
    }
}