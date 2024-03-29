﻿namespace MarneTools.Data;

public class ModConfig
{
    public Games Games { get; set; }
    public GlobalOptions GlobalOptions { get; set; }

    public ModConfig()
    {
        GlobalOptions = new GlobalOptions();
        Games = new Games();
    }
}

public class Games
{
    public Bf1 bf1 { get; set; }

    public Games()
    {
        bf1 = new Bf1();
    }
}

public class GlobalOptions
{
    public string ApplyModOrder { get; set; } = "List";
    public bool UseDefaultProfile { get; set; } = false;
    public bool DeleteCollectionMods { get; set; } = true;
    public bool UpdateCheck { get; set; } = true;
    public bool UpdateCheckPrerelease { get; set; } = false;
    public string MaxCasFileSize { get; set; } = "1GB";
}

public class Bf1
{
    public string GamePath { get; set; } = "D:\\Origin Games\\Battlefield 1";
    public string BookmarkDb { get; set; } = "[Asset Bookmarks]|[Legacy Bookmarks]";
    public Options Options { get; set; }
    public Packs Packs { get; set; }

    public Bf1()
    {
        Options = new Options();
        Packs = new Packs();
    }
}

public class Options
{
    public string SelectedPack { get; set; } = "Default";
    public string CommandLineArgs { get; set; } = "";
}

public class Packs
{
    public string @Default { get; set; } = "mod_name.fbmod:True";
}