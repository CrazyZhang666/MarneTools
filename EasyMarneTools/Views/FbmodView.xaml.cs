using CommunityToolkit.Mvvm.Input;
using EasyMarneTools.Helper;
using EasyMarneTools.Utils;

namespace EasyMarneTools.Views;

/// <summary>
/// FbmodView.xaml 的交互逻辑
/// </summary>
public partial class FbmodView : UserControl
{
    public FbmodView()
    {
        InitializeComponent();
    }

    [RelayCommand]
    private void RunFrostyModManager()
    {
        ProcessHelper.OpenProcess(CoreUtil.File_FrostyModManager);
    }

    [RelayCommand]
    private void CloseFrostyModManager()
    {
        ProcessHelper.CloseProcess(CoreUtil.Name_FrostyModManager);
    }
}
