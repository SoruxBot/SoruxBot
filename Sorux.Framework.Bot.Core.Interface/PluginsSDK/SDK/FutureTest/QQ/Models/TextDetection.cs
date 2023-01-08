namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ.Models;

public class TextDetection
{
    /// <summary>
    /// 文本
    /// </summary>
    public string text { get; set; }
    /// <summary>
    /// 置信度
    /// </summary>
    public string confidence { get; set; }
    /// <summary>
    /// 坐标
    /// </summary>
    public string coordinates { get; set; }
}