using System.Data.SQLite;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage;

public class PluginsDataStorage : IPluginsDataStorage
{
    private SQLiteConnection _sqLiteConnection;
    
    private string CleanTableName(string tableName)
        => tableName.Replace("'", "")
            .Replace("\"", "")
            .Replace(";", "")
            .Replace("#", "")
            .Replace("--", "");

    private SQLiteCommand PreparedStatement(string sql, params string[]? args)
    {
        var command = new SQLiteCommand(sql, _sqLiteConnection);
        if (args == null) return command;
        for (int i = 0; i < args.Length; i++)
        {
            command.Parameters.AddWithValue("@arg" + i, args[i]);
        }
        return command;
    }

    public PluginsDataStorage()
    {
        _sqLiteConnection = new SQLiteConnection("Data Source=" + DsLocalStorage.GetPluginsSqliteDataPath());
        _sqLiteConnection.Open();
    }

    private void CreateTableIfNotExist(string pluginMark)
    {
        
        var command =  PreparedStatement(
            $"CREATE TABLE IF NOT EXISTS {CleanTableName(pluginMark)} (key varchar(255), value varchar(255))");
        command.ExecuteNonQuery();
    }

    public bool AddStringSettings(string pluginMark, string key, string value)
    {
        CreateTableIfNotExist(pluginMark);
        var command
            = PreparedStatement($"insert into {CleanTableName(pluginMark)} (key, value) values (@arg0,@arg1)", key,
                value);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool RemoveStringSettings(string pluginMark, string key)
    {
        CreateTableIfNotExist(pluginMark);
        var command
            = PreparedStatement($"insert into {CleanTableName(pluginMark)} where key = @arg0", key);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public string GetStringSettings(string pluginMark, string key)
    {
        CreateTableIfNotExist(pluginMark);
        var command 
            = PreparedStatement($"select value from {CleanTableName(pluginMark)} where key = @arg0",key);
        string res = (string)command.ExecuteScalar();
        return res;
    }

    public bool EditStringSettings(string pluginMark, string key, string value)
    {
        CreateTableIfNotExist(pluginMark);
        var command 
            = PreparedStatement($"update {CleanTableName(pluginMark)} set value = @arg0 where key = @arg1",
                key,value);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool AddBinarySettings(string pluginMark, string key, byte[] value)
    {
        var dic = DsLocalStorage.GetPluginsDataDirectory(pluginMark);
        if (!Directory.Exists(dic)) Directory.CreateDirectory(dic);
        File.WriteAllBytes(DsLocalStorage.GetPluginsDataFile(pluginMark, key.GetSha256()), value);
        return true;
    }

    public bool RemoveBinarySettings(string pluginMark, string key)
    {
        var path = DsLocalStorage.GetPluginsDataFile(pluginMark, key.GetSha256());

        if (!File.Exists(path)) return false;

        File.Delete(path);
        return true;
    }

    public byte[]? GetBinarySettings(string pluginMark, string key)
    {
        var path = DsLocalStorage.GetPluginsDataFile(pluginMark, key.GetSha256());

        if (!File.Exists(path)) return null;

        return File.ReadAllBytes(path);
    }

    public bool EditBinarySettings(string pluginMark, string key, byte[] value)
    {
        if (!RemoveBinarySettings(pluginMark, key)) return false;

        AddBinarySettings(pluginMark, key, value);
        return true;
    }
}