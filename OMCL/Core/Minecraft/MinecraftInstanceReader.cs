namespace OMCL.Core.Minecraft
{
    public abstract class MinecraftInstanceReader
    {
        public virtual bool IsBased => false;
        public abstract bool GetMinecraftInstance(MinecraftDirectory directory, string name , out MinecraftInstance instance);

    }
    

}
