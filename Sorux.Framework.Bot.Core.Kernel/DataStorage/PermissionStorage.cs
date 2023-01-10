using System.Data.SQLite;
using System.Text;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage;

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
        string sql = "create table  if not exists "+ tableName +" (node varchar(255), state varchar(255))";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        command.ExecuteNonQuery();
    }
    
    public bool AddNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        string sql = $"insert into permissionTarget (node, state) values ('{condition}','true')";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool RemoveNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        string sql = $"delete from permissionTarget where node = '{condition}'";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        int res = command.ExecuteNonQuery();
        return res == 1;
    }

    public bool GetNodeCondition(string condition)
    {
        CreateTableIfNotExist("permissionTarget");
        string sql = $"select state from permissionTarget where node = '{condition}'";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        object res = command.ExecuteScalar();
        return res != null && ((string)res).Equals("true");
    }

    public bool AddPermission(string identity, string node,string condition)
    {
        CreateTableIfNotExist(identity);
        string sql = $"insert into {identity} (node, state) values ('{node}','true')";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        command.ExecuteNonQuery();
        return AddNodeCondition(condition);
    }

    public bool RemovePermission(string identity, string node, string condition)
    {
        CreateTableIfNotExist(identity);
        string sql = $"delete from {identity} where node = '{node}'";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        return RemoveNodeCondition(condition);
    }
    
    public string GetPersonPermissionList(string identity)
    {
        CreateTableIfNotExist(identity);
        string sql = $"select node from {identity}";
        SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
        SQLiteDataReader sqLiteDataReader = command.ExecuteReader();
        StringBuilder stringBuilder = new StringBuilder();
        while (sqLiteDataReader.Read())
        {
            stringBuilder.Append(sqLiteDataReader[0] + "\n");
        }
        return stringBuilder.ToString();
    }
}