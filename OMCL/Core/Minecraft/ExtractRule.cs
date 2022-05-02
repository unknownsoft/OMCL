using System;
using System.Collections.Generic;

namespace OMCL.Core.Minecraft
{
    public class ExtractRule
    {
        public override string ToString()
        {
            return String.Join(",", Excludes);
        }
        public List<string> Excludes { get; set; } = new List<string>();
    }
    

}
