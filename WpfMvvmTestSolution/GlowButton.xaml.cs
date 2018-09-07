using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InvendTest
{
    /// <summary>
    /// Interaction logic for GlowButton.xaml
    /// </summary>
    public partial class GlowButton : Button
    {
        public Visibility GlowVisibility
        {
            get => Dispatcher.Invoke(() => (Visibility)GetValue(GlowVisibilityProperty));
            set => Dispatcher.Invoke(() => SetValue(GlowVisibilityProperty, value));
        }

        public Brush GlowColor
        {
            get => Dispatcher.Invoke(() => (Brush)GetValue(GlowColorProperty));
            set => Dispatcher.Invoke(() => SetValue(GlowColorProperty, value));
        }

        public static DependencyProperty GlowColorProperty =
             DependencyProperty.Register("GlowColor", typeof(Brush), typeof(GlowButton),
                 new FrameworkPropertyMetadata((SolidColorBrush)(new BrushConverter().ConvertFrom("#213452")), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static DependencyProperty GlowVisibilityProperty =
             DependencyProperty.Register("GlowVisibility", typeof(Visibility), typeof(GlowButton),
                 new FrameworkPropertyMetadata(Visibility.Hidden, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public GlowButton()
        {
            InitializeComponent();
        }

        public void StartGlow() => GlowVisibility = Visibility.Visible;

        public void StopGlow() => GlowVisibility = Visibility.Hidden;
    }
}