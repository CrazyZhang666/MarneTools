using MarneTools.Api;
using MarneTools.Utils;
using MarneTools.Models;

namespace MarneTools.Views;

/// <summary>
/// ServerView.xaml 的交互逻辑
/// </summary>
public partial class ServerView : UserControl
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public ServerModel ServerModel { get; set; } = new();

    public ServerView()
    {
        InitializeComponent();

        // 更新服务器信息线程
        new Thread(UpdateServerThread)
        {
            Name = "UpdateServerThread",
            IsBackground = true
        }.Start();
    }

    /// <summary>
    /// 更新服务器信息线程
    /// </summary>
    private async void UpdateServerThread()
    {
        while (CoreUtil.IsAppRunning)
        {
            try
            {
                var response = await CoreApi.GetServerList();
                if (response.IsSuccess)
                {
                    var jsonNode = JsonNode.Parse(response.Content)!;

                    var jsonArray = jsonNode!["servers"]!.AsArray()!;
                    foreach (var item in jsonArray)
                    {
                        var name = item!["name"]!.GetValue<string>();
                        if (!name.Equals("Lindos"))
                            continue;

                        ServerModel.Name = name;

                        ServerModel.MapName = MapUtil.GetGameMapName(item!["mapName"]!.GetValue<string>());
                        ServerModel.GameMode = ModeUtil.GetGameModeName(item!["gameMode"]!.GetValue<string>());

                        ServerModel.MapImage = MapUtil.GetGameMapImage(item!["mapName"]!.GetValue<string>());

                        ServerModel.Region = item!["region"]!.GetValue<string>();
                        ServerModel.Country = item!["country"]!.GetValue<string>();

                        ServerModel.TickRate = 60;

                        ServerModel.Player = item!["currentPlayers"]!.GetValue<int>();
                        ServerModel.MaxPlayer = item!["maxPlayers"]!.GetValue<int>();

                        var modArray = item!["modList"]!.AsArray()!;

                        ServerModel.ModName = modArray![0]!["name"]!.GetValue<string>();
                        ServerModel.ModVersion = modArray![0]!["version"]!.GetValue<string>();
                        ServerModel.ModFile = modArray![0]!["file_name"]!.GetValue<string>();
                    }
                }
            }
            catch { }

            Thread.Sleep(5000);
        }
    }
}
