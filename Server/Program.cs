using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace Server;

public class Program
{
    static void Main(string[] args)
    {
        using var md5 = MD5.Create();

        using var marneDllFs = File.OpenRead(".\\Marne\\Marne.dll");
        var marneDllMd5 = md5.ComputeHash(marneDllFs);

        Console.WriteLine("Marne.dll");
        Console.WriteLine(BitConverter.ToString(marneDllMd5).Replace("-", ""));
        Console.WriteLine();

        using var marneExeFs = File.OpenRead(".\\Marne\\MarneLauncher.exe");
        var marneExeMd5 = md5.ComputeHash(marneExeFs);

        Console.WriteLine("MarneLauncher.exe");
        Console.WriteLine(BitConverter.ToString(marneExeMd5).Replace("-", ""));
        Console.WriteLine();

        using var fbmodFs = File.OpenRead(".\\Mods\\SinaiCavalryBattle-v0.10.0.0.fbmod");
        var fbmodMd5 = md5.ComputeHash(fbmodFs);

        Console.WriteLine("SinaiCavalryBattle-v0.10.0.0.fbmod");
        Console.WriteLine(BitConverter.ToString(fbmodMd5).Replace("-", ""));
        Console.WriteLine();

        Console.WriteLine("End!");
    }
}
