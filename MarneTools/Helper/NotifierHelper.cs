using Notification.Wpf;
using Notification.Wpf.Constants;
using Notification.Wpf.Controls;

namespace MarneTools.Helper;

public static class NotifierHelper
{
    private static readonly NotificationManager _notificationManager = new();

    private static readonly TimeSpan ExpirationTime = TimeSpan.FromSeconds(2);

    static NotifierHelper()
    {
        NotificationConstants.MessagePosition = NotificationPosition.BottomCenter;
        NotificationConstants.NotificationsOverlayWindowMaxCount = 3;

        NotificationConstants.MinWidth = 350D;
        NotificationConstants.MaxWidth = NotificationConstants.MinWidth;

        NotificationConstants.FontName = "微软雅黑";
        NotificationConstants.TitleSize = 12;
        NotificationConstants.MessageSize = 12;
        NotificationConstants.MessageTextAlignment = TextAlignment.Left;
        NotificationConstants.TitleTextAlignment = TextAlignment.Left;

        NotificationConstants.DefaultBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#444444"));
        NotificationConstants.DefaultForegroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

        NotificationConstants.InformationBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#409EFF"));
        NotificationConstants.SuccessBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#67C23A"));
        NotificationConstants.WarningBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E6A23C"));
        NotificationConstants.ErrorBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F56C6C"));
    }

    /// <summary>
    /// 显示Toast通知
    /// </summary>
    /// <param name="type">通知类型</param>
    /// <param name="message">通知消息字符串</param>
    public static void Show(NotifierType type, string message)
    {
        var title = type switch
        {
            NotifierType.None => "",
            NotifierType.Information => "信息",
            NotifierType.Success => "成功",
            NotifierType.Warning => "警告",
            NotifierType.Error => "错误",
            NotifierType.Notification => "通知",
            _ => "",
        };

        var content = new NotificationContent
        {
            Title = title,
            Message = message,
            Type = (NotificationType)type,
            TrimType = NotificationTextTrimType.NoTrim,
        };

        _notificationManager.Show(content, "MainWindowArea", ExpirationTime, null, null, true, false);
    }

    /// <summary>
    /// 显示异常通知信息
    /// </summary>
    /// <param name="ex">异常</param>
    public static void ShowException(Exception ex)
    {
        var content = new NotificationContent
        {
            Title = "错误",
            Message = $"发生未知异常\n{ex.Message}",
            Type = NotificationType.Error,
            TrimType = NotificationTextTrimType.NoTrim,
        };

        _notificationManager.Show(content, "MainWindowArea", ExpirationTime, null, null, true, false);
    }
}

public enum NotifierType
{
    /// <summary>
    /// 无
    /// </summary>
    None,
    /// <summary>
    /// 信息
    /// </summary>
    Information,
    /// <summary>
    /// 成功
    /// </summary>
    Success,
    /// <summary>
    /// 警告
    /// </summary>
    Warning,
    /// <summary>
    /// 错误
    /// </summary>
    Error,
    /// <summary>
    /// 通知
    /// </summary>
    Notification
}