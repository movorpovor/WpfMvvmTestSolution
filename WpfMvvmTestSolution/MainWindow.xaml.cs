using System.Windows;

namespace InvendTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new InvendTestVM();
            InitializeComponent();
        }
    }
}