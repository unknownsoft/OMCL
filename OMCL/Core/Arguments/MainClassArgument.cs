namespace OMCL.Core.Arguments
{
    public sealed class MainClassArgument : StringArgument
    {
        public MainClassArgument(string classname) : base(classname)
        {
        }
        public override bool WithSpaces => false;
        public override Priority Priority => Priority.Last;
    }
}
