namespace MarneTools.Api;

public class RespContent
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode HttpCode { get; set; }
    public string Content { get; set; }
}
