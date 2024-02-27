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
    /// dinput8.dll文件路径
    /// </summary>
    private string File_dinput8Dll;

    public NameView()
    {
        InitializeComponent();

        Task.Run(() =>
        {
            File_dinput8Dll = Path.Combine(CoreUtil.BF1InstallDir, "dinput8.dll");

            if (File.Exists(File_dinput8Dll))
            {
                var stream = new FileStream(File_dinput8Dll, FileMode.Open);
                var reader = new BinaryReader(stream, Encoding.UTF8, false);

                reader.BaseStream.Position = 0x5BC8;
                var bytes = reader.ReadBytes(16);

                NameModel.PlayerName = Encoding.UTF8.GetString(bytes);

                reader.Close();
                stream.Close();
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

        var playerName = NameModel.PlayerName.Trim().Replace("\0", "");

        if (string.IsNullOrWhiteSpace(playerName))
        {
            NotifierHelper.Show(NotifierType.Warning, "游戏ID不能为空，请重新修改");
            return;
        }

        var nameHexBytes = Encoding.UTF8.GetBytes(playerName);
        if (nameHexBytes.Length > 16)
        {
            NotifierHelper.Show(NotifierType.Warning, "游戏ID字节数不能超过16字节，请重新修改");
            return;
        }

        if (!File.Exists(File_dinput8Dll))
        {
            FileHelper.ExtractResFile("MarneTools.Resources.dinput8.dll", File_dinput8Dll);
        }

        var stream = new FileStream(File_dinput8Dll, FileMode.Open, FileAccess.Write);
        var writer = new BinaryWriter(stream, Encoding.UTF8, false);

        writer.BaseStream.Position = 0x5BC8;
        for (int i = 0; i < 16; i++)
        {
            if (nameHexBytes.Length > i)
            {
                writer.Write(nameHexBytes[i]);
                continue;
            }

            writer.Write(new byte[] { 0x00 });
        }

        writer.Close();
        stream.Close();

        NotifierHelper.Show(NotifierType.Success, "修改中文游戏ID成功，请离线启动战地1生效");
    }
}
