using OMCL.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCL.Core.Minecraft
{
    public abstract class MinecraftInstance
    {
        public override string ToString() => Name;
        public static MinecraftInstance Parse(MinecraftDirectory directory,string name)
        {
            foreach(var reader in MinecraftManager.Readers)
            {
                if (!reader.IsBased)
                {
                    MinecraftInstance res = null;
                    if(reader.GetMinecraftInstance(directory,name,out res))
                    {
                        return res;
                    }
                }
            }
            foreach (var reader in MinecraftManager.Readers)
            {
                if (reader.IsBased)
                {
                    MinecraftInstance res = null;
                    if (reader.GetMinecraftInstance(directory, name, out res))
                    {
                        return res;
                    }
                }
            }
            {
                MinecraftInstance res = null;
                new VanillaMinecraftInstanceReader().GetMinecraftInstance(directory, name, out res);
                return res;
            }
        }
        public abstract IEnumerable<Arguments.Argument> GetArguments(LaunchIdentity identity, MinecraftLaunchSettings settings);
        public MinecraftInstance(string path, MinecraftDirectory dir)
        {
            Directory =dir;
            Path = path;
            ConfigFile = new ConfigFile(System.IO.Path.Combine(path, "OMCL\\omcl.json"));
        }
        public virtual bool FollowGlobalMemorySettings
        {
            get => ConfigFile.GetValue<bool>("follow_global_settings\\memory", true);
            set => ConfigFile.SetValue("follow_global_settings\\memory", value);
        }
        public virtual int MaxMemoryUse
        {
            get
            {
                if (FollowGlobalMemorySettings) return GlobalConfigSettings.Minecraft.MaxMemoryUse;
                else return ConfigFile.GetValue<int>("max_memory_use", 0);
            }
            set
            {
                if (FollowGlobalMemorySettings) GlobalConfigSettings.Minecraft.MaxMemoryUse = value;
                else ConfigFile.SetValue("max_memory_use", value);
            }
        }
        public virtual int MinMemoryUse
        {
            get
            {
                if (FollowGlobalMemorySettings) return GlobalConfigSettings.Minecraft.MinMemoryUse;
                else return ConfigFile.GetValue<int>("min_memory_use", 0);
            }
            set
            {
                if (FollowGlobalMemorySettings) GlobalConfigSettings.Minecraft.MinMemoryUse = value;
                else ConfigFile.SetValue("min_memory_use", value);
            }
        }
        public virtual string Name { get => ConfigFile.GetValue<string>("name", LocalName); set => ConfigFile.SetValue("name", value); }
        public virtual string Description { get => ConfigFile.GetValue<string>("description", new StringBuilder(Name).Append("(").Append(LocalName).Append(",").Append(ID).Append(")").ToString()); set => ConfigFile.SetValue("description", value); }
        public virtual string Path { get; private set; }
        public ConfigFile ConfigFile { get; private set; }
        public abstract string ID { get; }
        public virtual MinecraftDirectory Directory { get; private set; }
        public virtual string LocalName => System.IO.Path.GetFileName(Path);
        public Dictionary<string, string> MiscDictionary { get; set; } = new Dictionary<string, string>();

    }
    

}
