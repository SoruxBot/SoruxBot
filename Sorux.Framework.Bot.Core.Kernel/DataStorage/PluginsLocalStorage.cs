using Sorux.Framework.Bot.Core.Kernel.Interface;
using Sorux.Framework.Bot.Core.Kernel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorux.Framework.Bot.Core.Kernel.Builder;
using Sorux.Framework.Bot.Core.Kernel.Plugins.Models;
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
            }
            catch (Exception e)
            {
                _loggerService.Error(e,"PluginsLocalStorage","Cannot load plugin:" + name,e.Message);
                return false;
            }
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
            }
            catch (Exception e)
            {
                _loggerService.Error(e,"PluginsLocalStorage","Cannot load plugin:" + name,e.Message);
                return false;
            }
            return true;
        }

        public bool TryGetPrivilege(int privilege, out int result)
        {
            if (pluginsInformationTable.AsEnumerable()
                    .Where(p => (int)p["privilege"] == privilege)
                    .FirstOrDefault() != null)
            {
                bool ret = TryGetPrivilege(privilege + 1, out int _);
                result = privilege + 1;
                return ret;
            }
            result = privilege;
            return true;

        }

        public int GetLastUsablePrivilege() => lastPrivilege;

        public void RemovePlugin(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllPlugins()
        {
            throw new NotImplementedException();
        }

        public List<Func<bool, MessageContext, ILoggerService, IPluginsDataStorage>> GetAction
                                                                    (EventType eventType,string TriggerMessage)
        {
            throw new NotImplementedException();
        }

        public string GetAuthor(string name)
        {
            throw new NotImplementedException();
        }

        public string GetFileName(string name)
        {
            throw new NotImplementedException();
        }

        public string GetVersion(string name)
        {
            throw new NotImplementedException();
        }

        public string GetDescription(string name)
        {
            throw new NotImplementedException();
        }

        public int GetPrivilege(string name)
        {
            throw new NotImplementedException();
        }

        public string GetUUID(string name)
        {
            throw new NotImplementedException();
        }

        public bool TryGetUUID(out string name)
        {
            throw new NotImplementedException();
        }

        public int EditPrivilege(string name, int privilege)
        {
            throw new NotImplementedException();
        }

        public string? GetPluginByPrivilege(int privilege)
        {
            throw new NotImplementedException();
        }

        public int EditPrivilegeByUpper(string name, int privilege)
        {
            throw new NotImplementedException();
        }
    }
}
