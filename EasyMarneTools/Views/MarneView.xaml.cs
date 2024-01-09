using CommunityToolkit.Mvvm.Input;
using EasyMarneTools.Helper;

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
        ProcessHelper.OpenProcess(FileUtil.File_MarneLauncher);
    }

    [RelayCommand]
    private void CloseMarneLauncher()
    {
        ProcessHelper.CloseProcess(FileUtil.Name_MarneLauncher);
    }

    [RelayCommand]
    private void RunBattlefieldChat()
    {
        ProcessHelper.OpenProcess(FileUtil.File_BattlefieldChat);
    }

    [RelayCommand]
    private void CloseBattlefieldChat()
    {
        ProcessHelper.CloseProcess(FileUtil.Name_BattlefieldChat);
    }
}
