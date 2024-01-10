using CommunityToolkit.Mvvm.Input;
using EasyMarneTools.Helper;
using EasyMarneTools.Utils;

namespace EasyMarneTools.Views;

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
