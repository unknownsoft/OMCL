using OMCL.Configuration;
using OMCL.Core.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCL.Core.Minecraft
{
    public class MinecraftLauncher
    {
        public Logger logger = new Logger("MinecraftLauncher");
        public void Launch(MinecraftInstance instance, LaunchIdentity identity) => Launch(instance, identity, MinecraftLaunchSettings.defaultSettings());
        public void Launch(MinecraftInstance instance,LaunchIdentity identity,MinecraftLaunchSettings settings)
        {
            var instlauncher = new MinecraftInstanceLauncher(instance);
            instlauncher.Identity = identity;
            instlauncher.Settings = settings;
        }
    }
    public class MinecraftInstanceLauncher
    {
        public LaunchIdentity Identity { get; set; }
        public MinecraftLaunchSettings Settings { get; set; }
        public MinecraftInstance Instance { get; private set; }
        public MinecraftInstanceLauncher(MinecraftInstance instance)
        {
            Instance = instance;
        }
        public string Argument 
        {
            get
            {
                ArgumentBuilder builder = new ArgumentBuilder(Instance.GetArguments(Identity, Settings));
                return builder.ToString();
            } 
        }
    }
}
