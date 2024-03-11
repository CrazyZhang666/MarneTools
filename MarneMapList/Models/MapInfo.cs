using System.Collections.Generic;

namespace MarneMapList.Models;

public class MapInfo
{
    public string DLC { get; set; }
    public string Name { get; set; }
    public string ChsName { get; set; }
    public string Url { get; set; }
    public List<string> Mode { get; set; }

    public MapInfo()
    {
        Mode = [];
    }
}
