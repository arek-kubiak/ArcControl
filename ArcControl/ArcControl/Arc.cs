using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace ArcControl
{
    public class Arc : Canvas
    {
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(Arc), new PropertyMetadata(50.0, OnSizePropertyChanged));
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof(double), typeof(Arc), new PropertyMetadata(2.0, OnSizePropertyChanged));
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Color), typeof(Arc), new PropertyMetadata(default(Brush)));
        public static readonly DependencyProperty PercentValueProperty = DependencyProperty.Register("PercentValue", typeof(double), typeof(Arc), new PropertyMetadata(0.0, OnPercentValuePropertyChanged));

        public Arc()
        {
            this.Loaded += OnLoaded;
        }

        public double Radius
        {
            get
            {
                return (double)GetValue(RadiusProperty);
            }
            set
            {
                SetValue(RadiusProperty, value);
            }
        }

        public double Thickness
        {
            get
            {
                return (double)GetValue(ThicknessProperty);
            }
            set
            {
                SetValue(ThicknessProperty, value);
            }
        }

        public Color Fill
        {
            get
            {
                return (Color)GetValue(FillProperty);
            }
            set
            {
                SetValue(FillProperty, value);
            }
        }

        public double PercentValue
        {
            get
            {
                return (double)GetValue(PercentValueProperty);
            }
            set
            {
                SetValue(PercentValueProperty, value);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SetControlSize();
            Draw();
        }

        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Arc;
            control.SetControlSize();
            control.Draw();
        }

        private static void OnPercentValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Arc;
            control.SetControlSize();
            control.Draw();
        }

        private void Draw()
        {
            Children.Clear();

            Path radialStrip = ArcHelper.GetCircleSegment(GetCenterPoint(), Radius, GetAngle());
            radialStrip.Stroke = new SolidColorBrush(Fill);
            radialStrip.StrokeThickness = Thickness;

            Children.Add(radialStrip);
        }

        private void SetControlSize()
        {
            Width = Radius * 2 + Thickness;
            Height = Radius * 2 + Thickness;
        }

        private Point GetCenterPoint()
        {
            return new Point(Radius + (Thickness / 2), Radius + (Thickness / 2));
        }

        private double GetAngle()
        {
            double angle = PercentValue / 100 * 360;
            if (angle >= 360)
            {
                angle = 359.999;
            }
            return angle;
        }
    }
}