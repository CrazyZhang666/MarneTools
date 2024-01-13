using MarneTools.Api;
using MarneTools.Models;
using MarneTools.Utils;

namespace MarneTools.Views;

/// <summary>
/// NoticeView.xaml 的交互逻辑
/// </summary>
public partial class NoticeView : UserControl
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public NoticeModel NoticeModel { get; set; } = new();

    public NoticeView()
    {
        InitializeComponent();

        NoticeModel.ActivityTxt = "加载中...";
        NoticeModel.NoticeTxt = "加载中...";

        // 更新服务器通知消息线程
        new Thread(UpdateNoticeThread)
        {
            Name = "UpdateNoticeThread",
            IsBackground = true
        }.Start();
    }

    /// <summary>
    /// 更新服务器通知消息线程
    /// </summary>
    private async void UpdateNoticeThread()
    {
        while (CoreUtil.IsAppRunning)
        {
            try 
            {
                var responseAct = await CoreApi.GetWebActivity();
                NoticeModel.ActivityTxt = responseAct.IsSuccess ? responseAct.Content : "加载活动信息失败";

                var responseNot = await CoreApi.GetWebNotice();
                NoticeModel.NoticeTxt = responseNot.IsSuccess ? responseNot.Content : "加载通知信息失败";

            } catch { }

            Thread.Sleep(10000);
        }
    }
}
