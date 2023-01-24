using System.Data.SQLite;
using System.Text;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage;

public class PermissionStorage
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

    public PermissionStorage()
    {
        _sqLiteConnection = new SQLiteConnection("Data Source=" + DsLocalStorage.GetPluginsPermissionDataPath());
        _sqLiteConnection.Open();
    }

    private void CreateTableIfNotExist(string tableName)
    {
        // preparestatement
        var command =  PreparedStatement(
            $"CREATE TABLE IF NOT EXISTS {CleanTableName(tableName)} (node varchar(255), state varchar(255))");
        
        command.ExecuteNonQuery();
    }

    public bool AddNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        var command = PreparedStatement(
            "INSERT INTO permissionTarget (node, state) VALUES (@arg0,'true')",
            condition);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool RemoveNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        var command = PreparedStatement(
            $"DELETE FROM permissionTarget WHERE node = @arg0",
            condition);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool GetNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        var command = PreparedStatement(
            $"SELECT state FROM permissionTarget WHERE node = @arg0",
            condition);
        object res = command.ExecuteScalar();
        return res != null && ((string)res).Equals("true");
    }

    public bool AddPermission(string identity, string node, string condition)
    {
        CreateTableIfNotExist(identity);
        var command = PreparedStatement(
            $"INSERT INTO {CleanTableName(identity)} (node, state) VALUES (@arg0,'true')",node);
        command.ExecuteNonQuery();
        return AddNodeCondition(condition);
    }

    public bool RemovePermission(string identity, string node, string condition)
    {
        CreateTableIfNotExist(identity);
        var command = PreparedStatement(
            $"DELETE FROM {CleanTableName(identity)} WHERE node = @arg0",node);
        // FIXME: NEED? command.ExecuteNonQuery();
        return RemoveNodeCondition(condition);
    }

    public string GetPersonPermissionList(string identity)
    {
        CreateTableIfNotExist(identity);
        var command = PreparedStatement(
            $"SELECT node FROM {CleanTableName(identity)}");
        SQLiteDataReader sqLiteDataReader = command.ExecuteReader();
        StringBuilder stringBuilder = new StringBuilder();
        while (sqLiteDataReader.Read())
        {
            stringBuilder.Append(sqLiteDataReader[0] + "\n");
        }

        return stringBuilder.ToString();
    }
}