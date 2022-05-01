using OMCL.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OMCL
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal static Logger asmlog = new Logger("Assembly");
        public App()
        {
            StaticLogger.Init();
            asmlog.info("Logger Initialization Complete");
            asmlog.info("Application Instance Created");
            asmlog.info("Class      :" + typeof(App).FullName);
            using (Process p = Process.GetCurrentProcess())
            {
                asmlog.info("Process    :" + p.Id);
            }
            asmlog.info("Initialize Models");
            Core.Minecraft.MinecraftManager.Init();
        }
    }
}
