namespace MarneTools.Models;

public partial class ServerModel : ObservableObject
{
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string mapName;

    [ObservableProperty]
    private string gameMode;

    [ObservableProperty]
    private string mapImage;

    [ObservableProperty]
    private string region;

    [ObservableProperty]
    private string country;

    [ObservableProperty]
    private int tickRate;

    [ObservableProperty]
    private int player;

    [ObservableProperty]
    private int maxPlayer;

    [ObservableProperty]
    private string modName;

    [ObservableProperty]
    private string modVersion;

    [ObservableProperty]
    private string modFile;
}
