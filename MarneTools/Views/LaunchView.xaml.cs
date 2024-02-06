using MarneTools.Helper;
using MarneTools.Utils;

namespace MarneTools.Views;

/// <summary>
/// LaunchView.xaml 的交互逻辑
/// </summary>
public partial class LaunchView : UserControl
{
    public LaunchView()
    {
        InitializeComponent();
    }

    #region Frosty Mod Manager
    [RelayCommand]
    private void RunFrostyModManager()
    {
        // 如果战地1正在运行，则不允许启动FrostyModManager
        if (ProcessHelper.IsAppRun(CoreUtil.Name_BF1))
        {
            NotifierHelper.Show(NotifierType.Warning, "战地1正在运行，请关闭后再启动FrostyModManager程序");
            return;
        }

        ProcessHelper.OpenProcess(CoreUtil.File_FrostyMod_FrostyModManager);
    }

    [RelayCommand]
    private void CloseFrostyModManager()
    {
        ProcessHelper.CloseProcess(CoreUtil.Name_FrostyModManager);
    }

    [RelayCommand]
    private void ClearModData()
    {
        try
        {
            if (ProcessHelper.IsAppRun(CoreUtil.Name_BF1))
            {
                NotifierHelper.Show(NotifierType.Warning, "战地1正在运行，请关闭后再执行清理Mod数据操作");
                return;
            }

            if (ProcessHelper.IsAppRun(CoreUtil.Name_FrostyModManager))
            {
                NotifierHelper.Show(NotifierType.Warning, "FrostyModManager正在运行，请关闭后再执行清理Mod数据操作");
                return;
            }

            var modDataDir = Path.Combine(CoreUtil.BF1InstallDir, "ModData");
            if (!Directory.Exists(modDataDir))
            {
                NotifierHelper.Show(NotifierType.Warning, "未发现战地1Mod数据文件夹，操作取消");
                return;
            }

            FileHelper.ClearDirectory(modDataDir);
            NotifierHelper.Show(NotifierType.Success, "执行清理Mod数据操作操作成功");
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
    #endregion

    #region Marne Launcher
    [RelayCommand]
    private void RunMarneLauncher()
    {
        ProcessHelper.OpenProcess(CoreUtil.File_Marne_MarneLauncher);
    }

    [RelayCommand]
    private void CloseMarneLauncher()
    {
        ProcessHelper.CloseProcess(CoreUtil.Name_MarneLauncher);
    }

    [RelayCommand]
    private void RunBattlefieldChat()
    {
        ProcessHelper.OpenProcess(CoreUtil.File_BattlefieldChat, true);
    }
    #endregion
}
