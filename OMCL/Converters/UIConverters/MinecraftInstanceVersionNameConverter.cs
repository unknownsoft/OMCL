using OMCL.Core.Minecraft;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OMCL.Converters.UIConverters
{
    public class MinecraftInstanceVersionNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "没有可用版本";
            if (!(value is MinecraftInstance)) return "没有可用版本";
            return (value as MinecraftInstance).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
