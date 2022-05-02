using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace OMCL.Core.Minecraft
{
    public class MinecraftJsonReaderException : SystemException
    {
        public MinecraftJsonReaderException(string msg, JObject json) : base(msg)
        {
            Json = json;
        }
        public JObject Json { get; set; }
        public JsonReaderException ReaderException { get; set; }
        public override string ToString()
        {
            if (ReaderException != null)
                return base.ToString() + "\nInner Reader Exception:\n" + ReaderException + "\nJson:\n" + Json;
            else
                return base.ToString() + "\nJson:\n" + Json;
        }
    }
    

}
