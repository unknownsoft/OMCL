using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OMCL.Core.Minecraft
{
    public class ArgumentCollection : List<Argument>
    {
        public override string ToString()
        {
            return String.Join(" ", this);
        }
        public static ArgumentCollection ParseLegacy(MinecraftInstance instance,string arg)
        {
            var result = new ArgumentCollection();
            result.Add(new StringReplacableArgument(instance, arg));
            return result;
        }
        public static ArgumentCollection Parse(MinecraftInstance instance, JArray json)
        {
            var result = new ArgumentCollection();
            foreach(JToken token in json)
            {
                result.Add(Argument.Parse(instance, token));
            }
            return result;
        }

        internal static ArgumentCollection Combine(ArgumentCollection para0, ArgumentCollection para1)
        {
            var result = new ArgumentCollection();
            foreach (var a in para0) result.Add(a);
            foreach (var a in para1) result.Add(a);
            return result;
        }
    }
    

}
