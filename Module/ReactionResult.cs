using Newtonsoft.Json;

namespace Module
{
    public enum ReactionStatus
    {
        Error = -1,
        Ok
    }
    public class ReactionResult
    {
        [JsonProperty()]
        public ReactionStatus Type { get; set; }
        [JsonProperty()]
        public string Message { get; set; }
    }
}