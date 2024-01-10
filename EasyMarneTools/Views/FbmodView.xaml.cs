using CommunityToolkit.Mvvm.Input;
using EasyMarneTools.Helper;

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
        ProcessHelper.OpenProcess(FileUtil.File_FrostyModManager);
    }

    [RelayCommand]
    private void CloseFrostyModManager()
    {
        ProcessHelper.CloseProcess(FileUtil.Name_FrostyModManager);
    }
}
