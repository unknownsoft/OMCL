using System;
using System.IO;
using System.Text;

namespace OMCL.Configuration
{
    public static class StaticLogger
    {
        private static FileStream fs;
        public static void Init()
        {
            if (!Directory.Exists("OMCL"))
            {
                Directory.CreateDirectory("OMCL");
            }
            if (File.Exists("OMCL\\log.log"))
            {
                File.Delete("OMCL\\log.log");
            }
            fs = new FileStream("OMCL\\log.log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
        public static void info(string name, string text)
        {
            log("INFO", name, text);
        }
        public static void error(string name, string text)
        {
            log("ERROR", name, text);
        }
        public static void error(string name, Exception exception, string description)
        {
            error(name, getExceptionDescription(exception, description));
        }
        public static void error(string name, Exception exception)
        {
            error(name, getExceptionDescription(exception, "A Error occured"));
        }
        public static void warn(string name, string text)
        {
            log("WARN", name, text);
        }
        public static void warn(string name, Exception exception, string description)
        {
            warn(name, getExceptionDescription(exception, description));
        }
        public static void warn(string name, Exception exception)
        {
            warn(name, getExceptionDescription(exception, "Warning"));
        }
        public static void fatal(string name, string text)
        {
            log("FATAL", name, text);
        }
        public static void fatal(string name, Exception exception, string description)
        {
            fatal(name, getExceptionDescription(exception, description));
        }
        public static void fatal(string name, Exception exception)
        {
            fatal(name, getExceptionDescription(exception, "A Fatal Error occured"));
        }
        private static string getExceptionDescription(Exception exception, string description)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(description).Append(":\nException stack Details:\n").Append(exception);
            return sb.ToString();
        }
        private static void log(string type,string name,string text)
        {
            foreach(var line in text.Split('\n'))
            {
                logline(type, name, line);
            }
        }
        private static void logline(string type,string name,string line)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[").Append(DateTime.Now).Append("/").Append(type).Append("](").Append(name).Append(")").Append(" ").Append(line).Append("\n");
            var bytes = Encoding.Default.GetBytes(sb.ToString());
            lock (fs)
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
        }
    }
}
