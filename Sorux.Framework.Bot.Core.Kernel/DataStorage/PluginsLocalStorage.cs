using Sorux.Framework.Bot.Core.Kernel.Interface;
using System.Data;
using Sorux.Framework.Bot.Core.Interface.PluginsSDK.Models;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Utils;

namespace Sorux.Framework.Bot.Core.Kernel.DataStorage
{
    public class PluginsLocalStorage : IPluginsStorage
    {
        private ILoggerService _loggerService;
        private BotContext _botContext;
        /// <summary>
        /// 内置数据库，用于存储：内存中的插件信息、各个事件的插件队列
        /// </summary>
        private static DataSet _dataSet = new DataSet();
        private int lastPrivilege = 0;
        private DataTable pluginsInformationTable;
        public PluginsLocalStorage(BotContext context, ILoggerService loggerService)
        {
            this._botContext = context;
            this._loggerService = loggerService;
            _loggerService.Info("PluginsLocalStorage","Built-in Plugins Local Storage: version:1.0.0");
            //初始化数据库
            InitDataSet();
        }

        private void InitDataSet()
        {
            DataTable pluginsInformationTable = new DataTable("pluginsInfotmation");
            DataColumn dataColumn;
            //插件信息表生成
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "name";
            dataColumn.DataType = typeof(string);
            dataColumn.Unique = true;
            dataColumn.AutoIncrement = false;
            pluginsInformationTable.Columns.Add(dataColumn);
            
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "author";
            dataColumn.DataType = typeof(string);
            dataColumn.AutoIncrement = false;
            pluginsInformationTable.Columns.Add(dataColumn);
            
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "filename";
            dataColumn.DataType = typeof(string);
            dataColumn.AutoIncrement = false;
            pluginsInformationTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "version";
            dataColumn.DataType = typeof(string);
            dataColumn.AutoIncrement = false;
            pluginsInformationTable.Columns.Add(dataColumn);
            
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "description";
            dataColumn.DataType = typeof(string);
            dataColumn.AutoIncrement = false;
            pluginsInformationTable.Columns.Add(dataColumn);
            
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "uuid";
            dataColumn.DataType = typeof(string);
            dataColumn.AutoIncrement = false;
            pluginsInformationTable.Columns.Add(dataColumn);
            
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "privilege";
            dataColumn.DataType = typeof(int);
            dataColumn.AutoIncrement = false;
            dataColumn.Unique = true;
            pluginsInformationTable.Columns.Add(dataColumn);
            
            _dataSet.Tables.Add(pluginsInformationTable);
            this.pluginsInformationTable = _dataSet.Tables["pluginsInfotmation"]!;
        }
        
        public bool AddPlugin(string name, string author, string filename, string version, string description, int privilege)
        {
            DataRow dataRow = pluginsInformationTable.NewRow();
            dataRow["name"] = name;
            dataRow["author"] = author;
            dataRow["filename"] = filename;
            dataRow["version"] = version;
            dataRow["description"] = description;
            dataRow["privilege"] = privilege;
            dataRow["uuid"] = "-1"; //-1表示的是UUID并不存在
            //try-catch出问题的大概率是优先级的问题，在这个方法中不检查优先级是否合格
            try
            {
                pluginsInformationTable.Rows.Add(dataRow);
                pluginsInformationTable.AcceptChanges();
            }
            catch (Exception e)
            {
                _loggerService.Error(e,"PluginsLocalStorage","Cannot load plugin:" + name,e.Message);
                return false;
            }
            _loggerService.Info("PluginsLocalStorage","Plugin:" + name + " is loaded exactly.");
            _loggerService.Info(name,"Loading from framework. Author:" + author + " ,version:" + version+ ", with privilege:" + privilege);
            _loggerService.Info(name,"Description:" + description);
            return true;
        }

        public bool AddPlugin(string name, string author, string filename, string version, string description, string uuid,
            int privilege)
        {
            DataRow dataRow = pluginsInformationTable.NewRow();
            dataRow["name"] = name;
            dataRow["author"] = author;
            dataRow["filename"] = filename;
            dataRow["version"] = version;
            dataRow["description"] = description;
            dataRow["privilege"] = privilege;
            dataRow["uuid"] = uuid;
            //try-catch出问题的大概率是优先级的问题，在这个方法中不检查优先级是否合格
            try
            {
                pluginsInformationTable.Rows.Add(dataRow);
                pluginsInformationTable.AcceptChanges();
            }
            catch (Exception e)
            {
                _loggerService.Error(e,"PluginsLocalStorage","Cannot load plugin:" + name,e.Message);
                return false;
            }
            _loggerService.Info("PluginsLocalStorage","Plugin:" + name + " is loaded exactly.");
            _loggerService.Info(name,"Loading from framework. Author:" + author + " ,version:" + version + ", with privilege:" + privilege);
            _loggerService.Info(name,"Description:" + description);
            return true;
        }

