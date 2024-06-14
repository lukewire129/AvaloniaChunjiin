using Avalonia.Controls;
using AvaloniaChunjiin.ViewModels;

namespace AvaloniaChunjiin;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new MainViewModel();
    }
}