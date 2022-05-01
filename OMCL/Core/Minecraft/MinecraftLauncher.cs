using OMCL.Configuration;
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
        }
    }
    public class MinecraftInstanceLauncher
    {
        public MinecraftInstanceLauncher(MinecraftInstance instance)
        {

        }
    }
}
