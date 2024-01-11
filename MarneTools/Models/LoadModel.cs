namespace MarneTools.Models;

public partial class LoadModel : ObservableObject
{
    [ObservableProperty]
    private double totalSize;

    [ObservableProperty]
    private double receiveSize;

    [ObservableProperty]
    private string downloadState;

    [ObservableProperty]
    private double progressValue;
}
