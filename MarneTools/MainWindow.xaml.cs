using MarneTools.Utils;

namespace MarneTools;

/// <summary>
/// MainWindow.xaml 的交互逻辑
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        CoreUtil.IsAppRunning = false;
    }
}