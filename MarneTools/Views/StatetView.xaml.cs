using MarneTools.Models;
using MarneTools.Utils;

namespace MarneTools.Views;

/// <summary>
/// StatetView.xaml 的交互逻辑
/// </summary>
public partial class StatetView : UserControl
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public StateModel StateModel { get; set; } = new();

    public StatetView()
    {
        InitializeComponent();

        // 检查目标进程状态线程
        new Thread(CheckStateThread)
        {
            Name = "CheckStateThread",
            IsBackground = true
        }.Start();
    }

    /// <summary>
    /// 检查目标进程状态线程
    /// </summary>
    private void CheckStateThread()
    {
        while (CoreUtil.IsAppRunning)
        {
            var processList = Process.GetProcesses().ToList();

            StateModel.IsMarneRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_MarneLauncher, StringComparison.OrdinalIgnoreCase)) is not null;
            StateModel.IsFrostyModRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_FrostyModManager, StringComparison.OrdinalIgnoreCase)) is not null;

            StateModel.IsOriginRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_Origin, StringComparison.OrdinalIgnoreCase)) is not null;
            StateModel.IsEaAppRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_EaApp, StringComparison.OrdinalIgnoreCase)) is not null;
            StateModel.IsSteamRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_Steam, StringComparison.OrdinalIgnoreCase)) is not null;
            StateModel.IsBF1Run = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_BF1, StringComparison.OrdinalIgnoreCase)) is not null;

            Thread.Sleep(1000);
        }
    }
}
