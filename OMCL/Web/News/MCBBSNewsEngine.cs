using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OMCL.Web.News
{
    public class MCBBSNewsEngine : NewsEngine
    {
        public override string Name => "我的世界中文论坛(MCBBS)";

        public override Uri Site => new Uri("https://www.mcbbs.net");

        public override List<New> GetNews()
        {
            throw new NotImplementedException();
        }
    }
    public class MCBBSNew : New
    {
        public override string Title => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override void OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

}
