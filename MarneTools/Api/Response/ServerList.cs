namespace MarneTools.Api;

public class ServerList
{
    public List<ServersItem> servers { get; set; }
}

public class ServersItem
{
    public int id { get; set; }
    public string name { get; set; }
    public string mapName { get; set; }
    public string gameMode { get; set; }
    public int maxPlayers { get; set; }
    public int tickRate { get; set; }
    public int password { get; set; }
    public int needSameMods { get; set; }
    public int allowMoreMods { get; set; }
    [JsonIgnore]
    public object modList { get; set; }
    [JsonIgnore]
    public object playerList { get; set; }
    public int currentPlayers { get; set; }
    public string region { get; set; }
    public string country { get; set; }
}
