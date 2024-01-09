namespace EasyMarneTools.Helper;

public static class ProcessHelper
{
    /// <summary>
    /// 判断程序是否运行
    /// </summary>
    /// <param name="processName">程序名称，不加.exe</param>
    /// <returns></returns>
    public static bool IsAppRun(string processName)
    {
        if (string.IsNullOrWhiteSpace(processName))
            return false;

        if (processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            processName = processName[..^4];

        return Process.GetProcessesByName(processName).Length > 0;
    }

    /// <summary>
    /// 打开http链接
    /// </summary>
    /// <param name="url">http链接路径</param>
    public static void OpenLink(string url)
    {
        if (!url.StartsWith("http"))
        {
            NotifierHelper.Show(NotifierType.Error, $"目标URL不是Http格式\n{url}");
            return;
        }

        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }

    /// <summary>
    /// 打开文件夹路径
    /// </summary>
    /// <param name="dirPath">文件夹路径</param>
    public static void OpenDirectory(string dirPath)
    {
        if (!Directory.Exists(dirPath))
        {
            NotifierHelper.Show(NotifierType.Error, $"要打开的文件夹路径不存在\n{dirPath}");
            return;
        }

        try
        {
            Process.Start(new ProcessStartInfo(dirPath) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 打开指定进程，可以附带运行参数
    /// </summary>
    /// <param name="filePath">可执行文件路径</param>
    /// <param name="args">可选参数</param>
    public static void OpenProcess(string filePath, string args = "")
    {
        // 判断程序是否存在
        if (!File.Exists(filePath))
        {
            NotifierHelper.Show(NotifierType.Error, $"要打开的文件路径不存在\n{filePath}");
            return;
        }

        var fileInfo = new FileInfo(filePath);

        // 如果程序已经在运行，则退出
        if (IsAppRun(fileInfo.Name))
        {
            NotifierHelper.Show(NotifierType.Warning, $"程序 {fileInfo.Name} 已经运行了，请不要重复运行");
            return;
        }

        try
        {
            // 如果应在启动进程时使用 shell，则为 true；如果直接从可执行文件创建进程，则为 false。
            // 默认值为 true .NET Framework 应用和 false .NET Core 应用。
            var processInfo = new ProcessStartInfo
            {
                FileName = fileInfo.FullName,
                UseShellExecute = false,
                WorkingDirectory = fileInfo.DirectoryName
            };

            if (!string.IsNullOrWhiteSpace(args))
                processInfo.Arguments = args;

            Process.Start(processInfo);

            NotifierHelper.Show(NotifierType.Success, $"正在启动程序 {fileInfo.Name} 中...");
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 根据名字关闭指定程序
    /// </summary>
    /// <param name="processName">程序名字，不需要加.exe</param>
    public static void CloseProcess(string processName)
    {
        if (string.IsNullOrWhiteSpace(processName))
            return;

        if (processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            processName = processName[..^4];

        // 如果程序已经关闭，则退出
        if (!IsAppRun(processName))
        {
            NotifierHelper.Show(NotifierType.Warning, $"程序 {processName} 已经关闭了，请不要重复关闭");
            return;
        }

        try
        {
            var appProcess = Process.GetProcesses();
            foreach (var targetPro in appProcess)
            {
                if (targetPro.ProcessName.Equals(processName))
                    targetPro.Kill();
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
}