        public bool TryGetPrivilege(int privilege, out int result)
        {
            if (pluginsInformationTable.AsEnumerable()
                    .FirstOrDefault(p => (int)p["privilege"] == privilege) != null)
            {
                TryGetPrivilege(privilege + 1, out int res);
                result = res;
                lastPrivilege = result + 1;
                return false;
            }
            result = privilege;
            lastPrivilege = result + 1;
            return true;
        }
        
        public bool TryGetPrivilegeUpper(int privilege, out int result)
        {
            if (pluginsInformationTable.AsEnumerable()
                    .FirstOrDefault(p => (int)p["privilege"] == privilege) != null)
            {
                TryGetPrivilege(privilege -1, out int res);
                result = res;
                lastPrivilege = result - 1;
                return false;
            }
            result = privilege;
            lastPrivilege = result - 1;
            return true;
        }
        
        public int GetLastUsablePrivilege() => lastPrivilege;

        public void RemovePlugin(string name)
        {
            DataRow dataRow = pluginsInformationTable.AsEnumerable().First(p => ((string)p["name"]).Equals(name));
            pluginsInformationTable.Rows.Remove(dataRow);
            pluginsInformationTable.AcceptChanges();
        }

        public void RemoveAllPlugins()
            => pluginsInformationTable.Clear();

        public List<Func<bool, MessageContext, ILoggerService, IPluginsDataStorage>> GetAction
                                                                    (EventType eventType,string TriggerMessage)
        {
            throw new NotImplementedException();
        }

        public string? GetAuthor(string name)
            => (string?)pluginsInformationTable.AsEnumerable().First(p => ((string)p["name"]).Equals(name))["author"];

        public string? GetFileName(string name)
            => (string?)pluginsInformationTable.AsEnumerable().First(p => ((string)p["name"]).Equals(name))["filename"];

        public string? GetVersion(string name)
            => (string?)pluginsInformationTable.AsEnumerable().First(p => ((string)p["name"]).Equals(name))["version"];

        public string? GetDescription(string name)
            => (string?)pluginsInformationTable.AsEnumerable().First(p => ((string)p["name"]).Equals(name))["descrption"];

        public int? GetPrivilege(string name)
            => (int)pluginsInformationTable.AsEnumerable().First(p => ((string)p["name"]).Equals(name))["privilege"];

        public string? GetUUID(string name)
            => (string?)pluginsInformationTable.AsEnumerable().First(p => ((string)p["name"]).Equals(name))["uuid"];

        public bool TryGetUUID(string name,out string uuid)
        {
            if (GetUUID("name") != null && GetUUID("name")!.Equals("-1"))
            {
                uuid = GetUUID("name")!;
                return false;
            }
            uuid = "-1";//表示uuid不存在
            return false;
        }

        public int EditPrivilege(string name, int privilege)
        {
            DataRow dataRow = pluginsInformationTable.AsEnumerable().First(p => ((string)p["name"]).Equals(name));
            if (TryGetPrivilege(privilege,out int newPrivilege))
            {
                dataRow["privilege"] = privilege;
                pluginsInformationTable.AcceptChanges();
                return privilege;
            }
            dataRow["privilege"] = newPrivilege;
            pluginsInformationTable.AcceptChanges();
            return newPrivilege;
        }

        public string? GetPluginByPrivilege(int privilege)
            => (string?)pluginsInformationTable.AsEnumerable().First(p => ((int)p["privilege"]).Equals(privilege))["name"];

        public int EditPrivilegeByUpper(string name, int privilege)
        {
            DataRow dataRow = pluginsInformationTable.AsEnumerable().First(p => ((string)p["name"]).Equals(name));
            if (TryGetPrivilegeUpper(privilege,out int newPrivilege))
            {
                dataRow["privilege"] = privilege;
                pluginsInformationTable.AcceptChanges();
                return privilege;
            }
            dataRow["privilege"] = newPrivilege;
            pluginsInformationTable.AcceptChanges();
            return newPrivilege;
        }

        public bool IsExists(string name) 
            => pluginsInformationTable.AsEnumerable().FirstOrDefault(p => ((string)p["name"]).Equals(name)) == null;
    }
}
