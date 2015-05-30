using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StudyDirectionDbWebApp.Models
{
    public class LoggerInfo
    {
        public int LastId { get; set; }

    }

    public static class LoggerInfoAdapter
    {
        public static LoggerInfo Get(string fileName)
        {
            string content =  GetFileContent(fileName);
            return JsonConvert.DeserializeObject<LoggerInfo>(content);
        }

        private static string GetFileContent(string fileName)
        {
            string content;
            using (var srd = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                content = srd.ReadToEnd();
            }
            return content;
        }

        public static void Set(LoggerInfo loggerInfo, string fileName)
        {
            var content = JsonConvert.SerializeObject(loggerInfo);
            SaveLoggerInfo(fileName, content);
        }

        private static void SaveLoggerInfo(string fileName, string content)
        {
            lock (fileName)
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);

                using (var sw = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate)))
                {
                    sw.Write(content);
                }
            }
        }
    }
}
