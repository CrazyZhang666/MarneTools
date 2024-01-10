using EasyMarneTools.Core;

namespace EasyMarneTools.Views;

/// <summary>
/// HomeView.xaml 的交互逻辑
/// </summary>
public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();

        Console.SetOut(new TextBoxWriter(this.TextBox_Logger));
    }
}
