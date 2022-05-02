namespace OMCL.Core.Minecraft
{
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
    

}
