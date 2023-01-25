using System.Collections.Concurrent;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage;

public static class DataTableHelper
{
    private static ConcurrentDictionary<string, string> _cache = new();
    private const string PREFIX = "X";

    private static string XSha256(string str)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
        var sb = new StringBuilder();
        sb.Append(PREFIX);
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    public static string GetTableName(string tableName)
    {
        if (_cache.ContainsKey(tableName)) return _cache[tableName];
        var name = "X" + XSha256(tableName);
        _cache.TryAdd(tableName, name);
        return name;
    }

    public static SQLiteCommand PreparedStatement(this SQLiteConnection con, string sql, params string[]? args)
    {
        var command = new SQLiteCommand(sql, con);
        if (args == null) return command;
        for (int i = 0; i < args.Length; ++i)
        {
            command.Parameters.AddWithValue("@arg" + i, args[i]);
        }

        return command;
    }
}