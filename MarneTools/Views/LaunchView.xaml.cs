using CommunityToolkit.Mvvm.Input;
using MarneTools.Helper;
using MarneTools.Utils;
using System.IO;
using System.Reflection;

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

    #region Radmin VPN
    [RelayCommand]
    private void RunRadminVPN()
    {
        // 如果找到 RadminLAN 程序，则直接运行
        if (File.Exists(CoreUtil.RadminInstallPath))
        {
            ProcessHelper.OpenProcess(CoreUtil.RadminInstallPath);
            return;
        }

        // 如果未发现 RadminLAN 程序，则提示用户安装
        if (MessageBox.Show("当前未发现用户安装 RadminLAN 程序，是否立即安装？", "警告",
            MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
        {
            ProcessHelper.OpenProcess(CoreUtil.File_RadminLAN);
        }
    }

    [RelayCommand]
    private void CloseRadminVPN()
    {
        ProcessHelper.CloseProcess(CoreUtil.Name_RadminLAN);
    }
    #endregion

    #region Marne Launcher
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
    #endregion

    #region Frosty Mod Manager
    [RelayCommand]
    private void RunFrostyModManager()
    {
        // 如果战地1正在运行，则不允许启动FrostyModManager
        if (ProcessHelper.IsAppRun(CoreUtil.Name_BF1))
        {
            NotifierHelper.Show(NotifierType.Warning, $"战地1正在运行，请关闭战地后再启动FrostyModManager程序");
            return;
        }

        ProcessHelper.OpenProcess(CoreUtil.File_FrostyModManager);
    }

    [RelayCommand]
    private void CloseFrostyModManager()
    {
        ProcessHelper.CloseProcess(CoreUtil.Name_FrostyModManager);
    }
    #endregion
}
