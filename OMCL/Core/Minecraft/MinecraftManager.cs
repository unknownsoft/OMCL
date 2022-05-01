using OMCL.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCL.Core.Minecraft
{
    public static class MinecraftManager
    {
        public static Logger logger = new Logger("Minecraft Manager");
        public static void Init()
        {
            logger.info("Begin initialization");
            if (GetMinecraftDirectoryPaths().Count == 0)
            {
                logger.info("Find 0 Minecraft Directories");
                Directory.CreateDirectory(".minecraft");
                logger.info("Add Local Minecraft Directory \".minecraft\" " + (AddMinecraftDirectory(".minecraft") ? "Succeeded" : "Failed"));
            }
            logger.info("MinecraftManager initialized");
        }
        public static readonly string MINECRAFT_PATHS_KEY = "omcl\\minecraftmanager\\paths";
        public static readonly string SELECTED_MINECRAFT_PATH_KEY = "omcl\\minecraftmanager\\paths\\selected";
        internal static List<MinecraftInstanceReader> Readers = new List<MinecraftInstanceReader>();
        public static void RegisterMinecraftInstanceReader(MinecraftInstanceReader reader)
        {
            Readers.Add(reader);
        }
        public static List<string> GetMinecraftDirectoryPaths() => new List<string>(Configuration.Config.AppConfig.GetArrayValue(MINECRAFT_PATHS_KEY));
        public static List<MinecraftDirectory> GetMinecraftDirectories()
        {
            var result = new List<MinecraftDirectory>();
            foreach(var dir in GetMinecraftDirectoryPaths())
            {
                if (Directory.Exists(dir))
                    result.Add(new MinecraftDirectory(dir));
            }
            return result;
        }
        public static bool AddMinecraftDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                Config.AppConfig.AddArrayValue(MINECRAFT_PATHS_KEY,directory);
                return true;
            }
            return false;
        }
        public static MinecraftDirectory SelectedDirectory
        {
            get
            {
                if (GetMinecraftDirectoryPaths().Count <= 0) return null;
                string selpath = Config.AppConfig.GetValue<string>(SELECTED_MINECRAFT_PATH_KEY, null);
                if (selpath == null)
                {
                    return GetMinecraftDirectories()[0];
                }
                else
                {
                    foreach(var dir in GetMinecraftDirectories())
                    {
                        if (dir.Path == selpath)
                        {
                            return dir;
                        }
                    }
                }
                return GetMinecraftDirectories()[0];
            }
        }
    }
}
