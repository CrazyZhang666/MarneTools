using MarneTools.Data;
using MarneTools.Utils;
using MarneTools.Helper;

namespace MarneTools;

/// <summary>
/// LoadWindow.xaml 的交互逻辑
/// </summary>
public partial class LoadWindow : Window
{
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
            TextBox_Logger.AppendText($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}  {log}{Environment.NewLine}");
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
            var response = await HttpHelper.GetServerConfig();
            if (!response.IsSuccessful)
            {
                AppendLogger("❌ 获取服务器配置信息失败，初始化终止");
                return;
            }

            var jsonNode = JsonNode.Parse(response.Content);

            CoreUtil.ServerVersion = Version.Parse(jsonNode["Version"].GetValue<string>());

            CoreUtil.WebUpdate = jsonNode["Update"].GetValue<string>();
            CoreUtil.WebModName = jsonNode["ModName"].GetValue<string>();
            CoreUtil.WebModFile = jsonNode["ModFile"].GetValue<string>();

            AppendLogger("✔️ 获取服务器配置信息成功");
            AppendLogger($"🔔 服务器版本号：{CoreUtil.ServerVersion}");
            AppendLogger($"🔔 客户端版本号：{CoreUtil.ClientVersion}");

            // 发现新版本
            if (CoreUtil.ServerVersion > CoreUtil.ClientVersion)
            {
                AppendLogger($"📢 发现新版本，请下载最新版本");

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

            // 强制关闭FrostyModManager程序
            ProcessHelper.CloseProcessNoHit(CoreUtil.Name_FrostyModManager);
            AppendLogger("✔️ 关闭FrostyModManager程序成功");

            // 清空旧版Mod文件夹
            FileHelper.ClearDirectory(CoreUtil.Dir_FrostyMod_Mods_Bf1);
            AppendLogger("✔️ 清空旧版Mod文件夹成功");

            // 通过注册表获取战地1安装目录
            using var bf1Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\EA Games\\Battlefield 1");
            if (bf1Reg is  null)
            {
                AppendLogger("❌ 获取战地1注册表失败，初始化终止");
                return;
            }

            AppendLogger("✔️ 获取战地1注册表成功");

            var installDir = bf1Reg.GetValue("Install Dir") as string;
            if (!Directory.Exists(installDir))
            {
                AppendLogger("❌ 获取战地1注册表安装目录失败，初始化终止");
                return;
            }

            CoreUtil.BF1InstallDir = Path.GetDirectoryName(installDir);
            AppendLogger("✔️ 获取战地1注册表安装目录成功");

            AppendLogger($"🔔 此电脑战地1安装目录：{CoreUtil.BF1InstallDir}");

            var modConfig = new ModConfig();

            // 获取Mod名称
            var modName = Path.GetFileName(CoreUtil.WebModFile);
            AppendLogger($"🔔 Mod中文名称：{CoreUtil.WebModName}");
            AppendLogger($"🔔 Mod文件名称：{modName}");

            // 下载mod到 Mods\bf1 文件夹
            AppendLogger("☁️ 开始下载Mod...");
            var bytes = await HttpHelper.DownloadMod(CoreUtil.WebModFile);
            if (bytes is  null)
            {
                AppendLogger("❌ 下载Mod失败，初始化终止");
                return;
            }

            AppendLogger("✔️ 下载Mod成功");
            File.WriteAllBytes(Path.Combine(CoreUtil.Dir_FrostyMod_Mods_Bf1, modName), bytes);
            AppendLogger("✔️ 保存Mod到指定文件夹成功");

            // 设置Mod名称并启用
            modConfig.Games.bf1.Packs.Default = $"{modName}:True";

            // 设置战地1安装目录
            modConfig.Games.bf1.GamePath = CoreUtil.BF1InstallDir;

            // 写入Frosty\manager_config.json配置文件
            File.WriteAllText(CoreUtil.File_FrostyMod_Frosty_ManagerConfig, JsonHelper.JsonSerialize(modConfig));
            AppendLogger("✔️ 写入FrostyModManager配置文件成功");

            AppendLogger("👏 初始化成功，正在跳转主程序");

            ///////////////////////////////////////

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
