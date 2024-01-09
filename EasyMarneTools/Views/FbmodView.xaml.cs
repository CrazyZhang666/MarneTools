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
        // 下载mod到 Mods\bf1 文件夹

        // 获取战地1安装目录

        // 修改 Frosty\manager_config.json 配置文件

        ProcessHelper.OpenProcess(FileUtil.File_FrostyModManager);
    }

    [RelayCommand]
    private void CloseFrostyModManager()
    {
        ProcessHelper.CloseProcess(FileUtil.Name_FrostyModManager);
    }
}
