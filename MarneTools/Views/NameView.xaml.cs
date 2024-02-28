using MarneTools.Helper;
using MarneTools.Models;
using MarneTools.Utils;

namespace MarneTools.Views;

/// <summary>
/// NameView.xaml 的交互逻辑
/// </summary>
public partial class NameView : UserControl
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public NameModel NameModel { get; set; } = new();

    /// <summary>
    /// playername.txt 文件路径
    /// </summary>
    private string File_PlayerName;

    public NameView()
    {
        InitializeComponent();

        Task.Run(() =>
        {
            File_PlayerName = Path.Combine(CoreUtil.BF1InstallDir, "playername.txt");

            if (File.Exists(File_PlayerName))
            {
                NameModel.PlayerName = File.ReadAllText(File_PlayerName, Encoding.UTF8);
            }
        });
    }

    [RelayCommand]
    private void ChangePlayerName()
    {
        if (ProcessHelper.IsAppRun(CoreUtil.Name_BF1))
        {
            NotifierHelper.Show(NotifierType.Warning, "战地1正在运行，请关闭后再执行修改ID操作");
            return;
        }

        var playerName = NameModel.PlayerName.Trim();

        if (string.IsNullOrWhiteSpace(playerName))
        {
            NotifierHelper.Show(NotifierType.Warning, "游戏ID不能为空，请重新修改");
            return;
        }

        var nameHexBytes = Encoding.UTF8.GetBytes(playerName);
        if (nameHexBytes.Length > 15)
        {
            NotifierHelper.Show(NotifierType.Warning, "游戏ID字节数不能超过15字节，请重新修改");
            return;
        }

        try
        {
            File.WriteAllText(File_PlayerName, playerName, Encoding.UTF8);

            NotifierHelper.Show(NotifierType.Success, "修改中文游戏ID成功，请启动战地1在线模式生效");
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
}
