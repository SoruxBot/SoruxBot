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
        for (int i = 0; i < args.Length; i++)
        {
            command.Parameters.Add(new("@arg" + i, args[i]));
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
            "CREATE TABLE IF NOT EXISTS @arg0 (node varchar(255), state varchar(255))",
            tableName);
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
            $"INSERT INTO @arg0 (node, state) VALUES (@arg1,'true')",
            identity, node);
        command.ExecuteNonQuery();
        return AddNodeCondition(condition);
    }

    public bool RemovePermission(string identity, string node, string condition)
    {
        CreateTableIfNotExist(identity);
        var command = PreparedStatement(
            $"DELETE FROM @arg0 WHERE node = @arg1", 
            identity, node);
        // FIXME: NEED? command.ExecuteNonQuery();
        return RemoveNodeCondition(condition);
    }

    public string GetPersonPermissionList(string identity)
    {
        CreateTableIfNotExist(identity);
        var command = PreparedStatement(
            $"SELECT node FROM @arg1", identity);
        SQLiteDataReader sqLiteDataReader = command.ExecuteReader();
        StringBuilder stringBuilder = new StringBuilder();
        while (sqLiteDataReader.Read())
        {
            stringBuilder.Append(sqLiteDataReader[0] + "\n");
        }

        return stringBuilder.ToString();
    }
}