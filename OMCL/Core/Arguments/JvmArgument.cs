namespace OMCL.Core.Arguments
{
    public sealed class JvmArgument : StringArgument
    {
        public JvmArgument(string text) : base(text)
        {
        }
        public override Priority Priority => Priority.Second;
    }
}
