using Newtonsoft.Json.Linq;

namespace OMCL.Core.Minecraft
{
    public class MinecraftArguments
    {
        public static MinecraftArguments Parse(MinecraftInstance instance, JObject versionjson)
        {
            var result = new MinecraftArguments();
            ArgumentCollection game0 = null;
            ArgumentCollection game1 = null;
            ArgumentCollection jvm = null;
            if (versionjson["arguments"] != null)
            {
                game1 = ArgumentCollection.Parse(instance, versionjson["arguments"]["game"] as JArray);
                if (versionjson["arguments"]["jvm"] != null) jvm = ArgumentCollection.Parse(instance, versionjson["arguments"]["jvm"] as JArray);
            }
            if (versionjson["minecraftArguments"] != null)
            {
                game0 = ArgumentCollection.ParseLegacy(instance, versionjson["minecraftArguments"].ToString());
            }
            if (game0 == null && game1 == null)
            {
                throw new MinecraftJsonReaderException("不能同时没有arguments\\\\game和minecraftArguments参数", versionjson);
            }else if (game0 != null && game1 != null)
            {
                result.Game = ArgumentCollection.Combine(game0, game1);
            }
            else
            {
                if (game0 != null) result.Game = game0;
                else result.Game = game1;
            }
            if (jvm != null) result.Jvm = jvm;
            return result;
        }
        public ArgumentCollection Game { get; set; }
        public ArgumentCollection Jvm { get; set; }
    }
    

}
