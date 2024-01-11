namespace MarneTools.Utils;

public static class MiscUtil
{
    /// <summary>
    /// 文件大小转换
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string GetFileForamtSize(long size)
    {
        var kb = size / 1024.0f;

        if (kb > 1024.0f)
            return $"{kb / 1024.0f:0.00}MB";
        else
            return $"{kb:0.00}KB";
    }
}
