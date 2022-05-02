namespace OMCL.Core.Arguments
{
    public sealed class GameArgument : StringArgument
    {
        public GameArgument(string text) : base(text)
        {
        }
        public override Priority Priority => Priority.Tail;
    }
}
