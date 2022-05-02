using Newtonsoft.Json.Linq;

namespace OMCL.Core.Minecraft
{
    public abstract class Argument
    {
        public Argument(MinecraftInstance instance) => Instance = instance;
        public MinecraftInstance Instance { get; set; }
        public abstract bool IsNecessary { get; }
        public abstract override string ToString();
        public static Argument Parse(MinecraftInstance instance, JToken token)
        {
            if (token is JValue) return new StringReplacableArgument(instance, token.ToString());
            if (token is JObject) return RuledArgument.Parse(instance, token as JObject);
            var obj = new JObject();
            obj["%%value%%"] = token;
            throw new MinecraftJsonReaderException("token should be a String Value or a Json Object", obj);
        }
    }
    

}
