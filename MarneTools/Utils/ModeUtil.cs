namespace MarneTools.Utils;

public static class ModeUtil
{
    private readonly static Dictionary<string, string> AllGameMode = new()
    {
        { "ZoneControl0", "空降补给" },
        { "AirAssault0", "空中突袭" },
        { "TugOfWar0", "前线" },
        { "Domination0", "抢攻" },
        { "Breakthrough0", "闪击行动" },
        { "Rush0", "突袭" },
        { "TeamDeathMatch0", "团队死斗" },
        { "BreakthroughLarge0", "行动模式" },
        { "Possession0", "战争信鸽" },
        { "Conquest0", "征服" }
    };

    /// <summary>
    /// 获取游戏模式名称
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetGameModeName(string name)
    {
        if (AllGameMode.TryGetValue(name, out string value))
            return value;

        return string.Empty;
    }
}
