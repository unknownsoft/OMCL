using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OMCL.Core.Minecraft
{
    public class MinecraftLibraries : List<MinecraftLibrary>
    {
        public MinecraftLibraries(MinecraftInstance instance) => MinecraftInstance = instance;
        public MinecraftInstance MinecraftInstance { get; set; }
        /// <summary>
        /// 需要Instance实例有名为JSON的属性并且JSON下需要有libraries项且为JArray
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static MinecraftLibraries Parse(MinecraftInstance instance)
        {
            return Parse(instance, ((dynamic)instance).JSON["libraries"]);
        }
        public static MinecraftLibraries Parse(MinecraftInstance instance, JArray json)
        {
            MinecraftLibraries lib = new MinecraftLibraries(instance);
            foreach(JObject obj in json)
            {
                lib.Add(MinecraftLibrary.Parse(lib, obj));
            }
            return lib;
        }
        public List<MinecraftLibrary> NecessaryLibraries
        {
            get
            {
                var result = new List<MinecraftLibrary>();
                foreach(var lib in this)
                {
                    if (lib.IsNecessary)
                        result.Add(lib);
                }
                return result;
            }
        }
        public List<MinecraftLibrary> UnNecessaryLibraries
        {
            get
            {
                var result = new List<MinecraftLibrary>();
                foreach (var lib in this)
                {
                    if (!lib.IsNecessary)
                        result.Add(lib);
                }
                return result;
            }
        }
    }
    

}
