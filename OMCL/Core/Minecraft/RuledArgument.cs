using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OMCL.Core.Minecraft
{
    public class RuledArgument : Argument
    {
        public static RuledArgument Parse(MinecraftInstance instance, JObject obj) => new RuledArgument(instance, obj);
        public List<StringReplacableArgument> Arguments { get; set; } = new List<StringReplacableArgument>();

        public RuledArgument(MinecraftInstance instance, JObject obj) : base(instance)
        {
            Rules = Rules.Parse(obj["rules"] as JArray);
            if(obj["value"] is JValue)
            {
                Arguments.Add(new StringReplacableArgument(instance, obj["value"].ToString()));
            }
            else
            {
                foreach (string value in obj["value"] as JArray)
                {
                    Arguments.Add(new StringReplacableArgument(instance, value));
                }
            }
        }

        public Rules Rules { get; set; }

        public override bool IsNecessary => Rules.IsMatch();
        public override string ToString()
        {
            List<string> s = new List<string>();
            foreach(var arg in Arguments)
            {
                if (arg.IsNecessary) s.Add(arg.ToString());
            }
            return String.Join(" ", s.ToArray());
        }
    }
    

}
