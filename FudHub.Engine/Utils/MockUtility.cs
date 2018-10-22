using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Engine.Utils
{
    public class MockUtility<T>
    {
        public static bool Save( object obj)
        {
            string _file = AppDomain.CurrentDomain.BaseDirectory + $@"\DataStore\{typeof(T).Name.ToString()}.json";
            string data = JsonConvert.SerializeObject(obj);
            if (!File.Exists(_file)) File.Create(_file);
            File.WriteAllText(_file, data);
            return true;
        }

        public static string GetContent()
        {
            string _file = AppDomain.CurrentDomain.BaseDirectory + $@"\DataStore\{typeof(T).Name.ToString()}.json";
            if (!File.Exists(_file)) File.Create(_file);
            string acc = File.ReadAllText(_file);
            return acc;
        }

        public static T GetObject()
        {
            var o = JsonConvert.DeserializeObject<T>(GetContent());
            return o;
        }

        public static List<T> GetList()
        {
            var l = JsonConvert.DeserializeObject<List<T>>(GetContent());
            return l;
        }
    }
}
