using System.Data.SQLite;
using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage;

public class PluginsDataStorage : IPluginsDataStorage
{
    private SQLiteConnection _sqLiteConnection;

    public PluginsDataStorage()
    {
        _sqLiteConnection = new SQLiteConnection("Data Source=" + DsLocalStorage.GetPluginsSqliteDataPath());
        _sqLiteConnection.Open();
    }

    private void CreateTableIfNotExist(string pluginMark)
    {
        var command = _sqLiteConnection.PreparedStatement(
            $"CREATE TABLE IF NOT EXISTS {DataTableHelper.GetTableName(pluginMark)} (key varchar(255), value varchar(255))");
        command.ExecuteNonQuery();
    }

    public bool AddStringSettings(string pluginMark, string key, string value)
    {
        CreateTableIfNotExist(pluginMark);
        var command = _sqLiteConnection.PreparedStatement(
            $"INSERT INTO {DataTableHelper.GetTableName(pluginMark)} (key, value) VALUES (@arg0,@arg1)",
            key, value);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool RemoveStringSettings(string pluginMark, string key)
    {
        CreateTableIfNotExist(pluginMark);
        var command = _sqLiteConnection.PreparedStatement(
            $"INSERT INTO {DataTableHelper.GetTableName(pluginMark)} WHERE key = @arg0",
            key);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public string GetStringSettings(string pluginMark, string key)
    {
        CreateTableIfNotExist(pluginMark);
        var command = _sqLiteConnection.PreparedStatement(
            $"SELECT VALUE FROM {DataTableHelper.GetTableName(pluginMark)} WHERE key = @arg0",
            key);
        string res = (string)command.ExecuteScalar();
        return res;
    }

    public bool EditStringSettings(string pluginMark, string key, string value)
    {
        CreateTableIfNotExist(pluginMark);
        var command = _sqLiteConnection.PreparedStatement(
            $"UPDATE {DataTableHelper.GetTableName(pluginMark)} SET value = @arg0 WHERE key = @arg1",
            key, value);
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