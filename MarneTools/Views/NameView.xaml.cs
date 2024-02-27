namespace MarneTools.Views;

/// <summary>
/// NameView.xaml 的交互逻辑
/// </summary>
public partial class NameView : UserControl
{
    public NameView()
    {
        InitializeComponent();
    }

    [RelayCommand]
    private void ChangePlayerName()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Stream stream = assembly.GetManifestResourceStream("MarneTools.Resources.dinput8.dll");
    }
}
