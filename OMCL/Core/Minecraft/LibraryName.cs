using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace OMCL.Core.Minecraft
{
    public struct LibraryName
    {
        public static LibraryName Parse(string name)
        {
            var result = new LibraryName();
            string[] sss = name.Split(':');
            if (sss.Length < 3 || sss.Length > 3) throw new MinecraftJsonReaderException("包名 " + name + " 不符合规则：段数过多或过少", new JObject());
            result.Package = Package.Parse(sss[0]);
            result.Name = sss[1];
            result.Version = sss[2];
            return result;
        }
        public Package Package { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public override string ToString()
        {
            return String.Join(":", Package, Name, Version);
        }
        public string ToDirectory()
        {
            return String.Join("\\", Package.ToPath(), Name, Version);
        }
        public string ToFullPath() => Path.Combine(ToDirectory(), Name + Version + ".jar");
    }
    

}
