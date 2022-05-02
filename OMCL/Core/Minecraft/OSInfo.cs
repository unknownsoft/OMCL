using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OMCL.Core.Minecraft
{
    public struct OSInfo
    {
        
        public Regex OSType { get; set; }
        public bool IsOSTypeMatch(OSType type)
        {
            return OSType.IsMatch(type.Name);
        }
        public Regex Version { get; set; }
        public string Arch { get; set; }
        public override string ToString()
        {
            List<string> lss = new List<string>();
            if (OSType != null) lss.Add(OSType.ToString());
            if (Version != null) lss.Add(Version.ToString());
            if (Arch != null) lss.Add(Arch.ToString());
            return String.Join(",", lss.ToArray());
        }
        public bool IsMatch(OSType type,string version,string arch)
        {
            if (OSType != null) if (!IsOSTypeMatch(type)) return false;
            if (Version != null) if (!IsOSTypeMatch(type)) return false;
            if (Arch != null) if ((Arch == "x86" && arch != "x86") || (Arch != "x86" && arch == "x86")) return false;
            return true;
        }

        public static OSInfo Parse(JObject obj)
        {
            OSInfo returnvalue = new OSInfo();
            if (obj["name"] != null) returnvalue.OSType = new Regex(obj["name"].ToString());
            if (obj["version"] != null) returnvalue.Version = new Regex(obj["version"].ToString());
            if (obj["arch"] != null) returnvalue.Arch = obj["arch"].ToString();
            return returnvalue;
        }
    }
    

}
