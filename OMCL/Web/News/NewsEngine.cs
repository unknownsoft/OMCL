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
        public abstract List<New> GetNews();
    }
}
