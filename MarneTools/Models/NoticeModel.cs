namespace MarneTools.Models;

public partial class NoticeModel : ObservableObject
{
    [ObservableProperty]
    private string activityTxt;

    [ObservableProperty]
    private string noticeTxt;
}
