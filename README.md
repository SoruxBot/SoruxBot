# SoruxBot  
A cross-chatting-platform bot framework implemented by CSharp.  
The framework supports multiplied chatting platform such as QQ, WeChat, Telegram, Discord and so on.  
Docs for SoruxBot: [Web](https://liaosunny123.github.io/SoruxBotDocs/)  
SoruxBot文档地址: [Web](https://liaosunny123.github.io/SoruxBotDocs/)  
# Feature  
You can use C# feature to make a plugin of this framework easily, such as attribute which implement command route and permission verify.  
This project is a easily-DIY framework which can be easily update according to your need by replacing the component of system.  
This framework is made after studying the source of asp.net core. So there are maybe some similiar logical in the main pipe model.  
# 架构  
SoruxBot 本身不实现协议层，接受外置的协议层输入[内置实现HTTP的正向和反向协议]，二次开发可通过加入自己的中间件，向按照内置接口MessageQueue加入消息管道。  
SoruxBot 提供可供二次开发的框架，框架耦合程度里，可以通过Interface中的接口实现二次开发，你可以Pr根据Interface实现的类到对应的文件夹，然后在Shell中指定特定的模块。目前可供替换的有：插件队列模块，消息队列模块，插件数据存储模块等。原生框架内置实现均为基于.net core库实现，你可以实现Redis，带数据库的类。
SoruxBot 处理消息的流程为：接受外界输入，加入消息管道，通过插件注册的过滤器进行过滤，然后经过指定插件的处理后输出。
# 特点  
- 框架支持一次开发，跨聊天平台使用。推荐使用框架内置协议完成主体功能开发，使用框架针对平台的扩展方法开发针对平台的扩展功能。  
- 框架支持指令路由，支持使用特性添加触发指令，支持指令参数的对应注入，含直接注入，可选注入等注入方式。  
- 框架支持权限节点，通过插件json文件中的权限节点配置命令的触发条件。内置多种触发变量，包括但不限于管理组，群主，非匿名成员等。  
- 框架支持冷却指令，通过特性添加冷却指令，支持与权限特性的搭配使用，方便区分不同用户组。  
- 框架支持针对方法的AOP，通过特性添加触发前和触发后的指定动作。  
- 框架支持IOC DI，通过Wrapper的配置指定对应的模块替换。  
- 框架支持解析特性，通过解析特性可以自动解析有特殊意义的文本，如艾特等，自动转换为可读文本。  
- 框架支持长对话模式，通过协程实现长对话功能，框架根据指定平台抽象出Read()，以方便插件开发。
- 框架支持扩展方法，通过指定API绕过框架指令路由直接与特定协议层通信。  
- 框架支持内置组件的替换，框架内部耦合程度低，可以根据个人需求基于Interface开发框架组建。推荐开发如Redis支持的缓存消息管道，如Mysql支持的数据库存储模块。  
- 框架支持模糊指令查询，可通过框架内置实现通过模糊字符查询可以匹配的特定插件向框架注册的命令。  
- 框架支持插件数据存储，插件可以通过框架提供的API方便的存储数据文件。[可针对特定配置项设置用户是否可以修改]
