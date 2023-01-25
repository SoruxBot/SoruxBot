namespace Sorux.Bot.Core.Interface.PluginsSDK.SDK.FutureTest.QQ.Models;

public class ImageOcrModel
{
    public List<TextDetection> texts { get; set; }

    /// <summary>
    /// 语言
    /// </summary>
    public string language { get; set; }
}