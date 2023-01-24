namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;

/// <summary>
/// MessageEntity 的帮助类，用于辅助处理图片信息
/// </summary>
public static class MessageEntityExtension
{
    /// <summary>
    /// 将 Meta 中的图片的 Url 进行提取，并且根据实际消息中的顺序写入 List 中
    /// </summary>
    /// <returns></returns>
    public static List<string> GetMessagePictureUrl(this MessageEntity messageEntity)
    {
        throw new NotImplementedException();
    }
}