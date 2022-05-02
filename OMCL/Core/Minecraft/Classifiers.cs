using System.Collections.Generic;

namespace OMCL.Core.Minecraft
{
    public class Classifiers
    {
        private Dictionary<string, LibraryDownloadInfo> Info { get; set; }
        private Dictionary<OSType, string> OSTypes { get; set; }
        public Classifiers(Dictionary<string, LibraryDownloadInfo> classifierkeypairs, Dictionary<OSType, string> natives)
        {
            Info = classifierkeypairs;
            OSTypes = natives;
        }
        public LibraryDownloadInfo? this[OSType type]
        {
            get
            {
                if (OSTypes.ContainsKey(type))
                {
                    string value = OSTypes[type];
                    if (Info.ContainsKey(value))
                    {
                        return Info[value];
                    }
                }
                return null;
            }
        }
    }
    

}
