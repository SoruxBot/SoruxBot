using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Interface
{
    public interface IPluginsDataStorage
    {
        bool AddStringSettings(string key, string value);
        bool RemoveStringSettings(string key);
        string GetStringSettings(string key);
        bool EditStringSettings(string key, string value);
        bool AddBinarySettings(string key, byte[] value);
        bool RemoveBinarySettings(string key);
        byte[] GetBinarySettings(string key);
        bool EditBinarySettings(string key, byte[] value);
    }
}
