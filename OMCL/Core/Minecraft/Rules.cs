using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OMCL.Core.Minecraft
{
    public class Rules : List<Rule>
    {
        public static Rules Parse(JArray rules)
        {
            var result = new Rules();
            foreach(JObject obj in rules)
            {
                result.Add(Rule.Parse(obj));
            }
            return result;
        }
        public bool IsMatch()
        {
            if (Count == 0) return true;
            else
            {
                bool match = false;
                foreach(var rule in this)
                {
                    if (rule.IsMatch())
                    {
                        if (rule.Action == Action.Allow)
                        {
                            match = true;
                        }
                        else
                        {
                            match = false;
                        }
                    }
                }
                return match;
            }
        }
    }
    

}
