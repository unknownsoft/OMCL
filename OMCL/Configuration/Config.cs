using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCL.Configuration
{
    public static class Config
    {
        public static ConfigFile AppConfig = new ConfigFile("OMCL\\config\\config.json");
    }
    public delegate void SettingsValueChangedEventHandler(string path, object value);
}
