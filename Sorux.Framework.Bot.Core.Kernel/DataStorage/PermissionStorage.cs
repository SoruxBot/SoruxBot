using System.Data.SQLite;
using System.Text;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage;

public class PermissionStorage
{
    private SQLiteConnection _sqLiteConnection;

    private SQLiteCommand PreparedStatement(string sql, params string[] args)
    {
        var command = new SQLiteCommand(sql, _sqLiteConnection);
        foreach (var t in args)
        {
            command.Parameters.Add(t);
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
            "CREATE TABLE IF NOT EXISTS ? (node varchar(255), state varchar(255))",
            tableName);
        command.ExecuteNonQuery();
    }

    public bool AddNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        var command = PreparedStatement(
            "INSERT INTO permissionTarget (node, state) VALUES (?,'true')",
            condition);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool RemoveNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        var command = PreparedStatement(
            $"DELETE FROM permissionTarget WHERE node = ?",
            condition);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool GetNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        var command = PreparedStatement(
            $"SELECT state FROM permissionTarget WHERE node = ?",
            condition);
        object res = command.ExecuteScalar();
        return res != null && ((string)res).Equals("true");
    }

    public bool AddPermission(string identity, string node, string condition)
    {
        CreateTableIfNotExist(identity);
        var command = PreparedStatement(
            $"INSERT INTO ? (node, state) VALUES (?,'true')",
            identity, node);
        command.ExecuteNonQuery();
        return AddNodeCondition(condition);
    }

    public bool RemovePermission(string identity, string node, string condition)
    {
        CreateTableIfNotExist(identity);
        var command = PreparedStatement(
            $"DELETE FROM ? WHERE node = ?", 
            identity, node);
        // FIXME: NEED? command.ExecuteNonQuery();
        return RemoveNodeCondition(condition);
    }

    public string GetPersonPermissionList(string identity)
    {
        CreateTableIfNotExist(identity);
        var command = PreparedStatement(
            $"SELECT node FROM ?", identity);
        SQLiteDataReader sqLiteDataReader = command.ExecuteReader();
        StringBuilder stringBuilder = new StringBuilder();
        while (sqLiteDataReader.Read())
        {
            stringBuilder.Append(sqLiteDataReader[0] + "\n");
        }

        return stringBuilder.ToString();
    }
}