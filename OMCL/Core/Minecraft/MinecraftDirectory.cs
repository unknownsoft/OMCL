using OMCL.Configuration;
using System.Collections.Generic;
using System.IO;

namespace OMCL.Core.Minecraft
{
    public class MinecraftDirectory
    {
        public ConfigFile Config { get; private set; }
        public string Path { get; set; }
        public DirectoryInfo Directory => new DirectoryInfo(Path);
        public MinecraftDirectory(string path)
        {
            Path = path;
            Config = new ConfigFile(System.IO.Path.Combine(Path, "omcl.json"));
        }
        public string Name { get => Config.GetValue<string>("name", "默认文件夹"); set => Config.SetValue("name", value); }
        public string Description { get => Config.GetValue<string>("description", Name); set => Config.SetValue("description", value); }
        public List<MinecraftInstance> Versions
        {
            get
            {
                if (!System.IO.Directory.Exists(GetMemberPath(MinecraftDirectoryMember.Versions))) return new List<MinecraftInstance>();
                var result = new List<MinecraftInstance>();
                foreach(var dir in System.IO.Directory.GetDirectories(GetMemberPath(MinecraftDirectoryMember.Versions)))
                {
                    string name = System.IO.Path.GetFileName(dir);
                    result.Add(MinecraftInstance.Parse(this, name));
                }
                return result;
            }
        }
        public string GetMemberPath(MinecraftDirectoryMember member)
        {
            return System.IO.Path.Combine(Path, new string[] { "versions", "libraries", "assets" }[(int)member]);
        }
    }
}
