using System.Text.Json;
using System.Text.Json.Serialization;

namespace MISA.WebFresher042023.Demo.Core.MISAException
{
    public class BaseException
    {
        public int ErrorCode { get; set; }

        public string? DevMessage { get; set; }

        public string? UserMessage { get; set; }

        public string? TraceId { get; set; }    

        public string? MoreInfo { get; set; }

        public object Errors { get; set; } 

        public BaseException() { }

       public BaseException(int errorCode, string? devMessage, string? userMessage, string? traceId, string? moreInfo, object? errors)
        {
            ErrorCode = errorCode;
            DevMessage = devMessage;
            UserMessage = userMessage;
            TraceId = traceId;
            MoreInfo = moreInfo;
            Errors = errors;
        }   

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);

        }
    }
}
