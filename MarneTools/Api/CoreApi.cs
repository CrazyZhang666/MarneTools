using RestSharp;

namespace MarneTools.Api;

public static class CoreApi
{
    private static readonly RestClient restClient;

    static CoreApi()
    {
        // 不抛出相关错误
        var options = new RestClientOptions()
        {
            MaxTimeout = 5000,
            ThrowOnAnyError = false,
            ThrowOnDeserializationError = false
        };

        restClient = new RestClient(options);
    }

    /// <summary>
    /// 通用Get请求
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<RespContent> GetAsync(string url)
    {
        var respContent = new RespContent();

        try
        {
            var request = new RestRequest($"https://45.12.53.1{url}");
            request.AddHeader("Host", "api.battlefield.vip");

            var response = await restClient.ExecuteGetAsync(request);

            respContent.IsSuccess = response.StatusCode == HttpStatusCode.OK;
            respContent.HttpCode = response.StatusCode;
            respContent.Content = response.Content;
        }
        catch (Exception ex)
        {
            respContent.Content = ex.Message;
        }

        return respContent;
    }

    /// <summary>
    /// 获取网络配置文件
    /// </summary>
    /// <returns></returns>
    public static async Task<RespContent> GetWebConfig()
    {
        return await GetAsync("/files/config.json");
    }

    /// <summary>
    /// 获取网络活动文件
    /// </summary>
    /// <returns></returns>
    public static async Task<RespContent> GetWebActivity()
    {
        return await GetAsync("/files/activity.txt");
    }

    /// <summary>
    /// 获取网络通知文件
    /// </summary>
    /// <returns></returns>
    public static async Task<RespContent> GetWebNotice()
    {
        return await GetAsync("/files/notice.txt");
    }

    /// <summary>
    /// 获取服务器列表
    /// </summary>
    /// <returns></returns>
    public static async Task<RespContent> GetServerList()
    {
        return await GetAsync("/server");
    }
}
