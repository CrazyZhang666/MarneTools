using CommunityToolkit.Mvvm.Input;
using EasyMarneTools.Helper;
using EasyMarneTools.Utils;

namespace EasyMarneTools.Views;

/// <summary>
/// MarneView.xaml 的交互逻辑
/// </summary>
public partial class MarneView : UserControl
{
    public MarneView()
    {
        InitializeComponent();
    }

    [RelayCommand]
    private void RunMarneLauncher()
    {
        ProcessHelper.OpenProcess(CoreUtil.File_MarneLauncher);
    }

    [RelayCommand]
    private void CloseMarneLauncher()
    {
        ProcessHelper.CloseProcess(CoreUtil.Name_MarneLauncher);
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
