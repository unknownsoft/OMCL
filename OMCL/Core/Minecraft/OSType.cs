using Newtonsoft.Json.Linq;

namespace OMCL.Core.Minecraft
{
    public class OSType
    {
        public static OSType Parse(string type)
        {
            switch (type)
            {
                case "windows": return Windows;
                case "osx": return MacOS;
                case "linux": return Linux;
                case "unknown": return Unknown;
                default:throw new MinecraftJsonReaderException("操作系统类型 " + type + " 不是预期的", new JObject());
            }
        }
        private OSType() { }
        public string Name { get; set; }
        public override string ToString() => Name;
        public static OSType Windows = new OSType() { Name = "windows" };
        public static OSType MacOS = new OSType() { Name = "osx" };
        public static OSType Linux = new OSType() { Name = "linux" };
        public static OSType Unknown = new OSType() { Name = "unknown" };
    }
    

}
