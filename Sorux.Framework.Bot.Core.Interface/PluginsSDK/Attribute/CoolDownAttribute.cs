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
    [AttributeUsage(AttributeTargets.Method)]
    public class CoolDownAttribute : System.Attribute
    {
        //Target at special user type.
        public CoolDownAttribute(int time,CoolDownLevel coolDownLevel = CoolDownLevel.SinglePerson)
        {

        }
        //The user who have the permission will be excluded from being cooling down.
        public CoolDownAttribute(string excludePermission, int time)
        {

        }
        public enum CoolDownLevel
        {
            None = 0, //表示没有特定的冷却对象 【已废弃此选项】
            SinglePerson = 1, //表示每个人的冷却时间独立计算
            CommonUsers = 2,  //表示所有的普通用户共用一个冷却时间
            CommonUsersAndManagers = 3, //表示所有的群用户公用一个冷却时间
            AllUsers = 4, //表示所有的群用户和群主公用一个冷却时间
            GlobalCommonUsers = 5,
            GlobalCommonUsersAndManagers = 6,
            GlobalAllUsers = 7,
        }
    }
}
