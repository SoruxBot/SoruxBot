using System.Data.SQLite;
using System.Security.Cryptography;
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
        string sql = "create table  if not exists " + pluginMark + " (key varchar(255), value varchar(255))";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        command.ExecuteNonQuery();
    }

    public bool AddStringSettings(string pluginMark, string key, string value)
    {
        CreateTableIfNotExist(pluginMark);
        string sql = $"insert into {pluginMark} (key, value) values ('{key}','{value}')";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool RemoveStringSettings(string pluginMark, string key)
    {
        CreateTableIfNotExist(pluginMark);
        string sql = $"delete from {pluginMark} where key = '{key}'";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public string GetStringSettings(string pluginMark, string key)
    {
        CreateTableIfNotExist(pluginMark);
        string sql = $"select value from {pluginMark} where key = '{key}'";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        string res = (string)command.ExecuteScalar();
        return res;
    }

    public bool EditStringSettings(string pluginMark, string key, string value)
    {
        CreateTableIfNotExist(pluginMark);
        string sql = $"update {pluginMark} set value = {value} where key = '{key}'";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool AddBinarySettings(string pluginMark, string key, byte[] value)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(DsLocalStorage.GetPluginsDataDirectory(pluginMark));
        if (!directoryInfo.Exists)
            directoryInfo.Create();
        File.WriteAllBytes(DsLocalStorage.GetPluginsDataFile(pluginMark, key.GetSha256()), value);
        return true;
    }

    public bool RemoveBinarySettings(string pluginMark, string key)
    {
        FileInfo fileInfo = new FileInfo(DsLocalStorage.GetPluginsDataFile(pluginMark, key.GetSha256()));
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
            return true;
        }
        else
        {
            return false;
        }
    }

    public byte[]? GetBinarySettings(string pluginMark, string key)
    {
        FileInfo fileInfo = new FileInfo(DsLocalStorage.GetPluginsDataFile(pluginMark, key.GetSha256()));
        if (fileInfo.Exists)
        {
            return File.ReadAllBytes(DsLocalStorage.GetPluginsDataFile(pluginMark, key.GetSha256()));
        }
        else
        {
            return null;
        }

        throw new NotImplementedException();
    }

    public bool EditBinarySettings(string pluginMark, string key, byte[] value)
    {
        if (RemoveBinarySettings(pluginMark, key))
        {
            AddBinarySettings(pluginMark, key, value);
            return true;
        }
        else
        {
            return false;
        }
    }
}