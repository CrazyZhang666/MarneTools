﻿using CommunityToolkit.Mvvm.Input;
using EasyMarneTools.Helper;
using EasyMarneTools.Utils;

namespace EasyMarneTools.Views;

/// <summary>
/// RadminView.xaml 的交互逻辑
/// </summary>
public partial class RadminView : UserControl
{
    public RadminView()
    {
        InitializeComponent();
    }

    [RelayCommand]
    private void RunRadminVPN()
    {
        // 如果找到 RadminLAN 程序，则直接运行
        if (File.Exists(CoreUtil.RadminInstallPath))
        {
            ProcessHelper.OpenProcess(CoreUtil.RadminInstallPath);
            return;
        }

        // 如果未发现 RadminLAN 程序，则提示用户安装
        if (MessageBox.Show("当前未发现用户安装 RadminLAN 程序，是否立即安装？", "警告",
            MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
        {
            ProcessHelper.OpenProcess(CoreUtil.File_RadminLAN);
        }
    }

    [RelayCommand]
    private void CloseRadminVPN()
    {
        ProcessHelper.CloseProcess(CoreUtil.Name_RadminLAN);
    }
}
