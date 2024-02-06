using Downloader;
using MarneTools.Api;
using MarneTools.Data;
using MarneTools.Helper;
using MarneTools.Models;
using MarneTools.Utils;

namespace MarneTools;

/// <summary>
/// LoadWindow.xaml 的交互逻辑
/// </summary>
public partial class LoadWindow : Window
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public LoadModel LoadModel { get; set; } = new();

    public LoadWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // 初始化线程
        new Thread(InitializeThread)
        {
            Name = "InitializeThread",
            IsBackground = true
        }.Start();

        LoadModel.ReceiveSize = 0;
        LoadModel.TotalSize = 100;
        LoadModel.DownloadState = "0KB / 0MB";
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {

    }

    /// <summary>
    /// 追加日志到UI界面
    /// </summary>
    /// <param name="log"></param>
    private void AppendLogger(string log)
    {
        this.Dispatcher.Invoke(() =>
        {
            TextBox_Logger.AppendText($"{DateTime.Now:T}  {log}{Environment.NewLine}");
        });
    }

    /// <summary>
    /// 初始化线程
    /// </summary>
    private async void InitializeThread()
    {
        try
        {
            // 检查软件更新
            AppendLogger("☁️ 开始获取服务器配置信息...");
            var response = await CoreApi.GetWebConfig();
            if (!response.IsSuccess)
            {
                AppendLogger($"❌ 获取服务器配置信息失败，初始化终止。异常信息：{response.Content}");
                return;
            }
            AppendLogger("✔️ 获取服务器配置信息成功");

            // 开始解析服务器配置文件
            var jsonNode = JsonNode.Parse(response.Content)!;
            AppendLogger("✔️ 解析服务器配置信息成功");

            // 获取服务器版本号
            var webVersion = Version.Parse(jsonNode!["Version"]!.GetValue<string>());

            AppendLogger($"🔔 客户端版本号：{CoreUtil.ClientVersion}");
            AppendLogger($"🔔 服务器版本号：{webVersion}");

            // 如果发现新版本
            if (webVersion > CoreUtil.ClientVersion)
            {
                AppendLogger($"📢 发现新版本，请下载最新版本");

                // 工具箱更新下载网盘地址
                var webUpdate = jsonNode!["Update"]!.GetValue<string>();

                this.Dispatcher.Invoke(() =>
                {
                    if (MessageBox.Show("发现新版本，点击确认键下载最新版本", "更新提示",
                        MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                    {
                        ProcessHelper.OpenLink(webUpdate);
                    }

                    Application.Current.Shutdown();
                });

                return;
            }

            AppendLogger("🔔 恭喜，你的工具箱版本是最新版本");

            /////////////////////////////////////////////////////

            // 服务器名称
            CoreUtil.ServerName = jsonNode!["ServerName"]!.GetValue<string>();

            // Mod文件更新下载地址、MD5
            var webModFile = jsonNode!["ModFile"]!.GetValue<string>();
            var webModFileMD5 = jsonNode!["ModFileMD5"]!.GetValue<string>();

            /////////////////////////////////////////////////////

            var downloadOpt = new DownloadConfiguration()
            {
                BufferBlockSize = 10240,
                MaximumBytesPerSecond = 1024 * 1024 * 50,
                MaximumMemoryBufferBytes = 1024 * 1024 * 50,
                ClearPackageOnCompletionWithFailure = true,
                ReserveStorageSpaceBeforeStartingDownload = true
            };

            // 获取Mod文件名称
            var modName = Path.GetFileName(webModFile);
            // 本地保存Mod文件路径
            var saveModPath = Path.Combine(CoreUtil.Dir_FrostyMod_Mods_Bf1, modName);

            // 判断Mod文件是否被占用
            if (!FileHelper.IsOccupied(saveModPath))
            {
                // 开始检查Mod文件是否需要更新
                var modFileMD5 = FileHelper.GetFileMD5(saveModPath);
                if (!webModFileMD5.Equals(modFileMD5))
                {
                    // 开始下载最新版本Mod文件
                    AppendLogger($"📢 发现新版本Mod文件，正在下载最新版本");

                    // 清空旧版Mod文件夹
                    FileHelper.ClearDirectory(CoreUtil.Dir_FrostyMod_Mods_Bf1);
                    AppendLogger("✔️ 清空Mod文件夹旧版Mod文件成功");

                    var downloader = DownloadBuilder.New()
                        .WithUrl(webModFile)
                        .WithFileLocation(saveModPath)
                        .WithConfiguration(downloadOpt)
                        .Build();

                    downloader.DownloadStarted += (sender, e) =>
                    {
                        LoadModel.ReceiveSize = 0;
                        LoadModel.TotalSize = e.TotalBytesToReceive;
                        LoadModel.DownloadState = "0KB / 0MB";

                        LoadModel.ProgressValue = 0;

                        AppendLogger($"🔔 Mod文件大小：{MiscUtil.GetFileForamtSize(e.TotalBytesToReceive)}");
                    };
                    downloader.DownloadProgressChanged += (sender, e) =>
                    {
                        LoadModel.ReceiveSize = e.ReceivedBytesSize;
                        LoadModel.DownloadState = $"{MiscUtil.GetFileForamtSize(e.ReceivedBytesSize)} / {MiscUtil.GetFileForamtSize(e.TotalBytesToReceive)}";

                        LoadModel.ProgressValue = LoadModel.ReceiveSize / LoadModel.TotalSize;
                    };
                    downloader.DownloadFileCompleted += (sender, e) =>
                    {
                        AppendLogger("✔️ Mod文件下载完成");
                    };

                    await downloader.StartAsync();
                }
                else
                {
                    AppendLogger("🔔 恭喜，你的Mod文件是最新版本");
                }
            }
            else
            {
                AppendLogger("⚠️ Mod文件被占用，跳过检查更新");
            }

            AppendLogger($"🔔 Mod文件名称：{modName}");

            /////////////////////////////////////////////////////

            var currentDir = Directory.GetCurrentDirectory();
            if (CoreUtil.CheckHasChinese(currentDir))
            {
                AppendLogger("❌ 当前运行路径含有中文，请在英文目录下运行，初始化终止");
                return;
            }

            /////////////////////////////////////////////////////

            // 通过注册表获取战地1安装目录
            using var bf1Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\EA Games\\Battlefield 1");
            if (bf1Reg is null)
            {
                AppendLogger("❌ 获取战地1注册表失败，请尝试右键管理员运行，初始化终止");
                return;
            }
            AppendLogger("✔️ 获取战地1注册表成功");

            var installDir = bf1Reg.GetValue("Install Dir") as string;
            if (!Directory.Exists(installDir))
            {
                AppendLogger("❌ 获取战地1注册表安装目录失败，请尝试右键管理员运行，初始化终止");
                return;
            }
            AppendLogger("✔️ 获取战地1注册表安装目录成功");

            CoreUtil.BF1InstallDir = Path.GetDirectoryName(installDir);
            AppendLogger($"🔔 此电脑战地1安装目录：{CoreUtil.BF1InstallDir}");

            /////////////////////////////////////////////////////

            var systeamDisk = Environment.GetEnvironmentVariable("systemdrive");
            if (CoreUtil.BF1InstallDir.StartsWith(systeamDisk))
            {
                if (!CoreUtil.IsRunAsAdmin())
                {
                    AppendLogger("❌ 检测到战地1安装在系统盘，请尝试右键管理员运行，初始化终止");
                    return;
                }
            }

            var dinput8Path = Path.Combine(CoreUtil.BF1InstallDir, "dinput8.dll");
            if (File.Exists(dinput8Path))
            {
                AppendLogger("❌ 发现战地1目录有免Orign补丁，请删除后再重试，初始化终止");
                return;
            }

            /////////////////////////////////////////////////////

            // 关闭FrostyModManager程序
            ProcessHelper.CloseProcessNoHit(CoreUtil.Name_FrostyModManager);
            AppendLogger("✔️ 关闭FrostyModManager程序成功");

            // 创建FrostyMod配置文件
            var modConfig = new ModConfig();

            // 设置Mod名称并启用
            modConfig.Games.bf1.Packs.Default = $"{modName}:True";

            // 设置战地1安装目录
            modConfig.Games.bf1.GamePath = CoreUtil.BF1InstallDir;

            // 写入Frosty\manager_config.json配置文件
            File.WriteAllText(CoreUtil.File_FrostyMod_Frosty_ManagerConfig, JsonHelper.JsonSerialize(modConfig));
            AppendLogger("✔️ 写入FrostyModManager配置文件成功");

            AppendLogger("👏 初始化成功，正在准备启动主程序");

            /////////////////////////////////////////////////////

            Thread.Sleep(500);

            this.Dispatcher.Invoke(() =>
            {
                var mainWindow = new MainWindow();
                // 显示主窗口
                mainWindow.Show();
                // 转移主程序控制权
                Application.Current.MainWindow = mainWindow;
                // 关闭初始化窗口
                this.Close();
            });
        }
        catch (Exception ex)
        {
            AppendLogger($"❌ 初始化异常：{ex.Message}");
        }
    }
}
