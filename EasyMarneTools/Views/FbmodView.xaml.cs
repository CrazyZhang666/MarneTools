using CommunityToolkit.Mvvm.Input;
using EasyMarneTools.Data;
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
        // 如果程序已经在运行，则退出
        if (ProcessHelper.IsAppRun(FileUtil.Name_FrostyModManager))
        {
            NotifierHelper.Show(NotifierType.Warning, $"程序 {FileUtil.Name_FrostyModManager} 已经运行了，请不要重复运行");
            return;
        }

        var modConfig = new ModConfig();

        // 下载mod到 Mods\bf1 文件夹

        // 通过注册表获取战地1安装目录
        using var bf1Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\EA Games\\Battlefield 1");
        if (bf1Reg is not null)
        {
            var dir = bf1Reg.GetValue("Install Dir") as string;

            if (Directory.Exists(dir))
                modConfig.Games.bf1.GamePath = Path.GetDirectoryName(dir);
        }

        // 写入 Frosty\manager_config.json 配置文件
        File.WriteAllText(FileUtil.File_Local_Frosty_ManagerConfig, JsonHelper.JsonSerialize(modConfig));

        ProcessHelper.OpenProcess(FileUtil.File_FrostyModManager);
    }

    [RelayCommand]
    private void CloseFrostyModManager()
    {
        ProcessHelper.CloseProcess(FileUtil.Name_FrostyModManager);
    }
}
