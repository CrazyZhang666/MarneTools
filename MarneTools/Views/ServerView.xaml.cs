using MarneTools.Api;
using MarneTools.Utils;
using MarneTools.Models;
using MarneTools.Helper;
using System.Xml.Linq;
using System.Windows;

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

        InitData("正在获取服务器名称...");
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
                var result = await CoreApi.GetServerList();
                if (result.IsSuccess)
                {
                    var serverList = JsonHelper.JsonDeserialize<ServerList>(result.Content);

                    foreach (var server in serverList.servers)
                    {
                        if (!server.name.Equals("DICE SB"))
                            continue;

                        ServerModel.MapImage = MapUtil.GetGameMapImage(server.mapName);
                        ServerModel.Name = $"{server.name} - {server.region} - {server.country}";

                        ServerModel.MapName = MapUtil.GetGameMapName(server.mapName);
                        ServerModel.GameMode = ModeUtil.GetGameModeName(server.gameMode);
                        ServerModel.TickRate = 60;

                        ServerModel.Player = server.currentPlayers;
                        ServerModel.MaxPlayer = server.maxPlayers;

                        ServerModel.Delay = new Random().Next(23, 35);
                    }
                }
                else
                {
                    InitData("网络异常，等待下次重试中...");
                }
            }
            catch { }

            Thread.Sleep(10000);
        }
    }

    private void InitData(string name)
    {
        ServerModel.MapImage = "\\Assets\\Maps\\MP_Desert.jpg";
        ServerModel.Name = name;

        ServerModel.MapName = "地图名称";
        ServerModel.GameMode = "游戏模式";
        ServerModel.TickRate = 60;

        ServerModel.Player = 0;
        ServerModel.MaxPlayer = 0;

        ServerModel.Delay = 999;
    }
}
