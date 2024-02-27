using MarneTools.Helper;
using MarneTools.Models;
using MarneTools.Utils;

namespace MarneTools.Views;

/// <summary>
/// FooterView.xaml 的交互逻辑
/// </summary>
public partial class FooterView : UserControl
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public FooterModel FooterModel { get; set; } = new();

    public FooterView()
    {
        InitializeComponent();

        FooterModel.Version = $"v{CoreUtil.ClientVersion}";
    }

    /// <summary>
    /// 超链接请求导航事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessHelper.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }
}
