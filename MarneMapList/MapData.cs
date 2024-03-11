using System.Collections.Generic;

namespace MarneMapList;

public static class MapData
{
    public static Dictionary<string, string[]> MapAndModeInfo = [];

    static MapData()
    {
        MapAndModeInfo.Add("MP_Amiens", ["Conquest0", "Rush0", "BreakThrough0", "BreakThroughLarge0", "Possession0", "TugOfWar0", "Domination0", "TeamDeathMatch0"]);
        MapAndModeInfo.Add("MP_Chateau", ["Conquest0", "Rush0", "BreakThrough0", "BreakThroughLarge0", "Possession0", "TugOfWar0", "Domination0", "TeamDeathMatch0"]);
        MapAndModeInfo.Add("MP_Desert", ["Conquest0", "Rush0", "BreakThrough0", "BreakThroughLarge0", "Possession0", "TugOfWar0", "Domination0", "TeamDeathMatch0"]);
    }
}
