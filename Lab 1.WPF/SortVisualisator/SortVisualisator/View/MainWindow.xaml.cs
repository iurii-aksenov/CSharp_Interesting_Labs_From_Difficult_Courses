using System.Windows;
using System.Windows.Controls;
using SortVisualisator.ViewModel;

namespace SortVisualisator.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Canvas canvas = new Canvas();
            var buttons  =  CanvasButtons.Children;
            DataContext = new ApplicationViewModel(buttons);
        }
    }
}
