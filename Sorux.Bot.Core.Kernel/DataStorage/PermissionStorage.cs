using System.Data.SQLite;
using System.Text;

namespace Sorux.Bot.Core.Kernel.DataStorage;

public class PermissionStorage
{
    private SQLiteConnection _sqLiteConnection;

    public PermissionStorage()
    {
        _sqLiteConnection = new SQLiteConnection("Data Source=" + DsLocalStorage.GetPluginsPermissionDataPath());
        _sqLiteConnection.Open();
    }

    private void CreateTableIfNotExist(string tableName)
    {
        // preparestatement
        var command = _sqLiteConnection.PreparedStatement(
            $"CREATE TABLE IF NOT EXISTS {DataTableHelper.GetTableName(tableName)} (node varchar(255), state varchar(255))");

        command.ExecuteNonQuery();
    }

    public bool AddNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        var command = _sqLiteConnection.PreparedStatement(
            "INSERT INTO permissionTarget (node, state) VALUES (@arg0,'true')",
            condition);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool RemoveNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        var command = _sqLiteConnection.PreparedStatement(
            $"DELETE FROM permissionTarget WHERE node = @arg0",
            condition);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool GetNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        var command = _sqLiteConnection.PreparedStatement(
            $"SELECT state FROM permissionTarget WHERE node = @arg0",
            condition);
        object res = command.ExecuteScalar();
        return res != null && ((string)res).Equals("true");
    }

    public bool AddPermission(string identity, string node, string condition)
    {
        CreateTableIfNotExist(identity);
        var command = _sqLiteConnection.PreparedStatement(
            $"INSERT INTO {DataTableHelper.GetTableName(identity)} (node, state) VALUES (@arg0,true)", node);
        command.ExecuteNonQuery();
        return AddNodeCondition(condition);
    }

    public bool RemovePermission(string identity, string node, string condition)
    {
        CreateTableIfNotExist(identity);
        var command = _sqLiteConnection.PreparedStatement(
            $"DELETE FROM {DataTableHelper.GetTableName(identity)} WHERE node = @arg0", node);
        // FIXME: NEED? command.ExecuteNonQuery();
        return RemoveNodeCondition(condition);
    }

    public string GetPersonPermissionList(string identity)
    {
        CreateTableIfNotExist(identity);
        var command = _sqLiteConnection.PreparedStatement(
            $"SELECT node FROM {DataTableHelper.GetTableName(identity)}");
        SQLiteDataReader sqLiteDataReader = command.ExecuteReader();
        StringBuilder stringBuilder = new StringBuilder();
        while (sqLiteDataReader.Read())
        {
            stringBuilder.Append(sqLiteDataReader[0] + "\n");
        }

        return stringBuilder.ToString();
    }
}