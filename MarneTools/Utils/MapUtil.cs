namespace MarneTools.Utils;

public static class MapUtil
{
    private readonly static Dictionary<string, string> AllGameMap = new()
    {
        { "Levels/MP/MP_MountainFort/MP_MountainFort", "格拉巴山" },
        { "Levels/MP/MP_Forest/MP_Forest", "阿尔贡森林" },
        { "Levels/MP/MP_ItalianCoast/MP_ItalianCoast", "帝国边境" },
        { "Levels/MP/MP_Chateau/MP_Chateau", "流血宴厅" },
        { "Levels/MP/MP_Scar/MP_Scar", "圣康坦的伤痕" },
        { "Levels/MP/MP_Desert/MP_Desert", "西奈沙漠" },
        { "Levels/MP/MP_Amiens/MP_Amiens", "亚眠" },
        { "Levels/MP/MP_Suez/MP_Suez", "苏伊士" },
        { "Levels/MP/MP_FaoFortress/MP_FaoFortress", "法欧堡" },
        { "Xpack0/Levels/MP/MP_Giant/MP_Giant", "庞然暗影" },
        { "Xpack1/Levels/MP_Fields/MP_Fields", "苏瓦松" },
        { "Xpack1/Levels/MP_Graveyard/MP_Graveyard", "决裂" },
        { "Xpack1/Levels/MP_Underworld/MP_Underworld", "法乌克斯要塞" },
        { "Xpack1/Levels/MP_Verdun/MP_Verdun", "凡尔登高地" },
        { "Xpack1-3/Levels/MP_Trench/MP_Trench", "尼维尔之夜" },
        { "Xpack1-3/Levels/MP_ShovelTown/MP_ShovelTown", "攻占托尔" },
        { "Xpack2/Levels/MP/MP_Bridge/MP_Bridge", "勃鲁希洛夫关口" },
        { "Xpack2/Levels/MP/MP_Islands/MP_Islands", "阿尔比恩" },
        { "Xpack2/Levels/MP/MP_Ravines/MP_Ravines", "武普库夫山口" },
        { "Xpack2/Levels/MP/MP_Valley/MP_Valley", "加利西亚" },
        { "Xpack2/Levels/MP/MP_Tsaritsyn/MP_Tsaritsyn", "察里津" },
        { "Xpack2/Levels/MP/MP_Volga/MP_Volga", "窝瓦河" },
        { "Xpack3/Levels/MP/MP_Beachhead/MP_Beachhead", "海丽丝岬" },
        { "Xpack3/Levels/MP/MP_Harbor/MP_Harbor", "泽布吕赫" },
        { "Xpack3/Levels/MP/MP_Naval/MP_Naval", "黑尔戈兰湾" },
        { "Xpack3/Levels/MP/MP_Ridge/MP_Ridge", "阿奇巴巴" },
        { "Xpack4/Levels/MP/MP_Offensive/MP_Offensive", "索姆河" },
        { "Xpack4/Levels/MP/MP_Hell/MP_Hell", "帕斯尚尔" },
        { "Xpack4/Levels/MP/MP_River/MP_River", "卡波雷托" },
        { "Xpack4/Levels/MP/MP_Alps/MP_Alps", "剃刀边缘" },
        { "Xpack4/Levels/MP/MP_Blitz/MP_Blitz", "伦敦的呼唤：夜袭" },
        { "Xpack4/Levels/MP/MP_London/MP_London", "伦敦的呼唤：灾祸" }
    };

    private readonly static Dictionary<string, string> AllGameMapImage = new()
    {
        { "Levels/MP/MP_MountainFort/MP_MountainFort", "MP_MountainFort" },
        { "Levels/MP/MP_Forest/MP_Forest", "MP_Forest" },
        { "Levels/MP/MP_ItalianCoast/MP_ItalianCoast", "MP_ItalianCoast" },
        { "Levels/MP/MP_Chateau/MP_Chateau", "MP_Chateau" },
        { "Levels/MP/MP_Scar/MP_Scar", "MP_Scar" },
        { "Levels/MP/MP_Desert/MP_Desert", "MP_Desert" },
        { "Levels/MP/MP_Amiens/MP_Amiens", "MP_Amiens" },
        { "Levels/MP/MP_Suez/MP_Suez", "MP_Suez" },
        { "Levels/MP/MP_FaoFortress/MP_FaoFortress", "MP_FaoFortress" },
        { "Xpack0/Levels/MP/MP_Giant/MP_Giant", "MP_Giant" },
        { "Xpack1/Levels/MP_Fields/MP_Fields", "MP_Fields" },
        { "Xpack1/Levels/MP_Graveyard/MP_Graveyard", "MP_Graveyard" },
        { "Xpack1/Levels/MP_Underworld/MP_Underworld", "MP_Underworld" },
        { "Xpack1/Levels/MP_Verdun/MP_Verdun", "MP_Verdun" },
        { "Xpack1-3/Levels/MP_Trench/MP_Trench", "MP_Trench" },
        { "Xpack1-3/Levels/MP_ShovelTown/MP_ShovelTown", "MP_ShovelTown" },
        { "Xpack2/Levels/MP/MP_Bridge/MP_Bridge", "MP_Bridge" },
        { "Xpack2/Levels/MP/MP_Islands/MP_Islands", "MP_Islands" },
        { "Xpack2/Levels/MP/MP_Ravines/MP_Ravines", "MP_Ravines" },
        { "Xpack2/Levels/MP/MP_Valley/MP_Valley", "MP_Valley" },
        { "Xpack2/Levels/MP/MP_Tsaritsyn/MP_Tsaritsyn", "MP_Tsaritsyn" },
        { "Xpack2/Levels/MP/MP_Volga/MP_Volga", "MP_Volga" },
        { "Xpack3/Levels/MP/MP_Beachhead/MP_Beachhead", "MP_Beachhead" },
        { "Xpack3/Levels/MP/MP_Harbor/MP_Harbor", "MP_Harbor" },
        { "Xpack3/Levels/MP/MP_Naval/MP_Naval", "MP_Naval" },
        { "Xpack3/Levels/MP/MP_Ridge/MP_Ridge", "MP_Ridge" },
        { "Xpack4/Levels/MP/MP_Offensive/MP_Offensive", "MP_Offensive" },
        { "Xpack4/Levels/MP/MP_Hell/MP_Hell", "MP_Hell" },
        { "Xpack4/Levels/MP/MP_River/MP_River", "MP_River" },
        { "Xpack4/Levels/MP/MP_Alps/MP_Alps", "MP_Alps" },
        { "Xpack4/Levels/MP/MP_Blitz/MP_Blitz", "MP_Blitz" },
        { "Xpack4/Levels/MP/MP_London/MP_London", "MP_London" }
    };

    /// <summary>
    /// 获取游戏地图名称
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetGameMapName(string name)
    {
        if (AllGameMap.TryGetValue(name, out string value))
            return value;

        return string.Empty;
    }

    /// <summary>
    /// 获取游戏地图图片
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetGameMapImage(string name)
    {
        if (AllGameMapImage.TryGetValue(name, out string value))
            return $"\\Assets\\Maps\\{value}.jpg";

        return string.Empty;
    }
}
