using Newtonsoft.Json.Linq;
using OMCL.Configuration;
using System.Collections.Generic;
using System.IO;

namespace OMCL.Core.Minecraft
{
    public class MinecraftLibrary
    {
        static Logger logger = new Logger("Minecraft Library Reader");
        public static MinecraftLibrary Parse(MinecraftLibraries libraries,JObject obj)
        {
            return CheckAndParse(new MinecraftLibrary(libraries), obj);
        }
        private static MinecraftLibrary CheckAndParse(MinecraftLibrary lib,JObject obj)
        {
            if (obj["name"] == null) throw new MinecraftJsonReaderException("name项不能为空",obj);
            lib.Name = LibraryName.Parse(obj["name"].ToString());
            if (obj["url"] == null) 
                lib.Url = Configuration.Minecraft.LibrariesSite;
            else 
                lib.Url = obj["url"].ToString();

            if (obj["downloads"] == null)
            {
                logger.warn("Object " + obj + " have neither artifact or classifiers keys");
                lib.CreateDefaultArtifact();
            }
            else
            {
                if (obj["downloads"]["artifact"] == null && obj["downloads"]["classifiers"] == null)
                {
                    logger.warn("Object " + obj + " have neither artifact or classifiers keys");
                    lib.CreateDefaultArtifact();
                }
                if (obj["downloads"]["artifact"] != null)
                {
                    lib.Artifact = LibraryDownloadInfo.Parse(lib.Parent, obj["downloads"]["artifact"] as JObject);
                }
                lib.ClassifiersKeyPairs = new Dictionary<string, LibraryDownloadInfo>();
                if (obj["downloads"]["classifiers"] != null && obj["natives"] != null)
                {
                    lib.ClassifiersKeyPairs = LibraryDownloadInfo.GetClassifiers(lib.Parent, obj["downloads"]["classifiers"] as JObject);
                    lib.Natives = ReadNatives(obj["natives"] as JObject);
                }
                else if (obj["downloads"]["classifiers"] == null && obj["natives"] == null) { }
                else
                {
                    throw new MinecraftJsonReaderException("classifiers和natives要么全为空要么全不为空", obj);
                }
            }
            lib.Extract = new ExtractRule();
            if (obj["extract"] != null)
            {
                if (obj["extract"]["exclude"] != null)
                {
                    foreach(string file in obj["extract"]["exclude"])
                    {
                        lib.Extract.Excludes.Add(file);
                    }
                }
            }
            if (obj["rules"] != null)
            {
                lib.Rules = Rules.Parse(obj["rules"] as JArray);
            }
            else
            {
                lib.Rules = new Rules();
            }
            return lib;
        }
        public static Dictionary<OSType, string> ReadNatives(JObject obj)
        {
            var result = new Dictionary<OSType, string>();
            foreach(var t in obj)
            {
                result.Add(OSType.Parse(t.Key), t.Value.ToString());
            }
            return result;
        }
        public void CreateDefaultArtifact()
        {
            this.Artifact = new LibraryDownloadInfo()
            {
                Libraries = this.Parent,
                Path = Name.ToFullPath(),
                Sha1 = "",
                Size = 0,
                Url = Path.Combine(Url, Name.ToFullPath())
            };
        }
        public MinecraftLibrary(MinecraftLibraries parent) => Parent = parent;
        public string Url { get; set; }
        public LibraryName Name { get; set; }
        public MinecraftLibraries Parent { get; set; }
        public LibraryDownloadInfo? Artifact { get; set; }
        public Dictionary<string, LibraryDownloadInfo> ClassifiersKeyPairs { get; set; }
        public Classifiers Classifiers => new Classifiers(ClassifiersKeyPairs, Natives);
        public Dictionary<OSType, string> Natives { get; set; }
        public ExtractRule Extract { get; set; }
        public Rules Rules { get; set; }
        public bool IsNecessary => Rules.IsMatch();
        public override string ToString() => Name.ToString();
    }
    

}
