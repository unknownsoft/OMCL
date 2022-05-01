using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMCL.Configuration
{
    public class Logger
    {
        public string Name { get; set; }
        public Logger(string name)
        {
            Name = name;
        }
        public void info( string text)
        {
            StaticLogger.info(Name, text);
        }
        public void error( string text)
        {
            StaticLogger.error(Name, text);
        }
        public void error( Exception exception, string description)
        {
            StaticLogger.error(Name, exception, description);
        }
        public void error( Exception exception)
        {
            StaticLogger.error(Name, exception);
        }
        public void warn( string text)
        {
            StaticLogger.warn(Name, text);
        }
        public void warn( Exception exception, string description)
        {
            StaticLogger.warn(Name, exception, description);
        }
        public void warn( Exception exception)
        {
            StaticLogger.warn(Name, exception);
        }
        public void fatal( string text)
        {
            StaticLogger.fatal(Name, text);
        }
        public void fatal( Exception exception, string description)
        {
            StaticLogger.fatal(Name, exception, description);
        }
        public void fatal( Exception exception)
        {
            StaticLogger.fatal(Name, exception);
        }
    }
}
