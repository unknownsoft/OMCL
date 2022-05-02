using System;
using System.Collections.Generic;

namespace OMCL.Core.Minecraft
{
    public class Package : List<string>
    {
        public override string ToString()
        {
            return String.Join(".", this);
        }
        public string ToPath()
        {
            return String.Join("\\", this);
        }
        public string ToPath(string sepatator)
        {
            return String.Join(sepatator, this);
        }
        public static Package Parse(string packagename) => Parse(packagename.Split('.'));
        public static Package Parse(params string[] names)
        {

            Package pkg = new Package();
            foreach (string p in names) if (p.Trim() != "") pkg.Add(p);
            return pkg;
        }
        public static Package ParseFromPath(string path) => Parse(path.Replace("\\", ".").Replace("/", "."));
    }
    

}
