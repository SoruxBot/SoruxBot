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
        // FIXME: SQL injection
        string sql = $"create table  if not exists {pluginMark} (key varchar(255), value varchar(255))";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        command.ExecuteNonQuery();
    }

    public bool AddStringSettings(string pluginMark, string key, string value)
    {
        CreateTableIfNotExist(pluginMark);
        // FIXME: SQL injection
        string sql = $"insert into {pluginMark} (key, value) values ('{key}','{value}')";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool RemoveStringSettings(string pluginMark, string key)
    {
        CreateTableIfNotExist(pluginMark);
        // FIXME: SQL injection
        string sql = $"delete from {pluginMark} where key = '{key}'";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public string GetStringSettings(string pluginMark, string key)
    {
        CreateTableIfNotExist(pluginMark);
        // FIXME: SQL injection
        string sql = $"select value from {pluginMark} where key = '{key}'";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        string res = (string)command.ExecuteScalar();
        return res;
    }

    public bool EditStringSettings(string pluginMark, string key, string value)
    {
        CreateTableIfNotExist(pluginMark);
        // FIXME: SQL injection
        string sql = $"update {pluginMark} set value = {value} where key = '{key}'";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
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