using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace OMCL.Core.Minecraft
{
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
        public MinecraftLibraries Libraries => MinecraftLibraries.Parse(this);
        public MinecraftArguments Arguments => MinecraftArguments.Parse(this, JSON);

        public override IEnumerable<Arguments.Argument> GetArguments(LaunchIdentity identity, MinecraftLaunchSettings settings)
        {
            MiscDictionary["auth_player_name"] = identity.Name;
            MiscDictionary["version_name"] = ID;
            MiscDictionary["game_directory"] = Directory.Path;
            MiscDictionary["assets_root"] = Directory.GetMemberPath(MinecraftDirectoryMember.Assets);
            MiscDictionary["assets_index_name"] = AssetIndexID;
            MiscDictionary["auth_uuid"] = identity.UUID;
            MiscDictionary["auth_access_token"] = identity.AuthToken;
            MiscDictionary["user_type"] = identity.LoginSignature;
            MiscDictionary["version_type"] = VersionType;
            MiscDictionary["is_demo_user"] = settings.IsDemo.ToString();
            MiscDictionary["has_custom_resolution"] = settings.HasCustomResolution.ToString();
            MiscDictionary["resolution_width"] = settings.Width.ToString();
            MiscDictionary["resolution_height"] = settings.Height.ToString();
            MiscDictionary["natives_directory"] = NativesDirectory;
            MiscDictionary["launcher_name"] = "OMCL";
            MiscDictionary["launcher_version"] = Properties.Settings.Default.DEBUGNUMBER;
            MiscDictionary["classpath"] = "%%%CLASS_PATH%%%%";
            MiscDictionary["library_directory"] = Directory.GetMemberPath(MinecraftDirectoryMember.Libraries);
            MiscDictionary["classpath_separator"] = ";";
            var result = new List<Arguments.Argument>();
            result.Add(new Arguments.MemoryArgument(GetMinMemUse(), GetMaxMemUse()));
            result.Add(new Arguments.JvmArgument(this.Arguments.Jvm.ToString()));
            result.Add(new Arguments.GameArgument(this.Arguments.Game.ToString()));
            result.Add(new Arguments.MainClassArgument(this.MainClass.ToString()));
            return result;
        }
        public string NativesDirectory => System.IO.Path.Combine(Path, ID + "-natives");
        public string VersionType => JSON["type"].ToString();
        public string AssetIndexID => JSON["assetIndex"]["id"].ToString();
        public string MainClass => JSON["mainClass"].ToString();
        public int GetMaxMemUse()
        {
            if (MaxMemoryUse == 0) return Memory.SuggestMinecraftMemory;
            else return MaxMemoryUse;
        }
        public int GetMinMemUse() => MinMemoryUse;
    }
    

}
