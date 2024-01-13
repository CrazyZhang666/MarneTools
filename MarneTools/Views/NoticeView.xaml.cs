using MarneTools.Api;
using MarneTools.Models;

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

        Task.Run(async () =>
        {
            var response = await CoreApi.GetWebActivity();
            if (!response.IsSuccess)
            {
                NoticeModel.ActivityTxt = "加载活动信息失败";
                return;
            }

            NoticeModel.ActivityTxt = response.Content;
        });

        Task.Run(async () =>
        {
            var response = await CoreApi.GetWebNotice();
            if (!response.IsSuccess)
            {
                NoticeModel.NoticeTxt = "加载通知信息失败";
                return;
            }

            NoticeModel.NoticeTxt = response.Content;
        });
    }
}
