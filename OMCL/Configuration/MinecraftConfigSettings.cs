namespace OMCL.Configuration
{
    public class MinecraftConfigSettings
    {
        public readonly string KEY_MAX_MEM_USE = "launcher\\minecraft\\max_memory_use";
        public readonly string KEY_MIN_MEM_USE = "launcher\\minecraft\\min_memory_use";
        public int MaxMemoryUse { get => Config.AppConfig.GetValue<int>(KEY_MAX_MEM_USE, 0); set => Config.AppConfig.SetValue(KEY_MAX_MEM_USE, value); }
        public int MinMemoryUse { get => Config.AppConfig.GetValue<int>(KEY_MIN_MEM_USE, 256); set => Config.AppConfig.SetValue(KEY_MIN_MEM_USE, value); }
    }
}
