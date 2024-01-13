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
    private int tickRate;

    [ObservableProperty]
    private int player;

    [ObservableProperty]
    private int maxPlayer;

    [ObservableProperty]
    private int ping;

    [ObservableProperty]
    private string pingImage;
}
