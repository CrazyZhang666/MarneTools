using RestSharp;

namespace MarneTools.Helper;

public static class HttpHelper
{
    private static readonly RestClient _client = null;

    static HttpHelper()
    {
        if (_client != null)
            return;

        // 不抛出相关错误
        var options = new RestClientOptions()
        {
            MaxTimeout = 5000,
            ThrowOnAnyError = false,
            ThrowOnDeserializationError = false
        };

        _client = new RestClient(options);
    }

    public static async Task<RestResponse> GetServerConfig()
    {
        var request = new RestRequest("https://battlefield.vip/marne/config.json");
        return await _client.ExecuteGetAsync(request);
    }

    public static async Task<byte[]> DownloadMod(string modAddress)
    {
        var request = new RestRequest(modAddress);
        return await _client.DownloadDataAsync(request);
    }
}
