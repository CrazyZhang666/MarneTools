namespace EasyMarneTools.Helper;

public static class FileUtil
{
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

    static FileUtil()
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
        CreateDirectory(Dir_Local_Frosty);
    }

    /// <summary>
    /// 创建文件夹，如果文件夹存在则不创建
    /// </summary>
    /// <param name="dirPath"></param>
    public static void CreateDirectory(string dirPath)
    {
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);
    }
}
