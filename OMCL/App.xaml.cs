using OMCL.Configuration;
using OMCL.Core.Minecraft;
using OMCL.Web.News;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
            ServicePointManager.DefaultConnectionLimit = 768;
            asmlog.info("Logger Initialization Complete");
            asmlog.info("Application Instance Created");
            asmlog.info("Class      :" + typeof(App).FullName);
            using (Process p = Process.GetCurrentProcess())
            {
                asmlog.info("Process    :" + p.Id);
            }
            asmlog.info("Initialize Models");
            Core.Minecraft.MinecraftManager.Init();
            var engin = new Web.News.MCBBSNewsEngine();
            var arg = Core.Minecraft.MinecraftManager.GetMinecraftDirectories()[0].Versions[5].GetArguments(Core.LaunchIdentity.CreateOffline("offline"), MinecraftLaunchSettings.defaultSettings());
            NewsEngine.RegisterEngine(engin);
        }
    }
}
