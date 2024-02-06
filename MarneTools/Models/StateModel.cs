namespace MarneTools.Models;

public partial class StateModel : ObservableObject
{
    [ObservableProperty]
    private bool isFrostyModRun;

    [ObservableProperty]
    private bool isMarneRun;

    [ObservableProperty]
    private bool isOriginRun;

    [ObservableProperty]
    private bool isSteamRun;

    [ObservableProperty]
    private bool isEaAppRun;

    [ObservableProperty]
    private bool isBF1Run;

    ///////////////////////

    [ObservableProperty]
    private string version;
}
