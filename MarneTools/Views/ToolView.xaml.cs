using CommunityToolkit.Mvvm.Input;
using MarneTools.Helper;
using MarneTools.Utils;

namespace MarneTools.Views;

/// <summary>
/// ToolView.xaml 的交互逻辑
/// </summary>
public partial class ToolView : UserControl
{
    public ToolView()
    {
        InitializeComponent();
    }

    [RelayCommand]
    private void RunBattlefieldChat()
    {
        ProcessHelper.OpenProcess(CoreUtil.File_BattlefieldChat);
    }

    [RelayCommand]
    private void CloseBattlefieldChat()
    {
        ProcessHelper.CloseProcess(CoreUtil.Name_BattlefieldChat);
    }
}
