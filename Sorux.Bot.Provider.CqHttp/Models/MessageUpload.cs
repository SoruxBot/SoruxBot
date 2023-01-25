namespace Sorux.Bot.Provider.CqHttp.Models;

public class MessageUpload : Meta
{
    public string message_type { get; init; }

    public string sub_type { get; init; }

    public long message_id { get; init; }

    public long user_id { get; init; }

    public string message { get; init; }

    public string raw_message { get; init; }

    public int font { get; init; } = 0;

    public Sender sender { get; init; }
}