using MarneTools.Utils;
using MarneTools.Models;

namespace MarneTools;

/// <summary>
/// MainWindow.xaml 的交互逻辑
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// 主窗口数据模型
    /// </summary>
    public MainModel MainModel { get; set; } = new();

    /// <summary>
    /// 页面导航字典
    /// </summary>
    private readonly Dictionary<string, UserControl> NavDictionary = [];

    /////////////////////////////////////////

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Navigate("LaunchView");

        // 检查目标进程是否运行线程
        new Thread(CheckProcessIsRunThread)
        {
            Name = "CheckProcessIsRunThread",
            IsBackground = true
        }.Start();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        CoreUtil.IsAppRunning = false;
    }

    /// <summary>
    /// View页面导航
    /// </summary>
    /// <param name="viewName">页面名称</param>
    [RelayCommand]
    private void Navigate(string viewName)
    {
        if (!NavDictionary.TryGetValue(viewName, out UserControl value))
        {
            var viewType = Type.GetType($"MarneTools.Views.{viewName}");
            if (viewType == null)
                return;

            if (Activator.CreateInstance(viewType) is not UserControl userControl)
                return;

            value = userControl;
            NavDictionary.Add(viewName, value);
        }

        if (ContentControl_NavRegion.Content == value)
            return;

        ContentControl_NavRegion.Content = value;
    }

    /// <summary>
    /// 检查目标进程是否运行线程
    /// </summary>
    private void CheckProcessIsRunThread()
    {
        while (CoreUtil.IsAppRunning)
        {
            var processList = Process.GetProcesses().ToList();

            MainModel.IsRadminRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_RadminLAN, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsMarneRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_MarneLauncher, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsFrostyModRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_FrostyModManager, StringComparison.OrdinalIgnoreCase)) is not null;

            MainModel.IsOriginRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_Origin, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsEaAppRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_EaApp, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsSteamRun = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_Steam, StringComparison.OrdinalIgnoreCase)) is not null;
            MainModel.IsBF1Run = processList.Find(x => x.ProcessName.Equals(CoreUtil.Name_BF1, StringComparison.OrdinalIgnoreCase)) is not null;

            Thread.Sleep(1000);
        }
    }
}