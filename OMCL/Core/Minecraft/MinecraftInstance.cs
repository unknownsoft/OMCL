using Newtonsoft.Json.Linq;
using OMCL.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCL.Core.Minecraft
{
    public abstract class MinecraftInstance
    {
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
        public MinecraftInstance(string path, MinecraftDirectory dir)
        {
            Directory =dir;
            Path = path;
            ConfigFile = new ConfigFile(System.IO.Path.Combine(path, "OMCL\\omcl.json"));
        }
        public virtual string Name { get => ConfigFile.GetValue<string>("name", LocalName); set => ConfigFile.SetValue("name", value); }
        public virtual string Description { get => ConfigFile.GetValue<string>("description", new StringBuilder(Name).Append("(").Append(LocalName).Append(",").Append(ID).Append(")").ToString()); set => ConfigFile.SetValue("description", value); }
        public virtual string Path { get; private set; }
        public ConfigFile ConfigFile { get; private set; }
        public abstract string ID { get; }
        public virtual MinecraftDirectory Directory { get; private set; }
        public virtual string LocalName => System.IO.Path.GetFileName(Path);

    }
    public abstract class MinecraftInstanceReader
    {
        public virtual bool IsBased => false;
        public abstract bool GetMinecraftInstance(MinecraftDirectory directory, string name , out MinecraftInstance instance);  

    }
    public class VanillaMinecraftInstanceReader : MinecraftInstanceReader
    {
        public override bool IsBased => true;

        public override bool GetMinecraftInstance(MinecraftDirectory directory, string name, out MinecraftInstance instance)
        {
            try
            {
                instance = new VanillaMinecraftInstance(name, directory);
            }
            catch
            {
                instance = null;
            }
            return true;
        }
    }
    public class VanillaMinecraftInstance : MinecraftInstance
    {
        public VanillaMinecraftInstance(string name, MinecraftDirectory dir) : base(System.IO.Path.Combine(dir.GetMemberPath(MinecraftDirectoryMember.Versions), name), dir)
        {
            JSONPath = System.IO.Path.Combine(dir.GetMemberPath(MinecraftDirectoryMember.Versions), name, name + ".json");
            if (!File.Exists(JSONPath))
            {
                throw new FileNotFoundException("版本JSON文件丢失。", JSONPath);
            }
            
            {//CheckValues
                object obj = null;
                obj = ID;
            }
        }
        public string JSONPath { get; set; }
        public JObject JSON => JObject.Parse(File.ReadAllText(JSONPath));
        public override string ID => JSON["id"].ToString();
    }

}
