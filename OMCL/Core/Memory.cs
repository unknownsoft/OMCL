using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCL.Core
{
    public class Memory
    {
        public static ComputerInfo Management = new ComputerInfo();
        public static int TotalMemory => (int)(Management.TotalPhysicalMemory / 1024 / 1024);
        public static int AvaliableMemory => (int)(Management.AvailablePhysicalMemory / 1024 / 1024);
        public static int UsedMemory => TotalMemory - AvaliableMemory;
        public static int SuggestMinecraftMemory => (int)(AvaliableMemory * 0.7f);
    }
}
