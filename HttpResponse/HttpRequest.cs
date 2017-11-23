using System;
using Newtonsoft.Json;

namespace HttpResponse
{
    public class HttpRequest
    {
        [JsonProperty()]
        public string Status { get; set; }

        [JsonProperty()]
        public string Message { get; set; }

        public HttpRequest(string code, string reason)
        {
            this.Status = code;
            this.Message = reason;
        }

        public HttpRequest()
        {
            this.Status = "KO";
            this.Message = "Message returned not defined";
        }

        public void ChangeResponse(string nStatus, string nMessage)
        {
            this.Status = nStatus;
            this.Message = nMessage;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings());
        }
    }
}
