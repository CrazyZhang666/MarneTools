using CommunityToolkit.Mvvm.Input;
using EasyMarneTools.Data;
using EasyMarneTools.Helper;
using EasyMarneTools.Models;
using EasyMarneTools.Utils;

namespace EasyMarneTools;

/// <summary>
/// MainWindow.xaml 的交互逻辑
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// 主窗口数据模型
    /// </summary>
    public MainModel MainModel { get; set; } = new();

    /// <summary>
    /// 页面导航字典
    /// </summary>
    private readonly Dictionary<string, UserControl> NavDictionary = [];

    /////////////////////////////////////////

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Navigate("HomeView");

        // 检查目标进程是否运行线程
        new Thread(CheckProcessIsRunThread)
        {
            Name = "CheckProcessIsRunThread",
            IsBackground = true
        }.Start();

        // 初始化核心数据线程
        new Thread(InitCoreDataThread)
        {
            Name = "InitCoreDataThread",
            IsBackground = true
        }.Start();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        CoreUtil.IsAppRunning = false;
    }

    /// <summary>
    /// View页面导航
    /// </summary>
    /// <param name="viewName">页面名称</param>
    [RelayCommand]
    private void Navigate(string viewName)
    {
        if (!NavDictionary.TryGetValue(viewName, out UserControl value))
        {
            var viewType = Type.GetType($"EasyMarneTools.Views.{viewName}");
            if (viewType == null)
                return;

            if (Activator.CreateInstance(viewType) is not UserControl userControl)
                return;

            value = userControl;
            NavDictionary.Add(viewName, value);
        }

        if (ContentControl_NavRegion.Content == value)
            return;

        ContentControl_NavRegion.Content = value;
    }

    /// <summary>
    /// 检查目标进程是否运行线程
    /// </summary>
    private void CheckProcessIsRunThread()
    {
        while (CoreUtil.IsAppRunning)
        {
            var processList = Process.GetProcesses().ToList();

            MainModel.IsRadminRun = processList.Find(x => x.ProcessName.Equals(FileUtil.Name_RadminLAN, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsFrostyModRun = processList.Find(x => x.ProcessName.Equals(FileUtil.Name_FrostyModManager, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsMarneRun = processList.Find(x => x.ProcessName.Equals(FileUtil.Name_MarneLauncher, StringComparison.OrdinalIgnoreCase)) is not null;

            MainModel.IsOriginRun = processList.Find(x => x.ProcessName.Equals(FileUtil.Name_Origin, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsEaAppRun = processList.Find(x => x.ProcessName.Equals(FileUtil.Name_EaApp, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsSteamRun = processList.Find(x => x.ProcessName.Equals(FileUtil.Name_Steam, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsBF1Run = processList.Find(x => x.ProcessName.Equals(FileUtil.Name_BF1, StringComparison.OrdinalIgnoreCase)) is not null;

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 初始化核心数据线程
    /// </summary>
    private void InitCoreDataThread()
    {
        // 强制关闭FrostyModManager程序
        ProcessHelper.CloseProcessNoHit(FileUtil.Name_FrostyModManager);

        // 通过注册表获取战地1安装目录
        using var bf1Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\EA Games\\Battlefield 1");
        if (bf1Reg is not null)
        {
            Console.WriteLine("✔️ 获取战地1注册表成功");

            var installDir = bf1Reg.GetValue("Install Dir") as string;
            if (Directory.Exists(installDir))
            {
                Globals.BF1InstallDir = Path.GetDirectoryName(installDir);
                Console.WriteLine("✔️ 获取战地1注册表安装目录成功");
            }
            else
            {
                Console.WriteLine("❌ 获取战地1注册表安装目录失败");
            }
        }
        else
        {
            Console.WriteLine("❌ 获取战地1注册表失败");
        }

        var modConfig = new ModConfig();

        // 下载mod到 Mods\bf1 文件夹

        // 获取战地1安装目录
        modConfig.Games.bf1.GamePath = Globals.BF1InstallDir;

        // 写入 Frosty\manager_config.json 配置文件
        File.WriteAllText(FileUtil.File_Local_Frosty_ManagerConfig, JsonHelper.JsonSerialize(modConfig));
    }
}