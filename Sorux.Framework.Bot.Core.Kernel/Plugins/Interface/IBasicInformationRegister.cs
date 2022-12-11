﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorux.Framework.Bot.Core.Kernel.Plugins.Interface
{
    public interface IBasicInformationRegister
    {
        string GetAuthor();
        string GetDescription();
        string GetName();
        string GetVersion();
        string GetDLL();
    }
}