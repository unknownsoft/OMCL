namespace OMCL.Core.Minecraft
{
    public struct MinecraftLaunchSettings
    {
        public static MinecraftLaunchSettings of(int width, int height) => new MinecraftLaunchSettings() { Width = width, Height = height, IsDemo = false, HasCustomResolution = true };
        public static MinecraftLaunchSettings defaultSettings() => new MinecraftLaunchSettings() { IsDemo = false, HasCustomResolution = false };
        public static MinecraftLaunchSettings ofDemo(int width, int height) => new MinecraftLaunchSettings() { Width = width, Height = height, IsDemo = true, HasCustomResolution = true };
        public static MinecraftLaunchSettings defaultSettingsDemo() => new MinecraftLaunchSettings() { IsDemo = true, HasCustomResolution = false };
        public int Width;
        public int Height;
        public bool HasCustomResolution;
        public bool IsDemo;
    }
}
