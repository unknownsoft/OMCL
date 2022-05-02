using System.Text.RegularExpressions;

namespace OMCL.Core.Minecraft
{
    public class StringReplacableArgument : Argument
    {
        public string Text = "";
        public StringReplacableArgument(MinecraftInstance instance,string text) : base(instance)
        {
            Text = text;
        }

        public override bool IsNecessary => true;

        public override string ToString()
        {
            string tr = Text;
            Regex reg = new Regex(@"\$\{([A-z_-]*)\}");
            var matches = reg.Matches(Text);
            foreach(Match match in matches)
            {
                var value = Instance.MiscDictionary[match.Groups[1].Value];
                if (value.Contains(" ")) value = "\"" + value + "\"";
                tr = tr.Replace(match.Value, value);
            }
            return tr;
        }
    }
    

}
