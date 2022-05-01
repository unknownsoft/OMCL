using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace OMCL.Configuration
{
    public class ConfigFile
    {
        public ConfigFile(string configpath)
        {
            ConfigPath = configpath;
        }
        public string ConfigPath = "";
        public void Check()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath));
            if (!File.Exists(ConfigPath))
            {
                File.WriteAllText(ConfigPath, new JObject().ToString());
            }
            try
            {
                JObject obj = JObject.Parse(File.ReadAllText(ConfigPath));
            }
            catch(JsonReaderException ex)
            {
                File.Delete(ConfigPath);
                Check();
            }
        }
        public JObject GetJObject()
        {
            Check();
            return JObject.Parse(File.ReadAllText(ConfigPath));
        }
        public void SetJObject(JObject obj)
        {
            Check();
            File.WriteAllText(ConfigPath, obj.ToString());
        }
        private Dictionary<String, object> _defaults = new Dictionary<string, object>();
        public Dictionary<string, object> Defaults => _defaults;
        public void RegisterDefaultValue(string path,object value)
        {
            Defaults[path] = value;
        }
        public void RegisterDefaultValue(string path, string value) => RegisterDefaultValue(path, (object)value);
        public void RegisterDefaultValue(string path, int value) => RegisterDefaultValue(path, (object)value);
        public void RegisterDefaultValue(string path, float value) => RegisterDefaultValue(path, (object)value);
        public void RegisterDefaultValue(string path, double value) => RegisterDefaultValue(path, (object)value);
        public void RegisterDefaultValue(string path, decimal value) => RegisterDefaultValue(path, (object)value);
        public void RegisterDefaultValue(string path, long value) => RegisterDefaultValue(path, (object)value);
        public void RegisterDefaultValue(string path, Color value) => RegisterDefaultValue(path, value.ToString());
        public void SetValue(string path,object value, bool eventraise = true)
        {
            var obj = GetJObject();
            obj[path] = (dynamic)value;
            SetJObject(obj);
            SettingsChanged(path, value);
            if (eventraise)
            {
                if (ValueChangedEvents.ContainsKey(path))
                {
                    foreach (var eve in ValueChangedEvents[path])
                    {
                        eve(path, value);
                    }
                }
            }
        }
        public void SetValue(string path, string value, bool eventraise = true) => SetValue(path, (object)value, eventraise);
        public void SetValue(string path, int value, bool eventraise = true) => SetValue(path, (object)value, eventraise);
        public void SetValue(string path, float value, bool eventraise = true) => SetValue(path, (object)value, eventraise);
        public void SetValue(string path, double value, bool eventraise = true) => SetValue(path, (object)value, eventraise);
        public void SetValue(string path, decimal value, bool eventraise = true) => SetValue(path, (object)value, eventraise);
        public void SetValue(string path, long value, bool eventraise = true) => SetValue(path, (object)value, eventraise);
        public void SetValue(string path, Color value, bool eventraise = true) => SetValue(path, (object)value.ToString(), eventraise);
        public object GetValue(string path,object def = null)
        {
            var obj = GetJObject();
            if (obj[path] == null)
            {
                if (def != null)
                {
                    return def;
                }
                if (Defaults.ContainsKey(path))
                {
                    return Defaults[path];
                }
                return def;
            }
            else
            {
                return obj[path];
            }
        }
        public T GetValue<T>(string path, T def)
        {
            if (GetValue(path, (object)def)==null) return default(T);
            if (typeof(T) == typeof(bool))
            {
                return (T)(dynamic)bool.Parse(GetValue(path, (object)def).ToString());
            }
            if (typeof(T) == typeof(double))
            {
                return (T)(dynamic)double.Parse(GetValue(path, (object)def).ToString());
            }
            if (typeof(T) == typeof(int))
            {
                return (T)(dynamic)int.Parse(GetValue(path, (object)def).ToString());
            }
            if (typeof(T) == typeof(long))
            {
                return (T)(dynamic)long.Parse(GetValue(path, (object)def).ToString());
            }
            if (typeof(T) == typeof(decimal))
            {
                return (T)(dynamic)decimal.Parse(GetValue(path, (object)def).ToString());
            }
            if (typeof(T) == typeof(float))
            {
                return (T)(dynamic)float.Parse(GetValue(path, (object)def).ToString());
            }
            if (typeof(T) == typeof(string))
            {
                return (T)(dynamic)(GetValue(path, (object)def).ToString());
            }
            if (typeof(T) == typeof(Color))
            {
                return (T)(dynamic)(Color)ColorConverter.ConvertFromString((GetValue(path, (object)def).ToString()));
            }
            return (T)GetValue(path, (object)def);
        }
        public T GetValue<T>(string path)
        {
            if (typeof(T) == typeof(bool))
            {
                return (T)(dynamic)bool.Parse(GetValue(path, null).ToString());
            }
            if (typeof(T) == typeof(double))
            {
                return (T)(dynamic)double.Parse(GetValue(path, null).ToString());
            }
            if (typeof(T) == typeof(int))
            {
                return (T)(dynamic)int.Parse(GetValue(path, null).ToString());
            }
            if (typeof(T) == typeof(long))
            {
                return (T)(dynamic)long.Parse(GetValue(path, null).ToString());
            }
            if (typeof(T) == typeof(decimal))
            {
                return (T)(dynamic)decimal.Parse(GetValue(path, null).ToString());
            }
            if (typeof(T) == typeof(float))
            {
                return (T)(dynamic)float.Parse(GetValue(path, null).ToString());
            }
            if (typeof(T) == typeof(string))
            {
                return (T)(dynamic)(GetValue(path, null).ToString());
            }
            if (typeof(T) == typeof(Color))
            {
                return (T)(dynamic)(Color)ColorConverter.ConvertFromString((GetValue(path, null).ToString()));
            }
            return (T)GetValue(path, null);
        }
        public void SetArrayValue(string path,string[] array)
        {
            JArray arr = new JArray();
            foreach (string item in array)
            {
                arr.Add(item);
            }
            var obj = GetJObject();
            obj[path] = arr;
            SetJObject(obj);
        }
        public string[] GetArrayValue(string path)
        {
            List<string> result = new List<string>();
            var obj = GetJObject();
            if (obj[path] == null) return new string[0];
            if (!(obj[path] is JArray)) return new string[0];
            foreach(string item in obj[path] as JArray)
            {
                result.Add(item);
            }
            return result.ToArray();
        }
        public void AddArrayValue(string path,string value)
        {
            var arr = new List<string>(GetArrayValue(path));
            arr.Add(value);
            SetArrayValue(path, arr.ToArray());
        }
        public bool RemoveArrayValue(string path, string value)
        {
            var arr = new List<string>(GetArrayValue(path));
            var result = arr.Remove(value);
            SetArrayValue(path, arr.ToArray());
            return result;
        }
        public bool ContainsArrayValue(string path, string value)
        {
            var arr = new List<string>(GetArrayValue(path));
            var result = arr.Contains(value);
            SetArrayValue(path, arr.ToArray());
            return result;
        }

        public void RegisterListener(string path,SettingsValueChangedEventHandler action)
        {
            if (ValueChangedEvents.ContainsKey(path))
            {
                ValueChangedEvents[path].Add(action);
            }
            else
            {
                ValueChangedEvents.Add(path, new List<SettingsValueChangedEventHandler>());
                ValueChangedEvents[path].Add(action);
            }
        }
        public event SettingsValueChangedEventHandler SettingsChanged = delegate { };
        private Dictionary<string, List<SettingsValueChangedEventHandler>> _valueChangedEvents = new Dictionary<string, List<SettingsValueChangedEventHandler>>();
        public Dictionary<string, List<SettingsValueChangedEventHandler>> ValueChangedEvents => _valueChangedEvents;
    }
}
