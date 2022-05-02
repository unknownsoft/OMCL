using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OMCL.Core.Minecraft
{
    public struct Rule
    {
        public static Rule Parse(JObject obj)
        {
            var result = new Rule();
            if (obj["action"] == null) throw new MinecraftJsonReaderException("action项不能为空", obj);
            result.Action = (Action)(new List<string> { "allow", "disallow" }.IndexOf(obj["action"].ToString()));
            if (obj["os"] != null)
            {
                result.OSInfo = Core.Minecraft.OSInfo.Parse(obj["os"] as JObject);
            }
            return result;
        }
        public override string ToString() => Action + "(" + OSInfo.ToString() + ")";
        public Action Action { get; set; }
        public OSInfo? OSInfo { get; set; }
        public bool IsMatch()
        {
            if (OSInfo.HasValue == false) return true;
            if (OSInfo.Value.IsMatch(OSType.Windows, Environment.OSVersion.Version.Major.ToString(), Environment.Is64BitOperatingSystem ? "x86_64" : "x86")) return true;
            return false;
        }
    }
    

}
