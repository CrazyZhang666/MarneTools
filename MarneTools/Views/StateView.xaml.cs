using MarneTools.Core;

namespace MarneTools.Views;

/// <summary>
/// StateView.xaml 的交互逻辑
/// </summary>
public partial class StateView : UserControl
{
    public StateView()
    {
        InitializeComponent();

        Console.SetOut(new TextBoxWriter(this.TextBox_Logger));
    }
}
