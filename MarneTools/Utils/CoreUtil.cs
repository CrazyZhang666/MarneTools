namespace MarneTools.Utils;

public static class CoreUtil
{
    public static bool IsAppRunning { get; set; } = true;
    public static Version ClientVersion { get; private set; }

    public static bool IsLanServer { get; set; }
    public static string PosterUrl { get; set; }

    ///////////////////////////////////////////////

    public static string BF1InstallDir { get; set; }

    public const string RadminInstallPath = "C:\\Program Files (x86)\\Radmin VPN\\RvRvpnGui.exe";

    public const string Name_RadminLAN = "RvRvpnGui";
    public const string Name_FrostyModManager = "FrostyModManager";
    public const string Name_MarneLauncher = "MarneLauncher";

    public const string Name_BattlefieldChat = "BattlefieldChat";

    public const string Name_Origin = "Origin";
    public const string Name_EaApp = "EADesktop";
    public const string Name_Steam = "steam";
    public const string Name_BF1 = "bf1";

    ///////////////////////////////////////////////

    public static string Dir_AppData { get; private set; }

    public static string Dir_FrostyMod { get; private set; }
    public static string Dir_Marne { get; private set; }

    ///////////////////////////////////////////////

    public static string Dir_FrostyMod_Frosty { get; private set; }
    public static string Dir_FrostyMod_Mods_Bf1 { get; private set; }

    public static string File_FrostyMod_FrostyModManager { get; private set; }
    public static string File_FrostyMod_Frosty_ManagerConfig { get; private set; }

    public static string File_Marne_MarneDll { get; private set; }
    public static string File_Marne_MarneLauncher { get; private set; }

    public static string File_RadminLAN { get; private set; }
    public static string File_BattlefieldChat { get; private set; }

    static CoreUtil()
    {
        ClientVersion = Application.ResourceAssembly.GetName().Version;

        ///////////////////////////////////////////////

        Dir_AppData = Path.Combine(Environment.CurrentDirectory, "AppData\\");

        Dir_FrostyMod = Path.Combine(Dir_AppData, "FrostyMod\\");
        Dir_Marne = Path.Combine(Dir_AppData, "Marne\\");

        ///////////////////////////////////////////////

        Dir_FrostyMod_Frosty = Path.Combine(Dir_FrostyMod, "Frosty\\");
        Dir_FrostyMod_Mods_Bf1 = Path.Combine(Dir_FrostyMod, "Mods\\bf1\\");

        File_FrostyMod_FrostyModManager = Path.Combine(Dir_FrostyMod, "FrostyModManager.exe");
        File_FrostyMod_Frosty_ManagerConfig = Path.Combine(Dir_FrostyMod_Frosty, "manager_config.json");

        File_Marne_MarneDll = Path.Combine(Dir_Marne, "Marne.dll");
        File_Marne_MarneLauncher = Path.Combine(Dir_Marne, "MarneLauncher.exe");

        File_RadminLAN = Path.Combine(Dir_AppData, "__Installer\\Radmin_LAN_1.4.4642.1.exe");
        File_BattlefieldChat = Path.Combine(Dir_AppData, "Tools\\BattlefieldChat.exe");
    }
}
