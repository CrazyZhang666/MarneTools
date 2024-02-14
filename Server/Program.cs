using System;
using System.IO;
using System.Security.Cryptography;

namespace Server;

public class Program
{
    static void Main(string[] args)
    {
        using var md5 = MD5.Create();

        using var fbmodFs = File.OpenRead(".\\Mods\\AllKitAndHero-2024NewYear-32v32-v3.2.0.0.fbmod");
        var fbmodMd5 = md5.ComputeHash(fbmodFs);

        Console.WriteLine("AllKitAndHero-2024NewYear-32v32-v3.2.0.0.fbmod");
        Console.WriteLine(BitConverter.ToString(fbmodMd5).Replace("-", ""));
        Console.WriteLine();

        Console.WriteLine("End!");
    }
}
