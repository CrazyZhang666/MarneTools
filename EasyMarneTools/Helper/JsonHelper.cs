namespace EasyMarneTools.Helper;

public static class JsonHelper
{
    /// <summary>
    /// 反序列化配置
    /// </summary>
    private static readonly JsonSerializerOptions OptionsDeserialize = new()
    {
        IncludeFields = true,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// 序列化配置
    /// </summary>
    private static readonly JsonSerializerOptions OptionsSerialize = new()
    {
        WriteIndented = true,
        IncludeFields = true,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    /// <summary>
    /// 反序列化，将json字符串转换成json类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static T JsonDeserialize<T>(string result)
    {
        return JsonSerializer.Deserialize<T>(result, OptionsDeserialize);
    }

    /// <summary>
    /// 序列化，将json类转换成json字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonClass"></param>
    /// <returns></returns>
    public static string JsonSerialize<T>(T jsonClass)
    {
        return JsonSerializer.Serialize(jsonClass, OptionsSerialize);
    }
}