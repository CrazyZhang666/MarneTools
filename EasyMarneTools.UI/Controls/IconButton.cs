﻿namespace EasyMarneTools.UI.Controls;

public class IconButton : Button
{
    /// <summary>
    /// Icon图标
    /// </summary>
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(string), typeof(IconButton), new PropertyMetadata(null));
}