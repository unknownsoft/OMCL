using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCL.Web.News
{
    public abstract class NewsEngine
    {
        public abstract string Name { get; }
        public abstract Uri Site { get; }
        public bool IsRunning { get; private set; }
        public List<New> GetNews()
        {
            IsRunning = true;
            var result = InsideGetNews();
            IsRunning = false;
            return result;
        }
        public List<New> News => GetNews();
        public abstract List<New> InsideGetNews();

        public static List<NewsEngine> RegisteredEngines { get; private set; } = new List<NewsEngine>();
        public static void RegisterEngine(NewsEngine engine) => RegisteredEngines.Add(engine);
    }
}
