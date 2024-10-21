using Avalonia.Controls;

namespace View.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainView view) : this()
    {
        Content = view;
    }

    public MainWindow()
    {
        InitializeComponent();
    }
}
