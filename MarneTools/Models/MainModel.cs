﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace MarneTools.Models;

public partial class MainModel : ObservableObject
{
    [ObservableProperty]
    private bool isRadminRun;

    [ObservableProperty]
    private bool isMarneRun;

    [ObservableProperty]
    private bool isFrostyModRun;

    [ObservableProperty]
    private bool isOriginRun;

    [ObservableProperty]
    private bool isSteamRun;

    [ObservableProperty]
    private bool isEaAppRun;

    [ObservableProperty]
    private bool isBF1Run;
}