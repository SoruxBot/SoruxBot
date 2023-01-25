namespace Sorux.Bot.Core.Interface.PluginsSDK.Register
{
    public interface IBasicInformationRegister
    {
        string GetAuthor();
        string GetDescription();
        string GetName();
        string GetVersion();
        string GetDLL();
    }
}