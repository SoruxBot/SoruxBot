using Sorux.Bot.Provider.CqHttp.Models.Enum;

namespace Sorux.Bot.Provider.CqHttp.Models;

public class Meta
{
    public long time { get; init; }

    public long self_id { get; init; }

    public PostType post_type { get; init; }
}