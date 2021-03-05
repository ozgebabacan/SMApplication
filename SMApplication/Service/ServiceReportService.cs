using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SMApplication.Models;
using SMApplication.Models.Enum;

namespace SMApplication.Service
{
    /// <summary>
    /// Servis log işlemleri için hazırlanan Class
    /// </summary>
    public class ServiceReportService
    {
        /// <summary>
        /// Info log dosyasına yazılan servis logları okunarak liste haline getirilir.
        /// </summary>
        /// <returns>LogResponse</returns>
        public LogResponse ReadLog()
        {
            var result = new LogResponse();
            result.Logs = new List<LogModel>();
            try
            {

                var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                path = path.Substring(6);

                FileStream fileStream = new FileStream($"{path}\\logs\\info.log", FileMode.Open);

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    var lines = reader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in lines)
                    {
                        result.Logs.Add(JsonConvert.DeserializeObject<LogModel>(line));
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = Status.Error;
                result.Message = $"İşlem sırasında hata alındı. Detayları : {ex.Message}";
                
            }

            return result;
        }

    }
}
