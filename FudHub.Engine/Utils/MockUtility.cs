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
        public static bool Save(object obj)
        {
            string _file = AppDomain.CurrentDomain.BaseDirectory + $@"\DataStore\{typeof(T).Name.ToString()}.json";
            string data = JsonConvert.SerializeObject(obj);
            var fs = new FileStream(_file, FileMode.Create);
            using (var s = new StreamWriter(fs))
            {
                s.WriteLine(data);
                s.Flush();
            }
            //File.WriteAllText(_file, data);
            return true;
        }

        public static string GetContent()
        {
            string _file = AppDomain.CurrentDomain.BaseDirectory + $@"\DataStore\{typeof(T).Name.ToString()}.json";
            var fs = new FileStream(_file, FileMode.Open, FileAccess.Read);
            string content = "";
            using (var r = new StreamReader(fs))
            {
                content = r.ReadToEnd();
            }
            //string acc = File.ReadAllText(_file);
            return content;
        }

        public static T GetObject()
        {
            var o = JsonConvert.DeserializeObject<T>(GetContent());
            return o;
        }

        public static List<T> GetList()
        {
            var l = JsonConvert.DeserializeObject<List<T>>(GetContent());
            return l ?? new List<T>();
        }
    }
}
