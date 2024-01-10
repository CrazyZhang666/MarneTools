namespace MarneTools.UI.Controls;

public class StateCard : Control
{
    /// <summary>
    /// 状态图片
    /// </summary>
    public ImageSource Source
    {
        get { return (ImageSource)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(ImageSource), typeof(StateCard), new PropertyMetadata(default));

    /// <summary>
    /// 状态标题
    /// </summary>
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(StateCard), new PropertyMetadata(default));

    /// <summary>
    /// 是否运行
    /// </summary>
    public bool IsRunning
    {
        get { return (bool)GetValue(IsRunningProperty); }
        set { SetValue(IsRunningProperty, value); }
    }
    public static readonly DependencyProperty IsRunningProperty =
        DependencyProperty.Register("IsRunning", typeof(bool), typeof(StateCard), new PropertyMetadata(default));
}
