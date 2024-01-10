using EasyMarneTools.Helper;

namespace EasyMarneTools.Utils;

public static class CoreUtil
{
    /// <summary>
    /// 主程序是否在运行，用于结束线程内循环
    /// </summary>
    public static bool IsAppRunning = true;

    /// <summary>
    /// 主窗口标题
    /// </summary>
    public const string MainAppWindowName = "Easy Marne Mod 启动器 - v";

    /// <summary>
    /// 程序服务端版本号，如：1.2.3.4
    /// </summary>
    public static Version ServerVersion = Version.Parse("0.0.0.0");

    /// <summary>
    /// 程序客户端版本号，如：1.2.3.4
    /// </summary>
    public static readonly Version ClientVersion = Application.ResourceAssembly.GetName().Version;

    /// <summary>
    /// 战地1安装目录
    /// </summary>
    public static string BF1InstallDir { get; set; }

    public static string WebUpdate { get; set; }
    public static string WebModName { get; set; }
    public static string WebModDownload { get; set; }

    public const string RadminInstallPath = "C:\\Program Files (x86)\\Radmin VPN\\RvRvpnGui.exe";

    public const string Name_RadminLAN = "RvRvpnGui";
    public const string Name_FrostyModManager = "FrostyModManager";
    public const string Name_MarneLauncher = "MarneLauncher";

    public const string Name_BattlefieldChat = "BattlefieldChat";

    public const string Name_Origin = "Origin";
    public const string Name_EaApp = "EADesktop";
    public const string Name_Steam = "steam";
    public const string Name_BF1 = "bf1";

    public static string Dir_AppData { get; private set; }

    public static string File_RadminLAN { get; private set; }
    public static string File_FrostyModManager { get; private set; }
    public static string File_MarneLauncher { get; private set; }
    public static string File_BattlefieldChat { get; private set; }

    public static string Dir_FrostyModManager_Mods_Bf1 { get; private set; }

    public static string Dir_LocalApplicationData { get; private set; }

    public static string Dir_Local_Frosty { get; private set; }
    public static string File_Local_Frosty_ManagerConfig { get; private set; }      // manager_config.json

    static CoreUtil()
    {
        Dir_AppData = ".\\AppData\\";

        File_RadminLAN = Path.Combine(Dir_AppData, "__Installer\\Radmin_LAN_1.4.4642.1.exe");
        File_FrostyModManager = Path.Combine(Dir_AppData, "FrostyModManager\\FrostyModManager.exe");
        File_MarneLauncher = Path.Combine(Dir_AppData, "Marne\\MarneLauncher.exe");
        File_BattlefieldChat = Path.Combine(Dir_AppData, "Tools\\BattlefieldChat.exe");

        Dir_FrostyModManager_Mods_Bf1 = Path.Combine(Dir_AppData, "FrostyModManager\\Mods\\bf1\\");

        Dir_LocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        Dir_Local_Frosty = Path.Combine(Dir_LocalApplicationData, "Frosty\\");
        File_Local_Frosty_ManagerConfig = Path.Combine(Dir_Local_Frosty, "manager_config.json");

        // 如果用户第一次使用FrostyMod，这个文件夹需要创建，以便生成配置文件
        FileHelper.CreateDirectory(Dir_Local_Frosty);
    }
}
