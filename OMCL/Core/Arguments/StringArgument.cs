namespace OMCL.Core.Arguments
{
    public abstract class StringArgument : Argument
    {
        public StringArgument(string text) => Text = text;
        public override bool IsEffective => true;

        public override bool WithSpaces => true;
        public string Text { get; set; }
        public override string ToString()
        {
            return Text.Trim();
        }
    }
}
