using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OMCL.Web.News
{
    public abstract class New
    {
        public abstract string Title { get; }
        public abstract string Description { get; }
        public abstract void OnClick(object sender, RoutedEventArgs e);
        public virtual Image Background => null;
        public override string ToString()
        {
            return Title;
        }
        public bool NoBackground = true;
    }
}
