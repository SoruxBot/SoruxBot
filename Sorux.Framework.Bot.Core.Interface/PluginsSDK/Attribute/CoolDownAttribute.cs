using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Interface.PluginsSDK.Attribute
{
    /// <summary>
    /// Cool down a method carry time.
    /// </summary>
    public class CoolDownAttribute : System.Attribute
    {
        //Target at special user type.
        public CoolDownAttribute(CoolDownTarget coolDownTarget, int time)
        {

        }
        //The user who have the permission will be excluded from being cooling down.
        public CoolDownAttribute(string excludePermission, int time)
        {

        }
        public enum CoolDownTarget
        {
            None = 0,
            SinglePerson = 1,
            CommonUsers = 2,
            CommonUsersAndManagers = 3,
            AllUsers = 4,
            GlobalCommonUsers = 5,
            GlobalCommonUsersAndManagers = 6,
            GlobalAllUsers = 7,
        }
    }
}
