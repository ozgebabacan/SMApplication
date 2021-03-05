using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMApplication.Models
{
    /// <summary>
    /// Servis logları modeli
    /// </summary>
    public class LogResponse:BaseResponse
    {

        public List<LogModel> Logs{ get; set; }
    }

    public class LogModel {
        public DateTime TimeStamp { get; set; }
        public string Level { get; set; }
        public string LoggerName { get; set; }
        public string Message { get; set; }
        public string CallSite { get; set; }
        public string Response { get; set; }

        public DateTime LocaleTime
        {
            get { return TimeStamp.ToLocalTime(); } 
        }
    }
}
