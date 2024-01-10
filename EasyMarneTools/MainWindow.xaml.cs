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
        // 设置主窗口标题
        this.Title = CoreUtil.MainAppWindowName + CoreUtil.ClientVersion;

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

            MainModel.IsRadminRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_RadminLAN, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsFrostyModRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_FrostyModManager, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsMarneRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_MarneLauncher, StringComparison.OrdinalIgnoreCase)) is not null;

            MainModel.IsOriginRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_Origin, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsEaAppRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_EaApp, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsSteamRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_Steam, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsBF1Run = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_BF1, StringComparison.OrdinalIgnoreCase)) is not null;

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 初始化核心数据线程
    /// </summary>
    private async void InitCoreDataThread()
    {
        try
        {
            this.Dispatcher.Invoke(() =>
            {
                NotifierHelper.Show(NotifierType.Information, "开始初始化工具，请稍后...");
            });

            // 检查软件更新
            Console.WriteLine("☁️ 开始获取服务器配置信息...");
            var response = await HttpHelper.GetServerConfig();
            if (response.IsSuccessful)
            {
                var jsonNode = JsonNode.Parse(response.Content);

                CoreUtil.ServerVersion = Version.Parse(jsonNode["Version"].GetValue<string>());

                CoreUtil.WebUpdate = jsonNode["Update"].GetValue<string>();
                CoreUtil.WebModName = jsonNode["ModName"].GetValue<string>();
                CoreUtil.WebModDownload = jsonNode["ModDownload"].GetValue<string>();

                Console.WriteLine("✔️ 获取服务器配置信息成功");
                Console.WriteLine($"🔔 服务器版本号：{CoreUtil.ServerVersion}");
                Console.WriteLine($"🔔 客户端版本号：{CoreUtil.ClientVersion}");

                // 发现新版本
                if (CoreUtil.ServerVersion > CoreUtil.ClientVersion)
                {
                    Console.WriteLine($"📢 发现新版本，请下载最新版本");

                    this.Dispatcher.Invoke(() =>
                    {
                        if (MessageBox.Show("发现新版本，点击确认键下载最新版本", "更新提示",
                            MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            ProcessHelper.OpenLink(CoreUtil.WebUpdate);
                        }

                        Application.Current.Shutdown();
                    });

                    return;
                }
            }
            else
            {
                Console.WriteLine("❌ 获取服务器配置信息失败，软件可能无法正常工作，初始化终止");
                this.Dispatcher.Invoke(() =>
                {
                    NotifierHelper.Show(NotifierType.Error, "获取服务器配置信息失败，初始化终止");
                });
                return;
            }

            // 强制关闭FrostyModManager程序
            ProcessHelper.CloseProcessNoHit(CoreUtil.Name_FrostyModManager);
            Console.WriteLine("✔️ 关闭FrostyModManager程序成功");

            // 清空旧版Mod文件夹
            FileHelper.ClearDirectory(CoreUtil.Dir_FrostyMod_Mods_Bf1);
            Console.WriteLine("✔️ 清空旧版Mod文件夹成功");

            // 通过注册表获取战地1安装目录
            using var bf1Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\EA Games\\Battlefield 1");
            if (bf1Reg is not null)
            {
                Console.WriteLine("✔️ 获取战地1注册表成功");

                var installDir = bf1Reg.GetValue("Install Dir") as string;
                if (Directory.Exists(installDir))
                {
                    CoreUtil.BF1InstallDir = Path.GetDirectoryName(installDir);
                    Console.WriteLine("✔️ 获取战地1注册表安装目录成功");

                    Console.WriteLine($"🔔 此电脑战地1安装目录：{CoreUtil.BF1InstallDir}");
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

            // 获取Mod名称
            var modName = Path.GetFileName(CoreUtil.WebModDownload);
            Console.WriteLine($"🔔 Mod中文名称：{CoreUtil.WebModName}");
            Console.WriteLine($"🔔 Mod文件名称：{modName}");

            // 下载mod到 Mods\bf1 文件夹
            Console.WriteLine("☁️ 开始下载Mod...");
            var bytes = await HttpHelper.DownloadMod(CoreUtil.WebModDownload);
            if (bytes is not null)
            {
                Console.WriteLine("✔️ 下载Mod成功");
                File.WriteAllBytes(Path.Combine(CoreUtil.Dir_FrostyMod_Mods_Bf1, modName), bytes);
                Console.WriteLine("✔️ 保存Mod到指定文件夹成功");
            }
            else
            {
                Console.WriteLine("❌ 下载Mod失败，软件可能无法正常工作，初始化终止");
                this.Dispatcher.Invoke(() =>
                {
                    NotifierHelper.Show(NotifierType.Error, "下载Mod失败，初始化终止");
                });
                return;
            }

            // 设置Mod名称并启用
            modConfig.Games.bf1.Packs.Default = $"{modName}:True";

            // 设置战地1安装目录
            modConfig.Games.bf1.GamePath = CoreUtil.BF1InstallDir;

            // 写入Frosty\manager_config.json配置文件
            File.WriteAllText(CoreUtil.File_FrostyMod_Frosty_ManagerConfig, JsonHelper.JsonSerialize(modConfig));
            Console.WriteLine("✔️ 写入FrostyModManager配置文件成功");

            Console.WriteLine("👏 初始化完毕");
            this.Dispatcher.Invoke(() =>
            {
                NotifierHelper.Show(NotifierType.Success, "初始化完毕，现在你可以正常使用工具了");
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ {ex.Message}");
            this.Dispatcher.Invoke(() =>
            {
                NotifierHelper.Show(NotifierType.Error, "初始化异常，请查看错误日志");
            });
        }
    }
}