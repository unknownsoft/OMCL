using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OMCL.Core.Minecraft
{
    public struct LibraryDownloadInfo
    {
        public static LibraryDownloadInfo Parse(MinecraftLibraries libraries,JObject obj)
        {
            if (obj["path"] == null) throw new MinecraftJsonReaderException("path项不能为null", obj);
            if (obj["url"] == null) throw new MinecraftJsonReaderException("url项不能为null", obj);
            if (obj["sha1"] == null) throw new MinecraftJsonReaderException("sha1项不能为null", obj);
            if (obj["size"] == null) throw new MinecraftJsonReaderException("size项不能为null", obj);
            return new LibraryDownloadInfo(libraries, obj["path"].ToString(), obj["url"].ToString(), obj["sha1"].ToString(), int.Parse(obj["size"].ToString()));
        }
        public static Dictionary<string,LibraryDownloadInfo> GetClassifiers(MinecraftLibraries libraries, JObject obj)
        {
            var result = new Dictionary<string,LibraryDownloadInfo>();
            foreach(var prop in obj)
            {
                result[prop.Key] = Parse(libraries, prop.Value as JObject);
            }
            return result;
        }
        public LibraryDownloadInfo(MinecraftLibraries libraries,string path,string url,string sha1,long size)
        {
            Libraries = libraries;
            Path = path;
            Url = url;
            Sha1 = sha1;
            Size = size;
        }
        public string Url { get; set; }
        public string Sha1 { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
        public MinecraftLibraries Libraries { get; set; }
        public override string ToString() => (string.Join(",", new string[] { Url, Path, Sha1, Size.ToString() }));
    }
    

}
