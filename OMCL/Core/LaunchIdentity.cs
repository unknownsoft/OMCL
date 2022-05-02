using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlingo.Xoom.UUID;

namespace OMCL.Core
{
    public struct LaunchIdentity
    {
        public static LaunchIdentity CreateOffline(string Name)
        {
            var identity = new LaunchIdentity();
            identity.Name = Name;
            identity.LoginSignature = "Offline";
            using(NameBasedGenerator generator = new NameBasedGenerator())
            {
                identity.UUID = identity.XUID = identity.AuthToken = generator.GenerateGuid("OfflinePlayer:" + Name).ToString();
            }
            return identity;
        }
        public static LaunchIdentity CreateOnline(string Name, string UUID, string XUID, string Authtoken) => new LaunchIdentity()
        {
            Name = Name,
            UUID = UUID,
            XUID = XUID,
            AuthToken = Authtoken,
            LoginSignature = "Mojang"
        };
        public static LaunchIdentity CreateOnline(string Name, string UUID, string Authtoken) => CreateOnline(Name, UUID, Authtoken, Authtoken);
        public string Name;
        public string UUID;
        public string XUID;
        public string AuthToken;
        public string LoginSignature;
    }
}
